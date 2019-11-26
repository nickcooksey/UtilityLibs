using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IDisplayDataList : IList<IDisplayData>
    {
        void AddRange(IEnumerable<IDisplayData> datasets);
        int FindNearestFileIndex(System.Drawing.PointF mousePt, ref System.Drawing.PointF minPt);
    }
}
