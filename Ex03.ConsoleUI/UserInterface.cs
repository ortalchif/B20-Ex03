using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly string r_MainMenu =
    @"Please enter your selection (1-8)
    (1) Put a new car into the garage.
    (2) Display the list of vehicle license numbers according to their condition
    (3) Changing the condition of the vehicle in the garage according to the license number
    (4) Maximize vehicle air pressure by license number
    (5) Fuel a vehicle driven by fuel according to license number, fuel type and quantity
    (6) Charge an electric vehicle according to the license number and the amount of minutes to charge
    (7) View full vehicle data by license number
    (8) Exit...
    ";

        private readonly Garage r_Garage;

        private eUserSelection m_UserSelection;
        private bool m_ExitProgram = false;

        public eUserSelection UserSelection
        {
            get
            {
                return m_UserSelection;
            }

            set
            {
                m_UserSelection = value;
            }
        }

        public UserInterface()
        {
            r_Garage = new Garage();
            welcomeScreen();
        }

        public enum eUserSelection
        {
            EnterNewVehicle = 1,
            ViewVehicleLicense,
            ChangeVehicleCondition,
            InflateAirInTheWheelsToMax,
            FuelVehicle,
            ChargeVehicle,
            ViewFullInformation,
            Exit
        }

        private void welcomeScreen()
        {
            Console.WriteLine("Hello And Welcome To Mor And Ortal Garage");
            firstScreen();
        }

        private void firstScreen()
        {
            while (!m_ExitProgram)
            {
                try
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine(r_MainMenu);
                    getUserSelection();
                    openMenuSelection();
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                }
            }
        }

        private void openMenuSelection()
        {
            switch (m_UserSelection)
            {
                case eUserSelection.EnterNewVehicle:
                    Console.Clear();
                    addVehicleToGarage();
                    break;
                case eUserSelection.ViewVehicleLicense:
                    Console.Clear();
                    displayAllLicenseNumbers();
                    break;
                case eUserSelection.ChangeVehicleCondition:
                    Console.Clear();
                    changeVehicleConditionInGarage();
                    break;
                case eUserSelection.InflateAirInTheWheelsToMax:
                    Console.Clear();
                    inflateVehicleTiers();
                    break;
                case eUserSelection.FuelVehicle:
                    Console.Clear();
                    addFuelToVehicle();
                    break;
                case eUserSelection.ChargeVehicle:
                    Console.Clear();
                    chargeElectricVehicle();
                    break;
                case eUserSelection.ViewFullInformation:
                    Console.Clear();
                    displayVehicleDetails();
                    break;
                case eUserSelection.Exit:
                    exitProgram();
                    break;
                default:
                    break;
            }
        }

        private void displayAllLicenseNumbers()
        {
            Information.eStatus status = (Information.eStatus)getUserChoiceFromEnumValues(typeof(Information.eStatus));
            List<string> licenses = r_Garage.GetAllLicenceNumberByParamater(status);

            if (licenses.Count > 0)
            {
                Console.WriteLine("List of vehicles with condition {0}", status);
                foreach (string currentLicense in licenses)
                {
                    Console.WriteLine(currentLicense);
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found!");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private void chargeElectricVehicle()
        {
            float chargeToAdd;
            string licenseNumber;
            string chargeAmountAsString;

            Console.WriteLine("Please enter license number: ");
            licenseNumber = Console.ReadLine();
            Console.WriteLine("Please enter how much to charge: ");
            chargeAmountAsString = Console.ReadLine();
            float.TryParse(chargeAmountAsString, out chargeToAdd);
            try
            {
                r_Garage.AddElectricToVehicle(licenseNumber, chargeToAdd);
                Console.WriteLine("Charged successfully");
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
            catch (FormatException ex)
            {
                errorMsg(ex);
            }
            catch (ValueOutOfRangeException ex)
            {
                Exception exception = new Exception(
                    string.Format(
                        "The value out of the range {0} to {1}",
                        ex.MinValue * 60,
                        ex.MaxValue * 60));
                errorMsg(exception);
            }
        }

        private void changeVehicleConditionInGarage()
        {
            try
            {
                string license;
                Console.WriteLine("Please enter licence number");
                license = Console.ReadLine();
                Information.eStatus newVehicleState = (
                    Information.eStatus)getUserChoiceFromEnumValues(typeof(Information.eStatus));
                r_Garage.ChangeExistingVehicleState(license, newVehicleState);
                Console.WriteLine("Vehicle state changed successfully");
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
            catch (FormatException ex)
            {
                errorMsg(ex);
            }
            catch (ValueOutOfRangeException ex)
            {
                errorMsg(ex);
            }
        }

        private void inflateVehicleTiers()
        {
            string license;

            try
            {
                Console.WriteLine("Please enter licence number");
                license = Console.ReadLine();
                r_Garage.AirPressureToMax(license);
                Console.WriteLine("Added air pressure successfully");
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
        }

        private void addFuelToVehicle()
        {
            float FuelToAdd;
            string FuelToAddAsString;
            string license;

            Console.WriteLine("Please enter licence number: ");
            license = Console.ReadLine();
            Fuel.eFuelType FuelType = (Fuel.eFuelType)getUserChoiceFromEnumValues(typeof(Fuel.eFuelType));
            Console.WriteLine("Please enter how many litres of fuel to add: ");
            FuelToAddAsString = Console.ReadLine();
            float.TryParse(FuelToAddAsString, out FuelToAdd);
            try
            {
                r_Garage.AddFuelToVehicle(license, FuelType, FuelToAdd);
                Console.WriteLine("Added Fuel successfully");
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
            catch (FormatException ex)
            {
                errorMsg(ex);
            }
            catch (ValueOutOfRangeException ex)
            {
                errorMsg(ex);
            }
        }

        private void getUserSelection()
        {
            int userSelection;
            bool result;

            result = int.TryParse(Console.ReadLine(), out userSelection);
            if (!result)
            {
                throw new FormatException("Invalid input only integer, try again");
            }

            if (userSelection < 0 || userSelection > 8)
            {
                throw new ValueOutOfRangeException(1, 8);
            }
            else
            {
                m_UserSelection = (eUserSelection)userSelection;
            }
        }

        private string getTireManufacturer()
        {
            string input;

            do
            {
                Console.WriteLine("Enter the tire manufacturer's name");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        private string getVehicleModelName()
        {
            string input;

            do
            {
                Console.WriteLine("Enter the model name of the vehicle");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        private string getVehicleLicensePlate()
        {
            string i_LicensePlate = string.Empty;

            Console.Clear();
            while (string.IsNullOrEmpty(i_LicensePlate))
            {
                Console.WriteLine("Insert vehicle license plate:");
                i_LicensePlate = Console.ReadLine();
            }

            try
            {
                r_Garage.IsLicensePlateExists(i_LicensePlate);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("This vehicle already exsits.");
                errorMsg(ex);
                firstScreen();
            }

            return i_LicensePlate;
        }

        private string getOwnerPhone()
        {
            string input = string.Empty;
            bool isValidInput = true;

            do
            {
                Console.WriteLine("Enter the owner's phone number");
                try
                {
                    input = Console.ReadLine();
                    isValidInput = Information.IsValidOwnerPhone(input);
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValidInput = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return input;
        }

        private string getOwnerName()
        {
            string input;
            bool result;

            do
            {
                Console.WriteLine("Please enter an owner name");
                input = Console.ReadLine();
                result = Information.IsValidOwnerName(input);
            }
            while (!result);

            return input;
        }

        private void exitProgram()
        {
            Console.WriteLine("Goodbye...");
            Thread.Sleep(500);
            m_ExitProgram = true;
        }

        private void addVehicleToGarage()
        {
            string ownerName;
            string ownerPhone;
            Vehicle vehicleToAdd;

            vehicleToAdd = getVehicleFromUser();
            ownerName = getOwnerName();
            ownerPhone = getOwnerPhone();
            try
            {
                r_Garage.AddVehicleToGarage(vehicleToAdd, ownerName, ownerPhone);
                Console.WriteLine("Vehicle was added succesfully");
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
        }

        private Vehicle getVehicleFromUser()
        {
            Vehicle createdVehicle = null;
            VehicleCreate.eType vehicleTypeToAdd;
            bool isValid = true;

            do
            {
                try
                {
                    vehicleTypeToAdd = (VehicleCreate.eType)getUserChoiceFromEnumValues(typeof(VehicleCreate.eType));

                    createdVehicle = VehicleCreate.CreateAVehicle(
                    vehicleTypeToAdd,
                    getVehicleLicensePlate(),
                    getVehicleModelName(),
                    getTireManufacturer());
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ArgumentException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);

            if (createdVehicle is Bike)
            {
                getEngineCapacity(createdVehicle);
                getLicenseType(createdVehicle);
            }
            else if (createdVehicle is Car)
            {
                getColor(createdVehicle);
                getAmountOfDoor(createdVehicle);
            }
            else if (createdVehicle is Truck)
            {
                getCapacityOfCargo(createdVehicle);
                getIfCanCargo(createdVehicle);
            }

            getAirPressureOfTiers(createdVehicle);
            getAmountOfEnergyToAdd(createdVehicle);

            return createdVehicle;
        }

        private void getLicenseType(Vehicle i_Vehicle)
        {
            Bike.eLisenceType lisenceType;
            bool isValid = true;

            do
            {
                try
                {
                    lisenceType = (Bike.eLisenceType)getUserChoiceFromEnumValues(typeof(Bike.eLisenceType));
                    r_Garage.InsertLicenseType((Bike)i_Vehicle, lisenceType);
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ArgumentException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void getEngineCapacity(Vehicle i_Vehicle)
        {
            string engineCapacity;
            bool isValid = true;

            do
            {
                try
                {
                    Console.WriteLine("Enter engine capacity");
                    engineCapacity = Console.ReadLine();
                    r_Garage.InsertEngineCapacity((Bike)i_Vehicle, engineCapacity);
                    isValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void getCapacityOfCargo(Vehicle i_Vehicle)
        {
            string CapacityOfCargo;
            bool isValid = true;

            do
            {
                try
                {
                    Console.WriteLine("Enter Capacity Of Cargo");
                    CapacityOfCargo = Console.ReadLine();
                    r_Garage.InsertCapacityOfCargo((Truck)i_Vehicle, CapacityOfCargo);
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void getIfCanCargo(Vehicle i_Vehicle)
        {
            bool CanCargo = false;
            string answer;

            do
            {
                Console.WriteLine("The Track Can Cargo a Hazardous Materials?");
                Console.WriteLine("Type yes or no");
                answer = Console.ReadLine();
                if (answer == "yes")
                {
                    CanCargo = true;
                }
                else if (answer == "no")
                {
                    CanCargo = false;
                }
            }
            while (answer != "yes" && answer != "no");

            r_Garage.InsertIfCanCargo((Truck)i_Vehicle, CanCargo);
        }

        private void getColor(Vehicle i_Vehicle)
        {
            Car.eColor color;
            bool isValid = true;

            do
            {
                try
                {
                    color = (Car.eColor)getUserChoiceFromEnumValues(typeof(Car.eColor));
                    r_Garage.InsertColor((Car)i_Vehicle, color);
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ArgumentException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void getAmountOfDoor(Vehicle i_Vehicle)
        {
            Car.eAmountOfDoors AmountOfDoors;
            bool isValid = true;

            do
            {
                try
                {
                    AmountOfDoors = (Car.eAmountOfDoors)getUserChoiceFromEnumValues(typeof(Car.eAmountOfDoors));
                    r_Garage.InsertAmountOfDoors((Car)i_Vehicle, AmountOfDoors);
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ArgumentException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void getAmountOfEnergyToAdd(Vehicle i_Vehicle)
        {
            string energyToAdd;
            bool isValidInput = true;

            do
            {
                try
                {
                    Console.WriteLine("Enter energy to add");
                    energyToAdd = Console.ReadLine();
                    r_Garage.InsertAmountOfEnergyToAdd(i_Vehicle, energyToAdd);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValidInput = false;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValidInput = false;
                }
            }
            while (!isValidInput);
        }

        private void getAirPressureOfTiers(Vehicle i_Vehicle)
        {
            string airPressureOfTiers;
            bool isValid = true;

            do
            {
                try
                {
                    Console.WriteLine("Enter air pressure of tiers to add");
                    airPressureOfTiers = Console.ReadLine();
                    r_Garage.InsertCurrrentAirPressureOfTiers(i_Vehicle, airPressureOfTiers);
                    isValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
                catch (FormatException ex)
                {
                    errorMsg(ex);
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private object getUserChoiceFromEnumValues(Type i_Enum)
        {
            object enumValueToReturn = null;
            int userchoiseIndexOfEnumValue;
            bool isNumber;
            string textualIndexOfEnumValue;

            if (!i_Enum.IsEnum)
            {
                throw new ArgumentException("Not eNum");
            }

            Console.WriteLine("Please choose one of the following: ");
            int currentValueIndex = 1;

            foreach (object enumValue in Enum.GetValues(i_Enum))
            {
                Console.WriteLine(string.Format(" {0}- {1}", currentValueIndex, enumValue));
                currentValueIndex++;
            }

            textualIndexOfEnumValue = Console.ReadLine();
            isNumber = int.TryParse(textualIndexOfEnumValue, out userchoiseIndexOfEnumValue);

            if (!isNumber)
            {
                throw new FormatException("Invalid number");
            }

            if (userchoiseIndexOfEnumValue < 1 || userchoiseIndexOfEnumValue > currentValueIndex - 1)
            {
                throw new ValueOutOfRangeException(1, currentValueIndex - 1);
            }

            currentValueIndex = 1;
            foreach (object enumValue in Enum.GetValues(i_Enum))
            {
                if (userchoiseIndexOfEnumValue == currentValueIndex)
                {
                    enumValueToReturn = enumValue;
                    break;
                }

                currentValueIndex++;
            }

            return enumValueToReturn;
        }

        private void displayVehicleDetails()
        {
            string licenseNumber = string.Empty;
            string result;

            Console.WriteLine("Please enter license number: ");
            licenseNumber = Console.ReadLine();
            try
            {
                result = r_Garage.DisplayFullVehicleDataByLicensePlate(licenseNumber);
                Console.WriteLine(result);
                Console.WriteLine("Press Any Key To Back To Menu");
                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                errorMsg(ex);
            }
        }

        private void errorMsg(Exception i_Exception)
        {
            ConsoleKeyInfo keyInfo;

            Console.WriteLine(i_Exception.Message);
            Console.WriteLine("Press ENTER to continue");
            do
            {
                keyInfo = Console.ReadKey(true);
            }
            while (keyInfo.Key != ConsoleKey.Enter);
        }
    }
}
