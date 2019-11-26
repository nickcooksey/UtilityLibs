using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections.Generic;

namespace DataLib
{
    public class DxfFile : IDxfFile
    {

        public string Filename { get; private set; }
        public static string Filter = "DXF files (*.dxf)|*.dxf|All files (*.*)|*.*";
      
        public List<IDwgEntity> Entities { get; private set; }

        private DxfHeaderSection headerSection;
        private DxfClasssesSection classesSection;
        private DxfTablesSection tablesSection;
        private DxfBlocksSection blocksSection;
        private DxfEntitiesSection entitiesSection;
        private DxfObjectsSection objectsSection;

        public DxfFile()
        {
            Entities = new List<IDwgEntity>();
           
        }
        public DxfFile(List<IDwgEntity> entities)
        {
            this.Entities = entities;
           

        }

        public IDisplayData AsDisplayData(double segmentLength)
        {
            try
            {
                var pointList = AsPointList(segmentLength);
                var dd = new DisplayData();
                foreach (var pt in pointList)
                {
                    dd.Add(new System.Drawing.PointF((float)pt.X, (float)pt.Y));
                }
                return dd;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<IVector3> AsPointList(double segmentLength)
        {
            try
            {
                var ptList = new List<IVector3>();
                foreach (IDwgEntity e in Entities)
                {
                    if (e is Line3 line)
                    {
                        double delta = segmentLength / line.Length;
                        int count = (int)Math.Round(1.0 / delta);
                        for (int i = 0; i < count; i++)
                        {
                            double x = line.Point1.X + (line.Point2.X - line.Point1.X) * delta * i;
                            double y = line.Point1.Y + (line.Point2.Y - line.Point1.Y) * delta * i;
                            double z = line.Point1.Z + (line.Point2.Z - line.Point1.Z) * delta * i;
                            ptList.Add(new Vector3(x, y, z));
                        }
                    }
                    if (e is Arc arc)
                    {
                        double delta = segmentLength / arc.Length;
                        double dAng = arc.SweepAngleRad * delta;
                        int count = (int)Math.Round(1.0 / delta);
                        double startAngle = Math.Min(arc.StartAngleRad, arc.EndAngleRad);
                        for (int i = 0; i < count; i++)
                        {
                            double x = arc.Center.X + arc.Radius * Math.Cos(startAngle + (i * dAng));
                            double y = arc.Center.Y + arc.Radius * Math.Sin(startAngle + (i * dAng));
                            double z = arc.Center.Z;
                            ptList.Add(new Vector3(x, y, z));
                        }
                    }
                }
                var xlist = new List<double>();
                foreach (IVector3 pt in ptList)
                {
                    xlist.Add(pt.X);
                }
                var ptArr = ptList.ToArray();
                Array.Sort(xlist.ToArray(), ptArr);
                ptList.Clear();
                ptList.AddRange(ptArr);
                return ptList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public void Save(string fileName, IProgress<int> progress)
        {
            try
            {
                Save(fileName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Save(string fileName)
        {
            try
            {
                int totalCount = Entities.Count;
                Filename = fileName;
                if (fileName != null && fileName != "")
                {
                    var file = BuildFile();
                    System.IO.File.WriteAllLines(fileName, file.ToArray());
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void BuildFromFile(string fileName)
        {
            try
            {
                Filename = fileName;
                var stringList = FileIOLib.FileIO.ReadTextFile(fileName);
                Entities = Parse(stringList);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// parses dxf file into list of DwgEntity objects
        /// </summary>
        /// <returns></returns>
        private List<IDwgEntity> Parse(List<string> text)
        {
            try
            {
                var entities = new List<IDwgEntity>();
                List<string> fileSection = new List<string>();
                for (int i = 0; i < text.Count; i++)
                {
                    string str = text[i].Trim();
                    if (str == "POINT")
                    {
                        var point = new Vector3();
                        point.ParseDXF(GetFileSection(text, i, 16));
                        entities.Add(point);
                    }
                    if (str == "LINE")
                    {
                        var line = new Line3();
                        line.ParseDxf(GetFileSection(text, i, 22));
                        entities.Add(line);
                    }
                    if (str == "CIRCLE")
                    {
                        var circ = new Arc();
                        circ.ParseCircleDxf(GetFileSection(text, i, 18));
                        entities.Add(circ);
                    }
                    if (str == "ARC")
                    {
                        var arc = new Arc();
                        arc.ParseArcDxf(GetFileSection(text, i, 24));
                        entities.Add(arc);
                    }

                }
                return entities;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private List<string> GetFileSection(List<string> file, int startingIndex, int sectionLength)
        {
            try
            {
                List<string> section = new List<string>();
                int index = startingIndex;
                while (index < file.Count && index < startingIndex + sectionLength)
                {
                    section.Add(file[index++]);
                }
                return section;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private List<string> BuildFile()
        {
            try
            {
                var content = new List<string>();

                content.AddRange(BuildEntities());

                content.Add("0");
                content.Add("EOF");
                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildHeader()
        {
            try
            {
                headerSection = new DxfHeaderSection();

                return headerSection.AsString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildClasses()
        {
            try
            {
                classesSection = new DxfClasssesSection();
                return classesSection.AsString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildTables()
        {
            try
            {
                tablesSection = new DxfTablesSection();
                var lineTable = new DxfTable(DxfTableTypeEnum.LTYPE);
                var lineType = new DxfLinetypeRecord();
                lineTable.AddRecord(lineType);

                var layerTable = new DxfTable(DxfTableTypeEnum.LAYER);
                var layer = new DxfLayerRecord("layer1", 25, LineTypeEnum.CONTINUOUS, 20);
                layerTable.AddRecord(layer);
                tablesSection.AddTable(lineTable);
                tablesSection.AddTable(layerTable);

                return tablesSection.AsString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildBlocks()
        {
            try
            {
                blocksSection = new DxfBlocksSection();
                return blocksSection.AsString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildEntities()
        {
            try
            {
                entitiesSection = new DxfEntitiesSection();
                foreach (DwgEntity entity in Entities)
                {
                    entitiesSection.AddEntity(entity);
                }
                return entitiesSection.AsString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> BuildObjects()
        {
            try
            {
                objectsSection = new DxfObjectsSection();
                return objectsSection.AsString();
            }
            catch (Exception)
            {

                throw;
            }
        }

       public IBoundingBox BoundingBox()
        {

            var bbList = new List<IBoundingBox>();
            foreach (DwgEntity entity in Entities)
            {
                if (entity is Line3)
                {
                    Line3 line = entity as Line3;
                    bbList.Add(line.BoundingBox());
                }
                if (entity is Arc)
                {
                    Arc arc = entity as Arc;
                    bbList.Add(arc.BoundingBox());
                }
                if (entity is Vector3)
                {
                    Vector3 pt = entity as Vector3;
                    bbList.Add(pt.BoundingBox());
                }
            }
            return BoundingBoxBuilder.Union(bbList);
        }


    }
}
