using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{

    [System.Xml.Serialization.XmlInclude(typeof(Arc))]
    [System.Xml.Serialization.XmlInclude(typeof(Line3))]
    [System.Xml.Serialization.XmlInclude(typeof(Vector3))]
    public abstract class DwgEntity : IDwgEntity
    {
        public string LayerName { get; set; }
        public System.Drawing.Color Col { get; set; }
        public int ID { get; set; }

        protected bool byLayer;
        protected string lineTypeName;
        protected string lineWeightNumber;
        protected string hardPointerID;
        protected string colorNumber;
        protected string entityName;
        protected string acDbName;
        private int rgbColorNumber;

        public void ParseCommonDxf(List<string> fileSection)
        {
            try
            {
                for (int i = 0; i < fileSection.Count; i++)
                {
                    var line = fileSection[i].Trim();
                    if (line == "5")
                    {
                        string hexVal = fileSection[i + 1];
                        int value = Convert.ToInt32(hexVal, 16);
                        ID = value;
                    }
                    if (line == "6")
                    {
                        lineTypeName = fileSection[i + 1];
                    }
                    if (line == "8")
                    {
                        LayerName = fileSection[i + 1];
                    }
                    if (line == "62")
                    {
                        var c = Convert.ToInt32(fileSection[i + 1]);
                        colorNumber = c.ToString();
                    }
                    if (line == "370")
                    {
                        var lw = Convert.ToInt16(fileSection[i + 1]);
                        lineWeightNumber = lw.ToString();
                    }
                    if (line == "420")
                    {
                        var rgb = Convert.ToInt32(fileSection[i + 1]);
                        rgbColorNumber = rgb;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> DxfHeader()
        {
            try
            {
                List<string> contents = new List<string>();
                contents.Add("0");
                contents.Add(entityName);
                contents.Add("5");
                contents.Add(ID.ToString());
                contents.Add("330");
                contents.Add("1F");
                contents.Add("100");
                contents.Add("AcDbEntity");
                contents.Add("  8");
                contents.Add(LayerName);

                rgbColorNumber = ColorConverter.GetDxfCustomColorNumber(Col);
                contents.Add("420");
                contents.Add(rgbColorNumber.ToString());
                contents.Add("100");
                contents.Add(acDbName);
                return contents;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public abstract List<string> AsDXFString();


    }
}
