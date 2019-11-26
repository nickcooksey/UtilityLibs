using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GeometryLibTests
{
    [TestClass]
    public class ArcTests
    {
        [TestMethod]
        public void Arc_defConst_returnsObj()
        {
            Arc arc = new Arc();

            arc.Radius = 1;
            arc.Center = new Vector3(1, 1, 1);

            arc.StartAngleRad = 0;
            arc.EndAngleRad = Math.PI / 2;
            double endx = arc.EndPoint.X;
            double endy = arc.EndPoint.Y;
            double endz = arc.EndPoint.Z;
            double sweep = arc.SweepAngleDeg;
            Assert.AreEqual(90, sweep, "sweep");
            Assert.AreEqual(1, endx);
            Assert.AreEqual(2, endy);
            Assert.AreEqual(1, endz);
            Assert.AreEqual(Math.PI / 2, arc.Length);

        }
        [TestMethod]
        public void Arc_3ptconst_returnsObj()
        {
            Vector3 pt1 = new Vector3(1, 0, 0);
            Vector3 pt2 = new Vector3(0, 1, 0);
            Vector3 pt3 = new Vector3(-1, 0, 0);

            Arc arc = new Arc(pt1, pt2, pt3);

            Assert.AreEqual(0, arc.Center.X);
            Assert.AreEqual(0, arc.Center.Y);
            Assert.AreEqual(0, arc.Center.Z);
            Assert.AreEqual(1, arc.Radius);

            Assert.AreEqual(Math.PI, arc.Length);
        }
        [TestMethod]
        public void Arc_Translate_returnVal()
        {
            Arc arc = new Arc();

            arc.Radius = 1;
            arc.Center = new Vector3(1, 1, 1);

            arc.StartAngleRad = 0;
            arc.EndAngleRad = Math.PI / 2;

            IArc arcTrans = arc.Translate(new Vector3(1, 1, 2));

            Assert.AreEqual(2d, arcTrans.Center.X, "centx");
            Assert.AreEqual(2d, arcTrans.Center.Y, "centy");
            Assert.AreEqual(3d, arcTrans.Center.Z, "centz");

        }
        [TestMethod]
        public void Arc_RotateX_returnVal()
        {
            Arc arc = new Arc();

            arc.Radius = 1;
            arc.Center = new Vector3(1, 1, 1);

            arc.StartAngleRad = 0;
            arc.EndAngleRad = Math.PI / 3;

            IArc arcRot = arc.RotateX(new Vector3(), Math.PI);

            Assert.AreEqual(Math.PI, arcRot.StartAngleRad, "start");
            Assert.AreEqual(4 * Math.PI / 3, arcRot.EndAngleRad, "end");
            Assert.AreEqual(1d, Math.Round(arcRot.Center.X, 8), "centx");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.Y, 8), "centy");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.Z, 8), "centz");
            Assert.AreEqual(arc.Radius, arcRot.Radius, "radius");

        }
        [TestMethod]
        public void Arc_RotateY_returnVal()
        {
            Arc arc = new Arc();

            arc.Radius = 1;
            arc.Center = new Vector3(1, 1, 1);

            arc.StartAngleRad = 0;
            arc.EndAngleRad = Math.PI / 3;

            IArc arcRot = arc.RotateY(new Vector3(), Math.PI);

            Assert.AreEqual(Math.PI, arcRot.StartAngleRad, "start");
            Assert.AreEqual(4 * Math.PI / 3, arcRot.EndAngleRad, "end");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.X, 8), "centx");
            Assert.AreEqual(1d, Math.Round(arcRot.Center.Y, 8), "centy");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.Z, 8), "centz");
            Assert.AreEqual(arc.Radius, arcRot.Radius, "radius");

        }
        [TestMethod]
        public void Arc_RotateZ_returnVal()
        {
            Arc arc = new Arc();

            arc.Radius = 1;
            arc.Center = new Vector3(1, 1, 1);

            arc.StartAngleRad = 0;
            arc.EndAngleRad = Math.PI / 3;

            IArc arcRot = arc.RotateZ(new Vector3(), Math.PI);

            Assert.AreEqual(Math.PI, arcRot.StartAngleRad, "start");
            Assert.AreEqual(4 * Math.PI / 3, arcRot.EndAngleRad, "end");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.X, 8), "centx");
            Assert.AreEqual(-1d, Math.Round(arcRot.Center.Y, 8), "centy");
            Assert.AreEqual(arc.Center.Z, Math.Round(arcRot.Center.Z, 8), "centz");
            Assert.AreEqual(arc.Radius, arcRot.Radius, "radius");

        }

    }
}
