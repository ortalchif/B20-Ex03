using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_ModelName;
        private readonly string m_LicensePlate;
        private readonly EngineType m_EngineType;
        private float m_EnergyPercent;
        private List<Tire> m_Tires;  // ? למה זה לא מזהה צמיג? 

        public Vehicle(string i_LicensePlate, string i_ModelName, EngineType.eEngineType i_Source)
        {
            m_ModelName = i_ModelName;
            m_LicensePlate = i_LicensePlate;
            m_Tires = new List<Tire>();

            if (i_Source == EngineType.eEngineType.Electric)
            {
                m_EngineType = new Electric();
            }
            else
            {
                m_EngineType = new Fuel();
            }
        }

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        public float EnergyPercent
        {
            get
            {
                return m_EnergyPercent;
            }

            set
            {
                m_EnergyPercent = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }

            set
            {
                m_Tires = value;
            }
        }

        public EngineType EngineType
        {
            get
            {
                return m_EngineType;
            }
        }

        public abstract void SetEngineType();

        public abstract void TireCreater(string i_TireManufacturer);

        public string VehicleDetails()
        {
            string result;

            result = string.Format(
@"Vehicel Data :
license plate : {0}
model name : {1}
Tires information : {2}
EnergyPrecent : {3}%
{4}",
m_LicensePlate,
m_ModelName,
m_Tires[0].ToString(),
m_EnergyPercent,
m_EngineType.ToString());

            return result;
        }

        public void UpdateEnergyPercent()
        {
            EnergyPercent = (EngineType.CurrentAmountOfEnergy / EngineType.MaxAmountOfEnergy) * 100;
        }

        public abstract override string ToString();
    }
}
