using IGeometryLib;
using System.Collections.Generic;

namespace IDataLib
{
   
    
   

  
 
    
    

    
    
    public interface IPlyFile
    {
        IPlyHeader Header { get; set; }
        PlyFormat Format { get; set; }
        List<IPlyVertex> Vertices { get; }
        List<IPlyFace> Faces { get; }
        List<IPlyEdge> Edges { get; }

        IVector3[] FaceNorms { get; set; }
        void BuildFromGrid2(ICartGridData pointStripList);
        void SaveAscii(string fileName);
    }
}
