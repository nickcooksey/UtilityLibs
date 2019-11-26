using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeometryLibTests
{
    [TestClass]
    public class GeomUtilTests
    {

        [TestMethod]
        public void GeometryUtil_breakMany_pointsOK()
        {
            Vector3 pt1 = new Vector3(6, 8, 1);
            Vector3 pt2 = new Vector3(3, 4, 1);
            var line = new Line3(pt1, pt2);
            double len = pt1.DistanceTo(pt2);
            double spacing = .1;
            int count = (int)(Math.Round(len / spacing)) + 1;
            List<IVector3> points = line.BreakMany(spacing);
            Assert.AreEqual(count, points.Count);
            double seglen = points[0].DistanceTo(points[1]);
            Assert.AreEqual(spacing, seglen, .01);
        }
    }
}
