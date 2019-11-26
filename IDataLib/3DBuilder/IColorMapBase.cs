using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
namespace IDataLib
{
    public interface IColorMapBase
    {
        double ScaledMaxValue { get; }
        double ScaledMinValue { get; }
        double BlueValue { get; }
        double AquaValue { get; }
        double GreenValue { get; }
        double YellowValue { get; }
        double RedValue { get; }
        int XInCount { get; }
        int ZInCount { get; }
        double DxInput { get; }
        double DxTarget { get; }
        double DzInput { get; }
        double DzTarget { get; }
        int XIndexMin { get; }
        int XIndexMax { get; }
        WriteableBitmap AltitudeMap { get; }
        int ZIndexMin { get; }
        int ZIndexMax { get; }
        double Texture_xscale { get; }
        double Texture_zscale { get; }
        double GetHeight(int xIndex, int zIndex);
    }
}
