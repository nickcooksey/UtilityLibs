using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
namespace IDataLib
{
    public interface IModel3DBuilder
    {
        string GreenValue { get; }
        string RedValue { get; }
        string YellowValue { get; }
        string AquaValue { get; }
        string BlueValue { get; }
        void BuildModel(ref Model3DGroup modelgroup, ICylGridData data, double radialDirection, double nominalRadius,
     double minToleranceValue, double maxToleranceValue, double scalingFactor, COLORCODE colorCode);
        void BuildModel(ref Model3DGroup modelgroup, ICartGridData data,
               double minToleranceValue, double maxToleranceValue, double scalingFactor, COLORCODE colorCode);
    }
    

  

}
