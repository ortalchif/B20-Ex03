using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleCreate
    {
        public enum eType
        {
            MotorcycleOnFuel = 1,
            MotorcycleOnElectric,
            CarOnFuel,
            CarOnElectric,
            Truck,
        }

        public static Vehicle CreateAVehicle(
            eType i_TypeOfVehicle,
            string i_LicensePlate,
            string i_ModelName,
            string i_TierManufacturer)
        {
            Vehicle result = null;

            switch (i_TypeOfVehicle)
            {
                case eType.MotorcycleOnFuel:
                    result = new Motorcycle(i_LicensePlate, i_ModelName, i_TierManufacturer, EngineType.eEngineType.Fuel);
                    break;

                case eType.MotorcycleOnElectric:
                    result = new Motorcycle(i_LicensePlate, i_ModelName, i_TierManufacturer, EngineType.eEngineType.Electric);
                    break;

                case eType.CarOnFuel:
                    result = new Car(i_LicensePlate, i_ModelName, i_TierManufacturer, EngineType.eEngineType.Fuel);
                    break;

                case eType.CarOnElectric:
                    result = new Car(i_LicensePlate, i_ModelName, i_TierManufacturer, EngineType.eEngineType.Electric);
                    break;

                case eType.Truck:
                    result = new Truck(i_LicensePlate, i_ModelName, i_TierManufacturer, EngineType.eEngineType.Fuel);
                    break;
            }

            return result;
        }
    }
}
