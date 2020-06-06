using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxBatteryRunningTime = 1.8f;
        private const float k_MaxAirPressuer = 31f;
        private const float k_MaxFuel = 55f;

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
        
    }
}
