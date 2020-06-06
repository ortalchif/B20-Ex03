using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(
                string.Format(
                "The value out of the range {0} to {1}",
                i_MinValue,
                i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MinValue
        {
            get
            {
                return this.r_MinValue;
            }
        }

        public float MaxValue
        {
            get
            {
                return this.r_MaxValue;
            }
        }
    }
}