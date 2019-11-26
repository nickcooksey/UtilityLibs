using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace GeometryLibTests
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void Line_defConst_returnsZeroLengthLine()
        {
            ILine3 line = new Line3();
            Assert.AreEqual(0, line.Length);

        }
        [TestMethod]
        public void Line_defConst_notNull()
        {
            ILine3 line = new Line3();
            Assert.IsNotNull(line);

        }
        [TestMethod]
        public void Line_const_lengthCorrect()
        {
            ILine3 line = new Line3(1, 1, 1, 2, 2, 2);
            Assert.AreEqual(Math.Sqrt(3), line.Length, "len");
        }
        [TestMethod]
        public void Line_translate_returnsVal()
        {
            ILine3 line = new Line3(1, 1, 1, 2, 2, 2);
            ILine3 ltrans = line.Translate(new Vector3(1, 1, 1));

            Assert.AreEqual(2, ltrans.Point1.X, "x1");
            Assert.AreEqual(2, ltrans.Point1.Y, "y1");
            Assert.AreEqual(2, ltrans.Point1.Z, "z1");
        }
        [TestMethod]
        public void Line_rotateZ_returnsVal()
        {
            ILine3 line = new Line3(1, 1, 0, 2, 2, 0);
            Vector3 pc = new Vector3(0, 0, 0);

            ILine3 lrot = line.RotateZ(pc, Math.PI);

            Assert.AreEqual(line.Length, lrot.Length, "len");
            Assert.AreEqual(-1d, Math.Round(lrot.Point1.X, 8), "x1");
            Assert.AreEqual(-1d, Math.Round(lrot.Point1.Y, 8), "y1");
            Assert.AreEqual(0d, Math.Round(lrot.Point1.Z, 8), "z1");
        }
        [TestMethod]
        public void Line_rotateX_returnsVal()
        {
            ILine3 line = new Line3(1, 1, 0, 2, 2, 0);
            Vector3 pc = new Vector3(0, 0, 0);

            ILine3 lrot = line.RotateX(pc, Math.PI);

            Assert.AreEqual(line.Length, lrot.Length, "len");
            Assert.AreEqual(1d, Math.Round(lrot.Point1.X, 8), "x1");
            Assert.AreEqual(-1d, Math.Round(lrot.Point1.Y, 8), "y1");
            Assert.AreEqual(0d, Math.Round(lrot.Point1.Z, 8), "z1");
            Assert.AreEqual(2d, Math.Round(lrot.Point2.X, 8), "x2");
            Assert.AreEqual(-2d, Math.Round(lrot.Point2.Y, 8), "y2");
            Assert.AreEqual(0d, Math.Round(lrot.Point2.Z, 8), "z2");
        }
        [TestMethod]
        public void Line_rotateY_returnsVal()
        {
            ILine3 line = new Line3(1, 1, 0, 2, 2, 0);
            Vector3 pc = new Vector3(0, 0, 0);

            ILine3 lrot = line.RotateY(pc, Math.PI);

            Assert.AreEqual(line.Length, lrot.Length, "len");
            Assert.AreEqual(-1d, Math.Round(lrot.Point1.X, 8), "x1");
            Assert.AreEqual(1d, Math.Round(lrot.Point1.Y, 8), "y1");
            Assert.AreEqual(0d, Math.Round(lrot.Point1.Z, 8), "z1");
            Assert.AreEqual(-2d, Math.Round(lrot.Point2.X, 8), "x2");
            Assert.AreEqual(2d, Math.Round(lrot.Point2.Y, 8), "y2");
            Assert.AreEqual(0d, Math.Round(lrot.Point2.Z, 8), "z2");
        }
    }
}
