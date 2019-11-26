using IDataLib;
namespace DataLib.Ply
{
    /// <summary>
    /// contains ply edge vertices
    /// </summary>
    public class PlyEdge : IPlyEdge
    {
        public int Vertex1 { get; set; }
        public int Vertex2 { get; set; }
        public System.Drawing.Color Color { get; set; }
        public bool ContainsColor { get; set; }
        public PlyEdge(bool containsColorIn)
        {
            ContainsColor = containsColorIn;
            if (ContainsColor)
                Color = System.Drawing.Color.White;

        }
    }
}
