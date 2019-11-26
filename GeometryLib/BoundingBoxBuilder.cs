using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class BoundingBoxBuilder
    {
       
        public static IBoundingBox Union(IEnumerable<IBoundingBox> bbList)
        {
            BoundingBox ext = new BoundingBox();
            double xmax = double.MaxValue * -1;
            double ymax = double.MaxValue * -1;
            double zmax = double.MaxValue * -1;
            double xmin = double.MaxValue;
            double ymin = double.MaxValue;
            double zmin = double.MaxValue;
            foreach (IBoundingBox extent in bbList)
            {
                xmax = Math.Max(xmax, extent.Max.X);
                ymax = Math.Max(ymax, extent.Max.Y);
                zmax = Math.Max(zmax, extent.Max.Z);
                xmin = Math.Min(xmin, extent.Min.X);
                ymin = Math.Min(ymin, extent.Min.Y);
                zmin = Math.Min(zmin, extent.Min.Z);
            }
            ext.Max = new Vector3(xmax, ymax, zmax);
            ext.Min = new Vector3(xmin, ymin, zmin);
            return ext;
        }
        public static IBoundingBox CubeFromPtArray(List<IVector3> points)
        {
            try
            {
                var boundingBox = BoundingBoxBuilder.FromPtArray(points.ToArray());
                var center = boundingBox.Center;
                var width = boundingBox.Max.Minus(boundingBox.Min);
                double maxDim = .5 * Math.Max(Math.Abs(width.X), Math.Max(Math.Abs(width.Y), Math.Abs(width.Z)));

                var size = new Vector3(maxDim, maxDim, maxDim);
                boundingBox = new BoundingBox(center.Minus(size), center.Plus(size));
                return boundingBox;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static IBoundingBox GetSearchBox(IVector3 searchPoint, double searchRadius)
        {
            var searchBox = new BoundingBox(
                                          searchPoint.X - searchRadius,
                                          searchPoint.Y - searchRadius,
                                          searchPoint.Z - searchRadius,
                                          searchPoint.X + searchRadius,
                                          searchPoint.Y + searchRadius,
                                          searchPoint.Z + searchRadius);
            return searchBox;
        }
        public static IBoundingBox GetSearchCylinder(IBoundingBox boundingBox, IVector3 searchPoint, IVector3 axis, double searchRadius)
        {
            var searchBox = new BoundingBox(
                                          searchPoint.X - Math.Abs(boundingBox.Min.X * axis.X - searchRadius),
                                          searchPoint.Y - Math.Abs(boundingBox.Min.Y * axis.Y - searchRadius),
                                          searchPoint.Z - Math.Abs(boundingBox.Min.Z * axis.Z - searchRadius),
                                          searchPoint.X + Math.Abs(boundingBox.Max.X * axis.X + searchRadius),
                                          searchPoint.Y + Math.Abs(boundingBox.Max.Y * axis.Y + searchRadius),
                                          searchPoint.Z + Math.Abs(boundingBox.Max.Z * axis.Z + searchRadius));
            return searchBox;
        }
        public static IBoundingBox FromPtArray(IVector3[] pts)
        {
            var ext = new BoundingBox();

            double xmax = double.MaxValue * -1;
            double ymax = double.MaxValue * -1;
            double zmax = double.MaxValue * -1;
            double xmin = double.MaxValue;
            double ymin = double.MaxValue;
            double zmin = double.MaxValue;

            if (pts.Length > 0)
            {
                foreach (IVector3 pt in pts)
                {
                    xmax = Math.Max(xmax, pt.X);
                    ymax = Math.Max(ymax, pt.Y);
                    zmax = Math.Max(zmax, pt.Z);
                    xmin = Math.Min(xmin, pt.X);
                    ymin = Math.Min(ymin, pt.Y);
                    zmin = Math.Min(zmin, pt.Z);
                }

                ext.Max = new Vector3(xmax, ymax, zmax);
                ext.Min = new Vector3(xmin, ymin, zmin);

            }
            else
            {
                ext.Max = new Vector3(0, 0, 0);
                ext.Min = new Vector3(0, 0, 0);
            }

            return ext;
        }

    }
}
