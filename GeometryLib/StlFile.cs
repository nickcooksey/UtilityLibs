using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    public class StlFile:List<STLTriangle> 
    {
        public Extents Extents 
        { 
            get 
            { 
                return _extents;
            } 
        }
        DrawingIO.Extents _extents;
        List<DrawingIO.Vector3> points;

        public StlFile(List<STLTriangle> triangles)
        {
            this.AddRange(triangles);
            getExtents();
        }
        void getExtents()
        {
            points = new List<DrawingIO.Vector3>();
            foreach (DrawingIO.STLTriangle tri in this)
            {
                points.AddRange(tri.Vertices);
            }
            _extents = ExtentsBuilder.FromPtArray(points.ToArray());            
        }

    }
    /// <summary>
    /// read write functions for STL files 
    /// </summary>
    public class StlFileParser
    {
        /// <summary>
        /// open binary or ascii file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>list of triangles</returns>
        static public List<STLTriangle> Open(string fileName)
        {
            List<STLTriangle> triangles = new List<STLTriangle>();
            string solid = "solid";
            bool isAscii = false;
            int count = 80;
            int start = 0;
            char[] buffer = new char[80];
            string bufferStr = "";

            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
            try
            {
                sr.Read(buffer, start, count);
                sr.Close();
                foreach (char c in buffer)
                {
                    if (char.IsLetter(c))
                    {
                        bufferStr += c.ToString();
                    }
                    bufferStr = bufferStr.ToUpper();
                    isAscii = bufferStr.Contains(solid);
                    if (isAscii)
                    {
                        triangles = openAscii(fileName);
                    }
                    else
                    {
                        triangles = openBinary(fileName);
                    }
                }
                return triangles;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return triangles;
            }
        }
       
        static private List<STLTriangle> openAscii(string fileName)
        {
            List<string> file = openTextFile(fileName);
            List<STLTriangle> triangles = new List<STLTriangle>();
            try
            {
                
                UInt32 triIndex = 0;
                for (int i = 0; i < file.Count; i++)
                {
                    if (file[i].Contains("normal"))
                    {
                        triangles.Add(new STLTriangle(parseVertex(file[i + 2]), parseVertex(file[i + 3]), parseVertex(file[i + 4]), parseVector(file[i]), triIndex));
                        triIndex++;
                    }                   
                }
                int triCount = triangles.Count;
                return triangles;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return triangles;
            }
        }
        static private List<string> openTextFile(string fileName)
        {
            List<string> file = new List<string>();
            if (fileName != null && fileName != "" & System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        file.Add(line);
                    }
                }
            }

            return file;
        }
        static string[] splitter = new string[1] { " " };
        static private Vector3 parseVertex(string line)
        {

            string[] words = line.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            double x = double.Parse(words[1]);
            double y = double.Parse(words[2]);
            double z = double.Parse(words[3]);
            Vector3 ptOut = new Vector3(x, y, z);
            return ptOut;
        }
        static private Vector3 parseVector(string line)
        {
            string[] words = line.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            double x = double.Parse(words[2]);
            double y = double.Parse(words[3]);
            double z = double.Parse(words[4]);
            Vector3 vOut = new Vector3(x, y, z);
            return vOut;
        }

        static private List<STLTriangle> openBinary(string fileName)
        {
            UInt32 triCountBin = 0;
            List<STLTriangle> triangles = new List<STLTriangle>();
            try
            {
                if(fileName!=null && fileName!="" & System.IO.File.Exists(fileName))
                {
                using (System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(fileName, System.IO.FileMode.Open)))
                {
                    br.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                    char[] header = new char[80];
                    UInt32 triIndex = 0;             
                    header = br.ReadChars(80);
                    triCountBin = br.ReadUInt32();

                       while (triIndex < triCountBin && br.BaseStream.Position < br.BaseStream.Length)
                       {
                            double n1 = br.ReadSingle();
                            double n2 = br.ReadSingle();
                            double n3 = br.ReadSingle();
                            double v1x = br.ReadSingle();
                            double v1y = br.ReadSingle();
                            double v1z = br.ReadSingle();
                            double v2x = br.ReadSingle();
                            double v2y = br.ReadSingle();
                            double v2z = br.ReadSingle();
                            double v3x = br.ReadSingle();
                            double v3y = br.ReadSingle();
                            double v3z = br.ReadSingle();
                            UInt16 attr = br.ReadUInt16();
                            Vector3 norm = new Vector3(n1, n2, n3);
                            Vector3 p1 = new Vector3(v1x, v1y, v1z);
                            Vector3 p2 = new Vector3(v2x, v2y, v2z);
                            Vector3 p3 = new Vector3(v3x, v3y, v3z);
                            triangles.Add(new STLTriangle(p1, p2, p3, triIndex));
                            triIndex++;                            
                       }
                   }
                }
                return triangles;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return triangles;
            }
        }

        /// <summary>
        /// save list of Triangles to STL file
        /// </summary>
        /// <param name="triangles"></param>
        /// <param name="fileName"></param>
        static public void SaveAscii(List<STLTriangle> triangles, string fileName)
        {
            List<string> file = new List<string>();
            if (triangles.Count > 0)
            {
                file.Add("solid ascii");
                foreach (STLTriangle tri in triangles)
                {
                    file.Add("facet normal " + tri.Normal.X.ToString("f7") + " "
                        + tri.Normal.Y.ToString("f7") + " " + tri.Normal.Z.ToString("f7"));
                    file.Add("outer loop");
                    file.Add("vertex " + tri.Vertices[0].X.ToString("f7") + " " +
                        tri.Vertices[0].Y.ToString("f7") + " " + tri.Vertices[0].Z.ToString("f7"));
                    file.Add("vertex " + tri.Vertices[1].X.ToString("f7") + " " +
                        tri.Vertices[1].Y.ToString("f7") + " " + tri.Vertices[1].Z.ToString("f7"));
                    file.Add("vertex " + tri.Vertices[2].X.ToString("f7") + " " +
                         tri.Vertices[2].Y.ToString("f7") + " " + tri.Vertices[2].Z.ToString("f7"));
                    file.Add("end loop");
                    file.Add("endfacet");
                }
                file.Add("end solid");
                System.IO.File.WriteAllLines(fileName, file);
            }
        }
        /// <summary>
        /// save list of Trangles to STL ASCII file
        /// </summary>
        /// <param name="triangles"></param>
        /// <param name="fileName"></param>
        static public void SaveBinary(List<STLTriangle> triangles, string fileName)
        {
            try
            {
                string headerstr = "binary stl file";
                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Open(fileName, System.IO.FileMode.Create)))
                {
                    char[] header = headerstr.ToCharArray(0, 80);
                    bw.Write(header, 0, 80);
                    UInt32 tricB = (UInt32)triangles.Count;
                    bw.Write(tricB);
                    foreach (STLTriangle tri in triangles)
                    {
                        bw.Write((float)tri.Normal.X);
                        bw.Write((float)tri.Normal.Y);
                        bw.Write((float)tri.Normal.Z);
                        bw.Write((float)tri.Vertices[0].X);
                        bw.Write((float)tri.Vertices[0].Y);
                        bw.Write((float)tri.Vertices[0].Z);
                        bw.Write((float)tri.Vertices[1].X);
                        bw.Write((float)tri.Vertices[1].Y);
                        bw.Write((float)tri.Vertices[1].Z);
                        bw.Write((float)tri.Vertices[2].X);
                        bw.Write((float)tri.Vertices[2].Y);
                        bw.Write((float)tri.Vertices[2].Z);
                        bw.Write((UInt16)0);

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }       
  
    }
}
