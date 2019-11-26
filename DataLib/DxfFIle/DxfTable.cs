using System;
using System.Collections.Generic;

namespace DataLib
{
    internal class DxfTable
    {
        private List<DxfTableRecord> Records;
        private string tableName;//2
        private string handle;//5
        private string objectType;//0
        private string subClassMarker;//100
        private string hardOwnerID;//360
        internal DxfTable(DxfTableTypeEnum tableName)
        {
            objectType = "TABLE";
            this.tableName = tableName.ToString();
            Records = new List<DxfTableRecord>();
        }
        internal void AddRecord(DxfTableRecord record)
        {
            Records.Add(record);
        }
        internal virtual List<string> Header()
        {
            try
            {
                var content = new List<string>();
                content.Add("  0");
                content.Add(objectType);
                content.Add("  2");
                content.Add(tableName);
                content.Add("  5");
                content.Add(handle);
                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal List<string> AsString()
        {
            var content = new List<string>();
            content.AddRange(Header());
            if (tableName == "LAYER")
            {
                content.Add("102");
                content.Add("{ACAD_XDICTIONARY");
                content.Add("360");
                content.Add(hardOwnerID);
                content.Add("102");
                content.Add("}");
            }
            foreach (DxfTableRecord record in Records)
            {
                content.AddRange(record.AsString());
            }
            content.Add("  0");
            content.Add("ENDTAB");

            return content;
        }
    }
    internal class DxfTableRecord
    {
        protected string generalSubclassMarker;
        protected string subclassMarker;
        protected string entityType;
        protected string handle;
        protected string softPointerID;
        public List<string> Header()
        {
            try
            {
                var content = new List<String>();
                content.Add("  0");
                content.Add(entityType);
                content.Add("  5");
                content.Add(handle);
                content.Add("330");
                content.Add(softPointerID);
                content.Add("100");
                content.Add(generalSubclassMarker);
                content.Add("100");
                content.Add(subclassMarker);
                content.Add("  2");
                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal DxfTableRecord()
        {
            generalSubclassMarker = "AcDbSymbolTableRecord";

        }
        internal virtual List<string> AsString()
        {
            var result = new List<String>();

            return result;
        }
    }
    internal class DxfLayerRecord : DxfTableRecord
    {
        internal string LayerName { get; private set; }

        private static int layerCount;
        private string flag;//70
        private string colorNumber;//62
        private string lineTypeName;//6
        private string lineWeight;//370
        private string hardPointerIDPlotStyle;//390
        private string hardPointerIDMaterial;//347
        internal DxfLayerRecord()
        {
            layerCount++;
            LayerName = "layer" + layerCount.ToString();
            flag = "    0";
            colorNumber = "1";
            lineTypeName = "Continuous";
            lineWeight = "    30";
        }
        internal DxfLayerRecord(string layerName, int colorNumber, LineTypeEnum lineTypeName, int lineWeight)
        {
            layerName.Trim();
            layerName.Replace(" ", "_");
            this.LayerName = layerName;

            this.lineTypeName = lineTypeName.ToString();

            if (colorNumber < 1)
            {
                colorNumber = 3;
            }
            if (colorNumber > 248)
            {
                colorNumber = 3;
            }
            this.colorNumber = colorNumber.ToString();

            if (lineWeight < 5)
            {
                lineWeight = 5;
            }
            if (lineWeight > 80)
            {
                lineWeight = 80;
            }
            this.lineWeight = lineWeight.ToString();
        }

        internal override List<string> AsString()
        {
            try
            {
                var content = new List<String>();
                content.AddRange(Header());

                content.Add(LayerName);
                content.Add(" 70");
                content.Add(flag);
                content.Add(" 62");
                content.Add(colorNumber);
                content.Add(" 6");
                content.Add(lineTypeName);
                content.Add("370");
                content.Add(lineWeight);
                content.Add("390");
                content.Add(hardPointerIDPlotStyle);
                content.Add("347");
                content.Add(hardPointerIDMaterial);
                return content;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
    internal enum LineTypeEnum
    {
        CONTINUOUS,
        DASH
    }
    internal class DxfLineType
    {
        internal LineTypeEnum LineTypeEnum { get; private set; }
        internal DxfLineType(LineTypeEnum typeEnum)
        {
            this.LineTypeEnum = typeEnum;
        }
        public override string ToString()
        {
            switch (LineTypeEnum)
            {
                case LineTypeEnum.DASH:
                    return "Dash";
                default:
                case LineTypeEnum.CONTINUOUS:
                    return "Continuous";
            }
        }
    }
    internal enum DxfTableTypeEnum
    {
        LAYER,
        LTYPE
    }
    internal class DxfLinetypeRecord : DxfTableRecord
    {
        private string lineTypeName;//2
        private string flag;//70
        private string description;//3
        private string alignmentCode;//72 always 65
        private string elementCount;//73
        private string patternLen;//40
        private string dashLength;//49
        private string complexType;//74
        private string shapeNumber;//75
        private string pointerStyle;//350
        private string scale;//46
        private DxfLineType lineType;
        internal DxfLinetypeRecord(LineTypeEnum dxfLineType)
        {
            switch (dxfLineType)
            {
                case LineTypeEnum.CONTINUOUS:
                    InitContinuous();
                    break;
                case LineTypeEnum.DASH:
                    InitDash();
                    break;
            }
        }
        internal DxfLinetypeRecord()
        {
            InitContinuous();
        }
        private void InitDash()
        {
            Init();
            lineTypeName = "Dashed";
            flag = "     0";
            description = "Dashed Line";
            alignmentCode = "    65";
            elementCount = "      0";
            patternLen = "0.0";
            lineType = new DxfLineType(LineTypeEnum.DASH);
        }
        private void InitContinuous()
        {
            Init();
            lineTypeName = "Continuous";
            flag = "     0";
            description = "Solid Line";
            alignmentCode = "    65";
            elementCount = "      0";
            patternLen = "0.0";
            lineType = new DxfLineType(LineTypeEnum.CONTINUOUS);
        }
        private void Init()
        {
            subclassMarker = "AcDbLinetypeTableRecord";
            entityType = "LTYPE";
            softPointerID = "5";
        }
        internal override List<string> AsString()
        {
            try
            {
                var content = new List<String>();
                content.AddRange(Header());


                content.Add(lineTypeName);
                content.Add(" 70");
                content.Add(flag);
                content.Add("  3");
                content.Add(description);
                content.Add(" 72");
                content.Add(alignmentCode);
                content.Add(" 73");
                content.Add(elementCount);
                content.Add(" 40");
                content.Add(patternLen);

                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
