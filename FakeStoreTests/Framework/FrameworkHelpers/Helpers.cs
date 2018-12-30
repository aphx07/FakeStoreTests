using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrameworkHelpers.Helpers
{
    public class Helpers
    {
        public static float ConvertPriceToNumber(string textPrice)
        {
            //Removing "zł." and empty spaces and replacing comas with dots. 
            //Parsing to float
            return float.Parse(Regex.Replace(textPrice, @"[zł.\s]", "").Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
        }
        
    }
}
