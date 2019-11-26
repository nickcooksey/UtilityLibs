using System;
 
using System.Windows.Media.Media3D;
namespace IDataLib
{
    public interface IColorMapBuilder<T> : IColorMapBase
    {
        void CreateAltitudeMap(double minToleranceValue, double maxToleranceValue, COLORCODE colorcode);
        GeometryModel3D DefineModel();
    }
}
