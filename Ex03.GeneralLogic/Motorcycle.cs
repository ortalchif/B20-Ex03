using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const float k_MaxBattery = 1.2f;
        private const float k_MaxAirPressuer = 30f;
        private const float k_MaxFuel = 7f;

        private eLisenceType m_LicenseType;
        private int m_EngineCapacity;

        public enum eLisenceType
        {
            A = 1,
            A1,
            A2,
            B,
        }

        public Motorcycle(string i_LicensePlate, string i_ModelName, string i_TireManufacturer, EngineType.eEngineType i_EngineType)
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

        public eLisenceType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        public override void SetEngineType()
        {
            if (EngineType is Fuel)
            {
                ((Fuel)EngineType).FuelType = Fuel.eFuelType.Octan95;
                EngineType.MaxAmountOfEnergy = k_MaxFuel;
            }
            else
            {
                EngineType.MaxAmountOfEnergy = k_MaxBattery;
            }
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"{0}
Motorcycle license type: {1}
Motorcycle engine cpacity: {2}",
VehicleDetails(),
m_LicenseType.ToString(),
m_EngineCapacity);
            return result;
        }
    }
}
