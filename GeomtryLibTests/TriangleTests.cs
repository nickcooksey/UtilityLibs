using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GeometryLibTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void triangle_containsvertices()
        {
            var v0 = new Vector3(2, 1, 0.5);
            var v1 = new Vector3(4, 2, 0.5);
            var v2 = new Vector3(2, 4, 0.5);
            Triangle tri = new Triangle(v0, v1, v2);
            bool b0 = tri.Contains(v0);
            Assert.IsTrue(b0);
            bool b1 = tri.Contains(v1);
            Assert.IsTrue(b1);
            bool b2 = tri.Contains(v1);
            Assert.IsTrue(b2);
        }
        [TestMethod]
        public void triangle_IntersectsTriPlane_testOK()
        {
            IVector3 v0 = new Vector3(2, 1, 0.5);
            IVector3 v1 = new Vector3(4, 2, 0.5);
            IVector3 v2 = new Vector3(2, 4, 0.5);
            IVector3 pt = new Vector3(4, 4, 1);
            IVector3 dir = new Vector3(0, 0, -1);
            var ray = new Ray3(pt, dir);
            Triangle tri = new Triangle(v0, v1, v2);
            IIntersection ir = tri.IntersectsTriPlane(ray);
            Assert.IsTrue(ir.Intersects);
            Assert.AreEqual(4.0, ir.Point.X, .001);
            Assert.AreEqual(4.0, ir.Point.Y, .001);
            Assert.AreEqual(0.5, ir.Point.Z, .001);
            IIntersection irt = tri.IntersectedBy(ray);
            Assert.IsFalse(irt.Intersects);
        }
        [TestMethod]
        public void triangle_IntersectsTriPlane_testFalseOK()
        {
            var v0 = new Vector3(2, 1, 0);
            var v1 = new Vector3(4, 2, 0);
            var v2 = new Vector3(2, 4, 0);
            var pt = new Vector3(4, 4, 1);
            var dir = new Vector3(1, 1, 0);
            var ray = new Ray3(pt, dir);
            Triangle tri = new Triangle(v0, v1, v2);
            IIntersection ir = tri.IntersectsTriPlane(ray);
            Assert.IsFalse(ir.Intersects);
        }
        [TestMethod]
        public void triangle_intersectedByTest_testOK()
        {
            IVector3 v0 = new Vector3(2, 1, 0);
            IVector3 v1 = new Vector3(4, 2, 0);
            IVector3 v2 = new Vector3(2, 4, 0);
            IVector3 pt = new Vector3(3, 2, 1);
            IVector3 dir = new Vector3(0, 0, -1);
            var ray = new Ray3(pt, dir);
            Triangle tri = new Triangle(v0, v1, v2);
            IIntersection ir = tri.IntersectedBy(ray);
            Assert.IsTrue(ir.Intersects);
            Assert.AreEqual(3.0, ir.Point.X, .001);
            Assert.AreEqual(2.0, ir.Point.Y, .001);
            Assert.AreEqual(0.0, ir.Point.Z, .001);
        }
        [TestMethod]
        public void triangle_containsPt_testOK()
        {
            var v0 = new Vector3(2, 1, 0);
            var v1 = new Vector3(4, 2, 0);
            var v2 = new Vector3(2, 4, 0);
            var pt = new Vector3(3, 2, 0);
            Triangle tri = new Triangle(v0, v1, v2);
            bool test = tri.Contains(pt);
            Assert.IsTrue(test);
        }
        [TestMethod]
        public void triangle_asPointGrid_gridOK()
        {
            var v0 = new Vector3(2, 1, 0);
            var v1 = new Vector3(4, 2, 0);
            var v2 = new Vector3(2, 4, 0);

            Triangle tri = new Triangle(v0, v1, v2);
            List<IVector3> points = tri.AsPointGrid(.1);
            bool areContained = false;
            foreach (IVector3 pt in points)
            {
                areContained = tri.Contains(pt);
                Assert.IsTrue(areContained);
            }
            IBoundingBox bb = BoundingBoxBuilder.FromPtArray(points.ToArray());
            Assert.AreEqual(4.0, bb.Max.X, .001);
            Assert.AreEqual(1.0, bb.Min.Y, .001);
            Assert.AreEqual(4.0, bb.Max.Y, .001);
            Assert.IsTrue(areContained);
        }
        [TestMethod]
        public void triangle_asPointGrid_countOK()
        {
            var v0 = new Vector3(5, 0, 0);
            var v1 = new Vector3(0, 5, 0);
            var v2 = new Vector3(5, 5, 0);

            Triangle tri = new Triangle(v0, v1, v2);
            List<IVector3> points = tri.AsPointGrid(.1);
            bool areContained = false;
            foreach (Vector3 pt in points)
            {
                areContained = tri.Contains(pt);
                Assert.IsTrue(areContained);
            }
            IBoundingBox bb = BoundingBoxBuilder.FromPtArray(points.ToArray());
            Assert.AreEqual(1300, points.Count, 40);
            Assert.AreEqual(5.0, bb.Max.X, .001);
            Assert.AreEqual(0.0, bb.Min.Y, .001);
            Assert.AreEqual(5.0, bb.Max.Y, .001);
            Assert.IsTrue(areContained);
        }
    }
}
