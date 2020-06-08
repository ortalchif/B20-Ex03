using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GaragelLogic
{
    public class Tire
    {
        private readonly string m_Manufacturer;
        private float m_CurrentAirPressure = 0;
        private float m_MaximumAirPressure;

        public Tire(string i_Manufacturer, float i_MaximumAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaximumAirPressure = i_MaximumAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return m_MaximumAirPressure;
            }

            set
            {
                m_MaximumAirPressure = value;
            }
        }

        public void AddAirPressure(float i_AirPressureToAdd)
        {
            if (CurrentAirPressure + i_AirPressureToAdd > MaximumAirPressure ||
                CurrentAirPressure + i_AirPressureToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, MaximumAirPressure);
            }

            CurrentAirPressure += i_AirPressureToAdd;
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"Air pressure: {0}
Manufacturer: {1}",
m_CurrentAirPressure,
m_Manufacturer);

            return result;
        }
    }
}
