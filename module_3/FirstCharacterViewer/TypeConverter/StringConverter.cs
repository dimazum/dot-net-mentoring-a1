using System;
using System.Linq;
using TypeConverter.Interfaces;

namespace TypeConverter
{
    public class StringConverter : IStringConverter
    {
        public int StrToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(str, "You entered empty string");
            }
            char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int response = 0;
            bool isNegative = str[0] == '-';
            string _str = str;
            if (isNegative)
            {
                _str = str.Substring(1);
            }
            else if (str[0] == '+')
            {
                _str = str.Substring(1);
            }

            foreach (char c in _str)
            {
                if (arr.All(n => n != c))
                {
                    throw new ArgumentException("The entered values are not numbers");
                }
                response *= 10;
                response += c - '0';
            }
            return isNegative ? -response : response;
        }
    }
}
