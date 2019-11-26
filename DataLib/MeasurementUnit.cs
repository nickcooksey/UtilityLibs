using IDataLib;
using System;

namespace DataLib
{

    public class MeasurementUnit : IMeasurementUnit
    {
        public double ConvToMicron { get; private set; }
        public string Name { get; private set; }
        public LengthUnit LengthUnits { get; private set; }

        private MeasurementUnitDictionary dictionary;
        public MeasurementUnit()
        {
            dictionary = new MeasurementUnitDictionary();
        }
        public MeasurementUnit(LengthUnit lengthUnit)
        {
            dictionary = new MeasurementUnitDictionary();
            SetLengthUnit(lengthUnit);
        }
        public void SetLengthUnit(LengthUnit lengthUnit)
        {
            ConvToMicron = dictionary.ConversionToUM(lengthUnit);
            Name = lengthUnit.ToString();
            LengthUnits = lengthUnit;
        }
        public IMeasurementUnit GetMeasurementUnit(string[,] words)
        {
            try
            {
                var unitList = dictionary.GetMeasurementUnitNames();
                LengthUnit lengthUnit = LengthUnit.MICRON;

                IMeasurementUnit inputUnit = new MeasurementUnit();

                foreach (string unitStr in unitList)
                {
                    for (int i = 0; i < words.GetLength(0); i++)
                    {
                        for (int j = 0; j < words.GetLength(1); j++)
                        {
                            string upperw = words[i, j].ToUpper();
                            if (upperw.Contains(unitStr))
                            {
                                Enum.TryParse(unitStr, out lengthUnit);
                                inputUnit.SetLengthUnit(lengthUnit);
                                break;
                            }
                        }
                    }
                }
                inputUnit.SetLengthUnit(lengthUnit);
                return inputUnit;
            }
            catch (Exception)
            {

                throw;
            }
        }

       
       
    }

}
