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
