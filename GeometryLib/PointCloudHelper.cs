using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DrawingIO
{
    public enum MeshType
    {
        STL,
        PLY,
        POINTCLOUD
    }
    public class PointCloudHelper
    {
        public static List<Vector3> ConvertCylToXYZ(List<PointCyl> inputData)
        {
            List<Vector3> output = new List<Vector3>();
            foreach (PointCyl d in inputData)
            {
                Vector3 pt = new Vector3(d.R * Math.Cos(d.ThetaRad), d.R * Math.Sin(d.ThetaRad), d.Z, d.Col);
                output.Add(pt);
            }
            return output;
        }
        public static PointCloud UnrollCylinder(List<PointCyl> ptsIn, double rNominal, double rScaling)
        {
            var ptsOut = new List<Vector3>();
            var norms = new List<Vector3>();
            foreach (PointCyl pt in ptsIn)
            {
                double th = pt.ThetaRad;
                while (th < 0)
                {
                    th += 2 * Math.PI;
                }
                th %= (2 * Math.PI);
                double x = rNominal * th;
                double y = pt.R * -rScaling;
                double z = pt.Z;
                ptsOut.Add(new Vector3(x, y, z, pt.Col));
            }
            var pcOut = new PointCloud(ptsOut);
            return Translate(pcOut, new Vector3(-1 * pcOut.Extents.Center().X, -1 * pcOut.Extents.Center().Y, -1 * pcOut.Extents.Center().Z));
        }
        public static PointCloud Translate(PointCloud pc, Vector3 translation)
        {
            var ptsOut = new List<Vector3>();
            foreach (Vector3 pt in pc.Points)
            {
                Vector3 ptOut = pt + translation;
                ptsOut.Add(ptOut);
            }
            if (pc.ContainsNormals)
            {
                return new PointCloud(ptsOut, pc.Normals);
            }
            else
            {
                return new PointCloud(ptsOut);
            }
           
        }
    }
    
}
