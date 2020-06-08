using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GaragelLogic
{
    public class Electric : EngineType
    {
        public override float GetPercentagesOfEnergy()
        {
            return (MaxAmountOfEnergy / CurrentAmountOfEnergy) * 100;
        }

        public void ChargeCar(Vehicle i_NewVehicle, float i_AmountOfTime)
        {
            UpdateEnergy(i_AmountOfTime);
            i_NewVehicle.UpdateEnergyPercent();
        }

        public override string ToString()
        {
            return string.Format(
@"Battery running time left : {0}
Max battery running time : {1}",
CurrentAmountOfEnergy,
MaxAmountOfEnergy);
        }
    }
}
