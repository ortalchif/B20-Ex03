using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Information
    {
        private const int k_MaxPhoneNumberLength = 10;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eStatus m_Status;
        private Vehicle m_Vehicle;

        public enum eStatus
        {
            InRepair = 1,
            Repaired,
            Paid,
        }

        public Information(Vehicle i_NewVehcile, string i_OwnerName, string i_OwnerPhone)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Vehicle = i_NewVehcile;
            m_Status = eStatus.InRepair;
        }

        public static bool IsValidOwnerPhone(string i_Input)
        {
            bool result = true;
            long checkNumber;

            if (i_Input == string.Empty)
            {
                result = false;
            }
            else
            {
                if (!long.TryParse(i_Input, out checkNumber))
                {
                    throw new FormatException("Phone Number Contains Only Numbers");
                }

                if (i_Input.Length > k_MaxPhoneNumberLength || i_Input.Length < 3)
                {
                    throw new ValueOutOfRangeException(3, k_MaxPhoneNumberLength);
                }
            }

            return result;
        }

        public static bool IsValidOwnerName(string i_OwnerName)
        {
            bool result = true;

            if (i_OwnerName.Length > 2)
            {
                foreach (char letter in i_OwnerName)
                {
                    if (!char.IsLetter(letter))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            set
            {
                m_OwnerPhone = value;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result;
            string information;

            result = new StringBuilder();
            information = string.Format(
@"Owner name: {0}
Owner phone: {1}
Vehicle status: {2}
",
m_OwnerName,
m_OwnerPhone,
m_Status);
            result.Append(information);
            result.Append(m_Vehicle.ToString());

            return result.ToString();
        }

        public void CheckEqualStatus(eStatus i_UserChoice)
        {
            if ((Information.eStatus)i_UserChoice == m_Status)
            {
                throw new ArgumentException(
                    string.Format(
                    "The vehicle is already in {0} status",
                    m_Status));
            }
        }
    }
}
