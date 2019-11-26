using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryLib;

namespace DataLib
{
    public class ProfileData : List<Vector2>
    {
        public void SortByX()
        {
            var xList = new List<double>();
            var ptList = new List<Vector2>();
            foreach (var pt in this)
            {
                xList.Add(pt.X);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(xList.ToArray(), arr);
            this.Clear();
            this.AddRange(arr);
        }
    }
}
