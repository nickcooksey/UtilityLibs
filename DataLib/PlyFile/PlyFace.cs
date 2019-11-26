using IDataLib;
using System.Collections.Generic;
namespace DataLib.Ply
{
    /// <summary>
    /// contains ply Face indices
    /// </summary>
    public class PlyFace : IPlyFace
    {
        public List<int> Indices { get { return _indices; } }

        private List<int> _indices;

        public PlyFace(List<int> indices)
        {
            _indices = indices;
        }
        public PlyFace(GeometryLib.Triangle tri)
        {
            _indices = new List<int>();
            _indices.Add(tri.Vertices[0].ID);
            _indices.Add(tri.Vertices[1].ID);
            _indices.Add(tri.Vertices[2].ID);
        }
    }
}
