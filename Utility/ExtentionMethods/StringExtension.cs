using System;
using System.Text.RegularExpressions;

namespace Utility.ExtentionMethods
{
    public static class StringExtension
    {
        public static int ExtractNumberFromString(this string numberString)
        {
            if (numberString == string.Empty)
                return -1;
            Regex regex = new Regex(@"\d+");
            Match match= regex.Match(numberString);

            return Convert.ToInt32(match.Value);            
        }
    }
}
