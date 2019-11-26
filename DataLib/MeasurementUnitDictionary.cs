using IDataLib;
using System.Collections.Generic;

namespace DataLib
{


    internal class MeasurementUnitDictionary 
    {

        internal double ConversionToUM(LengthUnit lengthUnit)
        {
            double value = 1.0;
            unitDictionary.TryGetValue(lengthUnit, out value);
            return value;
        }
        internal List<string> GetMeasurementUnitNames()
        {
            var keys = unitDictionary.Keys;
            var keyList = new List<string>();
            foreach (LengthUnit key in keys)
            {
                keyList.Add(key.ToString());
            }
            return keyList;
        }

        private Dictionary<LengthUnit, double> unitDictionary;
        internal MeasurementUnitDictionary()
        {
            unitDictionary = new Dictionary<LengthUnit, double>();
            unitDictionary.Add(LengthUnit.INCH, 3.937e-5);
            unitDictionary.Add(LengthUnit.MICRON, 1);
            unitDictionary.Add(LengthUnit.MM, 1e-3);
            unitDictionary.Add(LengthUnit.UM, 1);

        }
    }
}
