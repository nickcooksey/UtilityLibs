using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface ICylTolData : IDataUtilities<IPointCylTol>
    {
        ICylData AsCylData(TolType tolType);
        void ShiftByTheta(double thetaRad);

    }
}
