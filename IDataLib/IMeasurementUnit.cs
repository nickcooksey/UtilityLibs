using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IMeasurementUnit
    {
        double ConvToMicron { get; }
        string Name { get; }
        LengthUnit LengthUnits { get; }
        void SetLengthUnit(LengthUnit lengthUnit);
        IMeasurementUnit GetMeasurementUnit(string[,] words);
    }
}
