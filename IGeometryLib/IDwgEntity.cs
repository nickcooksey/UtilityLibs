using System.Collections.Generic;

namespace IGeometryLib
{
    public interface IDwgEntity
    {
        string LayerName { get; set; }
        System.Drawing.Color Col { get; set; }
        int ID { get; set; }
        void ParseCommonDxf(List<string> fileSection);
        List<string> DxfHeader();
        List<string> AsDXFString();
    }
}
