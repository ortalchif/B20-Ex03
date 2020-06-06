using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EngineType
    {
        public enum eEngineType
        {
            Fuel = 1,
            Electric,
        }

        private float m_CurrentAmountOfEnergy;
        private float m_MaxAmountOfEnergy;

        public float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                m_CurrentAmountOfEnergy = value;
            }
        }

        public float MaxAmountOfEnergy
        {
            get
            {
                return m_MaxAmountOfEnergy;
            }

            set
            {
                m_MaxAmountOfEnergy = value;
            }
        }

        public void UpdateEnergy(float i_EnergyToEnter)
        {
            if (m_CurrentAmountOfEnergy + i_EnergyToEnter > m_MaxAmountOfEnergy
                || m_CurrentAmountOfEnergy + i_EnergyToEnter < 0)
            {
                throw new ValueOutOfRangeException(0, MaxAmountOfEnergy - CurrentAmountOfEnergy);
            }

            m_CurrentAmountOfEnergy += i_EnergyToEnter;
        }

        public abstract float GetPercentagesOfEnergy();

        public abstract override string ToString();
    }
}
