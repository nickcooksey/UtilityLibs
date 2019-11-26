using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    public class PointCloudFile
    {
        static string asciiExtension = "asc";
        static string csvExtension = "csv";
        static string xyzExtension = "xyz";

        public static string Filter = "CSV Data files (*." + csvExtension + ")|*."
            + csvExtension + "|XYZ Data files (*." + xyzExtension + ")|*."
            + xyzExtension + "|All files(*.*)|*.*";
        public static bool Save(List<Vector3> points, List<Vector3> normals, string fileName)
        {
            List<string> file = new List<string>();
            if ((points != null) && (normals != null) && (fileName != null) && (fileName != ""))
            {
                int count = Math.Min(points.Count, normals.Count);
                for (int i = 0; i < count; i++)
                {
                    file.Add(points[i].X.ToString() + " " + points[i].Y.ToString() + " " + points[i].Z.ToString()
                            + " " + normals[i].X + " " + normals[i].Y + " " + normals[i].Z);
                }

                System.IO.File.WriteAllLines(fileName, file);
                return true;
            }
            else
            {
                return false;
            }

        }
        static public void SaveAsPlyFile(PointCloud pointCloud, string fileName)
        {
            PlyFile file = new PlyFile();
            for (int i = 0; i < pointCloud.Points.Count; i++)
            {
                Vector3 pt = pointCloud.Points[i];
                Vector3 norm = pointCloud.Normals[i];
                if (pt.Col == null)
                {
                    pt.Col = new PlyColor();
                }
                file.Vertices.Add(new PlyVertex(pt, norm, pt.Col));
            }
            file.Save(fileName);
        }
        public static bool Save(List<Vector3> points, string fileName)
        {
            List<string> file = new List<string>();
            if ((points != null) && (fileName != null) && (fileName != ""))
            {

                foreach (Vector3 pt in points)
                {
                    file.Add(pt.X.ToString() + " " + pt.Y.ToString() + " " + pt.Z.ToString());
                }

                System.IO.File.WriteAllLines(fileName, file);
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
