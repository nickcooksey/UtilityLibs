using IDataLib;
namespace DataLib.Ply
{
    /// <summary>
    /// contains property name and type
    /// </summary>
    public class PlyProperty : IPlyProperty
    {
        public string Name { get; set; }

        private string typeName;
        public string TypeName
        {
            get
            {
                return typeName;
            }
            set
            {
                typeName = value;
            }
        }
        public PlyPropertyType Type { get; set; }
        public bool IsList { get; set; }
        public string ListCountTypeName { get; set; }

        public PlyProperty()
        {
            Type = PlyPropertyType.other;
        }

    }
}
