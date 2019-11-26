using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface ICylData : IDataUtilities<IPointCyl>
    {
        double MinRadius { get; set; }
        Tuple<double, double> GetMinMaxR();
        IPointCyl GetMinRadiusPt();
        double GetMinAveRadius(int searchWindowHalfW);
        void RotateAbtZ(double thetaRad);
        void SortByTheta();
        void SortByZ();
        void SortByR();
        ICylData CenterToFirstGrooveMidpoint();
        void TrimWidth(double minAngleRads, double maxAngleRads);
        ICartData Unroll(double scaling, double unrollRadius);
    }
}
