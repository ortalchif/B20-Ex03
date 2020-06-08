using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GaragelLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const float k_MaxAirPressuer = 28f;
        private const float k_MaxFuel = 120f;

        private bool m_HazardousMaterials;
        private float m_Capacity;

        public bool HazardousMaterials
        {
            get
            {
                return m_HazardousMaterials;
            }

            set
            {
                m_HazardousMaterials = value;
            }
        }

        public float Capacity
        {
            get
            {
                return m_Capacity;
            }

            set
            {
                m_Capacity = value;
            }
        }

        public Truck(string i_LicensePlate, string i_ModelName, string i_TireManufacturer, EngineType.eEngineType i_EngineType)
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
            ((Fuel)EngineType).FuelType = Fuel.eFuelType.Soler;
            EngineType.MaxAmountOfEnergy = k_MaxFuel;
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"{0}
Is a truck transporting hazardous materials?: {1}
Truck capacity is: {2}",
VehicleDetails(),
m_HazardousMaterials,
m_Capacity);

            return result;
        }
    }
}
