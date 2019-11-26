using GeometryLib;
using IDataLib;
using IGeometryLib;

namespace DataLib.Ply
{
    /// <summary>
    /// contains ply vertex, normal,color
    /// </summary>
    public class PlyVertex : Vector3, IPlyVertex
    {


        public IVector3 Normal
        {
            get { return _normal; }
        }
        public bool UsedInFace { get; set; }
        public bool ContainsColor { get; set; }
        public bool ContainsNormal
        {
            get { return _containsNormal; }
        }

        private int _normalCount;
        private IVector3 _normal;
        private bool _containsNormal;

        public void AddNormal(IVector3 newNormal)
        {
            if (newNormal.Length != 0)
            {
                _normalCount++;
                _containsNormal = true;
            }
            _normal = _normal.Plus(newNormal);
        }
        public PlyVertex()
        {
            ContainsColor = false;
            _containsNormal = false;

            _normal = new Vector3();

        }
        public PlyVertex(IVector3 vert)
        {
            ContainsColor = true;
            _containsNormal = false;
            X = vert.X;
            Y = vert.Y;
            Z = vert.Z;
            ID = vert.ID;
            _normal = new Vector3();
            Col = vert.Col;

        }
        public PlyVertex(IVector3 vert, int id)
        {
            ContainsColor = true;
            _containsNormal = false;
            X = vert.X;
            Y = vert.Y;
            Z = vert.Z;
            ID = id;
            _normal = new Vector3();
            Col = vert.Col;

        }
        public PlyVertex(IVector3 vert, IVector3 normal)
        {
            ContainsColor = true;
            _containsNormal = true;
            _normalCount++;
            X = vert.X;
            Y = vert.Y;
            Z = vert.Z;
            _normal = normal;
            Col = vert.Col;

        }
        public PlyVertex(IVector3 vert, IVector3 normal, System.Drawing.Color color)
        {
            ContainsColor = true;
            _containsNormal = true;
            _normalCount++;
            X = vert.X;
            Y = vert.Y;
            Z = vert.Z;
            _normal = normal;
            Col = color;
        }

    }
}
