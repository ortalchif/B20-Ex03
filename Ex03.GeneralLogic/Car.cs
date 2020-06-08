using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxBatteryRunningTime = 2.1f;
        private const float k_MaxAirPressuer = 32f;
        private const float k_MaxFuel = 60f;

        private eColor m_Color;
        private eAmountOfDoors m_AmountOfDoors;

        public enum eColor
        {
            Red = 1,
            white,
            Black,
            Grey
        }

        public enum eAmountOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public eAmountOfDoors AmountOfDoors
        {
            get
            {
                return m_AmountOfDoors;
            }

            set
            {
                m_AmountOfDoors = value;
            }
        }


        public Car(string i_LicensePlate, string i_ModelName, string i_TireManufacturer, EngineType.eEngineType i_EngineType)
            : base(i_LicensePlate, i_ModelName, i_EngineType)
        {
            TireCreater(i_TireManufacturer);
            SetEngineType();
        }

        public override void TireCreater(string i_TireManufacturer)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Tires.Add(new Tire(i_TireManufacturer, k_MaxAirPressuer));
            }
        }
        
        public override void SetEngineType()
        {
            if (EngineType is Fuel)
            {
                ((Fuel)EngineType).FuelType = Fuel.eFuelType.Octan96;
                EngineType.MaxAmountOfEnergy = k_MaxFuel;
            }
            else
            {
                EngineType.MaxAmountOfEnergy = k_MaxBatteryRunningTime;
            }
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"{0}
Car Color: {1}
Car door quantity: {2}
",
VehicleDetails(),
m_Color.ToString(),
m_AmountOfDoors.ToString());

            return result;
        }
    }
}
