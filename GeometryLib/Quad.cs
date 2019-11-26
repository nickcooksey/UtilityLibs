namespace GeometryLib
{
    public class Quad
    {
        public Vector3[] Vertices { get { return _verts; } }
        // public Vector3 Normal { get { return _normal; } }
        public int Index { get { return _index; } }

        private Vector3[] _verts;

        //Vector3 _normal;
        private int _index;

        public Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
        {
            _verts = new Vector3[4];
            _verts[0] = p1;
            _verts[1] = p2;
            _verts[2] = p3;
            _verts[3] = p4;
            _index = 0;
        }
    }
}
