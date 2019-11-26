using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface ICartData : IDataUtilities<IVector3>
    {

        ICartData Unroll(double unrollRadius);
        ICylData AutoFitToCircle2Pts(System.Drawing.RectangleF searchBox1, System.Drawing.RectangleF searchBox2, double knownRadius);
        ICylData AutoFitToCircle3Pts(System.Drawing.RectangleF searchBox1, System.Drawing.RectangleF searchBox2);
        ICylData FitToCircle2Pts(IVector2 pt1, IVector2 pt2, double fitRadius);
        ICylData FitToCircle3Pts(IVector2 pt1, IVector2 pt2, IVector2 pt3);
        ICylData FitToCircle2Pts(double x1, double x2, double radius);
        IVector3 GetIntersectionAtX(double x);
        void RotateDataToLine(IVector3 pt1, IVector3 pt2);
        ICartData CenterToMidpoint();
        Tuple<double, double> GetMinMaxY();
        ICylData AsCylData();
        ICartData Translate(IVector3 vector3);
        void SortByX();
        void SortByY();
        void SortByZ();
    }
}
