using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IDisplayData : IList<System.Drawing.PointF>
    {
        string ShortFileName { get; }
        double DataViewLocation { get; set; }
        System.Drawing.Color Color { get; set; }
        string FileName { get; set; }
        void AddRange(IEnumerable<System.Drawing.PointF> pointFs);
        System.Drawing.RectangleF GetBoundingRect(float borderPercent, int decimalPlaces);
        System.Drawing.RectangleF GetBoundingRect();
        IDisplayData TrimToWindow(System.Drawing.RectangleF window);
    }
}
