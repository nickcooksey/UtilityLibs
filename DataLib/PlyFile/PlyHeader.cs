using IDataLib;
using System.Collections.Generic;
namespace DataLib.Ply
{
    /// <summary>
    /// contains header info for ply file
    /// </summary>
    public class PlyHeader : IPlyHeader
    {
        public PlyHeader()
        {
            Elements = new List<IPlyElement>();
        }
        public IList<IPlyElement> Elements { get; set; }
    }
}
