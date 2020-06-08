using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GaragelLogic
{
    public class Fuel : EngineType
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler,
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public void Refueling(Vehicle i_NewVehicle, eFuelType i_FuelType, float i_Amount)
        {
            if (i_FuelType == m_FuelType)
            {
                UpdateEnergy(i_Amount);
                i_NewVehicle.UpdateEnergyPercent();
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                    "You enterd an improper fuel type, {0} insted of {1}",
                    i_FuelType,
                    m_FuelType));
            }
        }

        public override float GetPercentagesOfEnergy()
        {
            return (MaxAmountOfEnergy / CurrentAmountOfEnergy) * 100;
        }

        public override string ToString()
        {
            return string.Format(
@"Fuel Type : {0}
Max amount of fuel : {1}
Current amount of fuel : {2}",
m_FuelType,
MaxAmountOfEnergy,
CurrentAmountOfEnergy);
        }
    }
}
