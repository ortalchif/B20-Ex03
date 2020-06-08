using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GaragelLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Information> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, Information>();
        }

        public Dictionary<string, Information> Vehicles
        {
            get
            {
                return m_Vehicles;
            }
        }

        public void AddVehicleToGarage(Vehicle i_AddVehicle, string i_OwnerName, string i_OwnerPhone)
        {
            Information vehicleInGarage;

            if (i_AddVehicle == null)
            {
                throw new ArgumentException("Given Vehicle not found.");
            }

            vehicleInGarage = new Information(i_AddVehicle, i_OwnerName, i_OwnerPhone);
            m_Vehicles.Add(i_AddVehicle.LicensePlate, vehicleInGarage);
        }

        public bool IsLicensePlateExists(string i_LicensePlate)
        {
            bool vehicleExsits;

            vehicleExsits = IsVehicleExists(i_LicensePlate);
            if (vehicleExsits)
            {
                ChangeExistingVehicleState(i_LicensePlate, Information.eStatus.InRepair);
            }

            return vehicleExsits;
        }

        public void ChangeExistingVehicleState(string i_LicensePlate, Information.eStatus i_VehicleStatus)
        {
            if (!IsVehicleExists(i_LicensePlate))
            {
                throw new ArgumentException("Given license Number not found.");
            }

            m_Vehicles[i_LicensePlate].CheckEqualStatus(i_VehicleStatus);
            m_Vehicles[i_LicensePlate].Status = i_VehicleStatus;
            throw new ArgumentException("Vehicle change to " + i_VehicleStatus);
        }

        public List<string> GetAllLicenceNumberByParamater(Information.eStatus i_VehicleConditionInTheGarage)
        {
            List<string> licensePlate = new List<string>();

            if (!isGarageEmpty())
            {
                foreach (Information information in m_Vehicles.Values)
                {
                    if (information.Status == i_VehicleConditionInTheGarage)
                    {
                        licensePlate.Add(information.Vehicle.LicensePlate);
                    }
                }
            }

            return licensePlate;
        }

        private bool isGarageEmpty()
        {
            return m_Vehicles.Count == 0;
        }

        public void AirPressureToMax(string i_LicensePlate)
        {
            if (!IsVehicleExists(i_LicensePlate))
            {
                throw new ArgumentException("Given license Number not found.");
            }
            else
            {
                foreach (Tire tire in m_Vehicles[i_LicensePlate].Vehicle.Tires)
                {
                    tire.AddAirPressure(tire.MaximumAirPressure - tire.CurrentAirPressure);
                }
            }
        }

        public string DisplayFullVehicleDataByLicensePlate(string i_LicensePlate)
        {
            string result = string.Empty;

            if (IsVehicleExists(i_LicensePlate))
            {
                result = m_Vehicles[i_LicensePlate].ToString();
            }
            else
            {
                throw new ArgumentException("Given license Number not found.");
            }

            return result;
        }

        public void AddElectricToVehicle(string i_LicensePlate, float i_MinToCharge)
        {
            if (IsVehicleExists(i_LicensePlate))
            {
                if (!(m_Vehicles[i_LicensePlate].Vehicle.EngineType is Electric))
                {
                    throw new ArgumentException("This Vehicle is not on electric");
                }

                (m_Vehicles[i_LicensePlate].Vehicle.EngineType as Electric).ChargeCar(m_Vehicles[i_LicensePlate].Vehicle, i_MinToCharge / 60);
            }
            else
            {
                throw new ArgumentException("Given license Number not found.");
            }
        }

        public void AddFuelToVehicle(string i_LicensePlate, Fuel.eFuelType i_FuelType, float i_FuelAmountToAdd)
        {
            if (IsVehicleExists(i_LicensePlate))
            {
                if (!(m_Vehicles[i_LicensePlate].Vehicle.EngineType is Fuel))
                {
                    throw new ArgumentException("This Vehicle is not on fuel");
                }

                (m_Vehicles[i_LicensePlate].Vehicle.EngineType as Fuel).Refueling(m_Vehicles[i_LicensePlate].Vehicle, i_FuelType, i_FuelAmountToAdd);
            }
            else
            {
                throw new ArgumentException("Given license Number not found.");
            }
        }

        public bool IsVehicleExists(string i_LicenseNumber)
        {
            Information garageVehicle;
            bool vehicleExists;

            vehicleExists = m_Vehicles.TryGetValue(i_LicenseNumber, out garageVehicle);

            return vehicleExists;
        }

        public void InsertCapacityOfCargo(Truck i_NewTruck, string i_CapacityOfCargo)
        {
            float CapacityOfCargo;

            if (!float.TryParse(i_CapacityOfCargo, out CapacityOfCargo))
            {
                throw new FormatException("Not Valid Float");
            }
            else if (CapacityOfCargo > 100000)
            {
                throw new ValueOutOfRangeException(1, 100000);
            }
            else
            {
                i_NewTruck.Capacity = CapacityOfCargo;
            }
        }

        public void InsertIfCanCargo(Truck i_NewTruck, bool HazardousMaterials)
        {
            if (HazardousMaterials == true)
            {
                i_NewTruck.HazardousMaterials = true;
            }
            else
            {
                i_NewTruck.HazardousMaterials = false;
            }
        }

        public void InsertAmountOfDoors(Car i_NewCar, Car.eAmountOfDoors i_UserChoice)
        {
            i_NewCar.AmountOfDoors = i_UserChoice;
        }

        public void InsertColor(Car i_NewCar, Car.eColor i_UserChoiceColor)
        {
            i_NewCar.Color = (Car.eColor)i_UserChoiceColor;
        }

        public void InsertEngineCapacity(Motorcycle i_NewBike, string i_EngineCapacity)
        {
            int engineCapacity;

            if (!int.TryParse(i_EngineCapacity, out engineCapacity))
            {
                throw new FormatException("Not Valid Interger");
            }
            else if (engineCapacity > 10000)
            {
                throw new ValueOutOfRangeException(1, 10000);
            }
            else
            {
                i_NewBike.EngineCapacity = engineCapacity;
            }
        }

        public void InsertLicenseType(Motorcycle i_NewBike, Motorcycle.eLisenceType i_LisenceType)
        {
            i_NewBike.LicenseType = i_LisenceType;
        }

        public void InsertAmountOfEnergyToAdd(Vehicle i_NewVehicle, string i_AmountOfEnergyToEnter)
        {
            float amountOfEnergyToEnter;
            bool valid;

            valid = float.TryParse(i_AmountOfEnergyToEnter, out amountOfEnergyToEnter);
            if (!valid)
            {
                throw new FormatException("Only Valid Number");
            }

            i_NewVehicle.EngineType.UpdateEnergy(amountOfEnergyToEnter);
            i_NewVehicle.UpdateEnergyPercent();
        }

        public void InsertCurrrentAirPressureOfTiers(Vehicle i_NewVehicle, string i_AirPressureToAdd)
        {
            float airPressureToAdd;
            bool valid;

            valid = float.TryParse(i_AirPressureToAdd, out airPressureToAdd);
            if (!valid)
            {
                throw new FormatException("Only Valid Number");
            }

            foreach (Tire currentTire in i_NewVehicle.Tires)
            {
                currentTire.AddAirPressure(airPressureToAdd);
            }
        }
    }
}
