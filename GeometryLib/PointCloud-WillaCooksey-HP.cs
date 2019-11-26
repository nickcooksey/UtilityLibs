using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometryLib
{
    public class PointCloud
    {
        /// <summary>
        /// collection of 3d points
        /// </summary>
        public List<Vector3> Points { get { return points; } }
        public List<Vector3> Normals { get { return normals; } }
        public BoundingBox BoundingBox { get { return boundingBox; } }
        public bool ContainsNormals{ get { return containsNormals; } }

        List<Vector3> points;
        List<Vector3> normals;
        bool containsNormals;
        BoundingBox boundingBox;

        private BoundingBox getExtents()
        {
            boundingBox =  BoundingBoxBuilder.FromPtArray(points.ToArray());                        
            return boundingBox;
        }
        
        
        public PointCloud(List<PointCyl> pts)
        {
            points = new List<Vector3>();
            foreach (PointCyl pt in pts)
            {
                Vector3 pt3d = new Vector3(pt);
                Points.Add(pt3d);
            }
            containsNormals = false;
            boundingBox = getExtents();
        }
        public PointCloud(List<Vector3> pts)
        {
            points = pts;
            normals = new List<Vector3>();
            containsNormals = false;
            boundingBox = getExtents();
        }
        public PointCloud(List<Vector3> pts,List<Vector3> norms)
        {
            points = pts;
            normals = norms;
            containsNormals = true;
            boundingBox = getExtents();
        }
       

    }

}
