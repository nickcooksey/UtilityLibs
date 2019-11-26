using IDataLib;
using System.Collections.Generic;

namespace DataLib.Ply
{
    public class PlyElement : IPlyElement
    {
        public PlyElementType Type { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return Type.ToString();
            }
            set
            {
                name = value;
            }
        }
        public bool ContainsVertex { get; set; }
        public bool ContainsNormal { get; set; }
        public bool ContainsColor { get; set; }
        public int Count { get; set; }
        public List<IPlyProperty> Properties { get; private set; }
        public void AddProperty(IPlyProperty property)
        {
            Properties.Add(property);
            //Properties.Add(property.Name, property);
        }

        public PlyElement()
        {
            Properties = new List<IPlyProperty>();
            Type = PlyElementType.other;
        }
    }
}
