using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IColorCoder
    {
        COLORCODE COLORCODE { get; }
        System.Drawing.Color MapColor(double value);
        void SetValues(double min, double max);
    }
}
