using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeometryLib;
namespace DrawingIOTests
{
    [TestClass]
    public class Vector2Tests
    {
        [TestMethod]
        public void Vect2_defConst_returnsOrigin()
        {
            Vector2 v = new Vector2();
            Assert.IsNotNull(v);
            Assert.AreEqual(0d, v.X);
            Assert.AreEqual(0d, v.Y);

        }
        [TestMethod]
        public void Vect2_Const_returnsvals()
        {
            Vector2 v = new Vector2(1,2);
            Assert.AreEqual(1, v.X);
            Assert.AreEqual(2d, v.Y);
        }
        [TestMethod]
        public void Vect2_distTo_returnsdist()
        {
            Vector2 v1 = new Vector2(1, 2);
            Vector2 v2 = new Vector2(4, 6);
            double d = v1.DistanceTo(v2);
            Assert.AreEqual(5d, d);
        }
        [TestMethod]
      public void Vect2_rotate_returnsVals()
      {
          Vector2 v1 = new Vector2(1, 1);
          Vector3 org = new Vector3();
          Vector2 vr = v1.RotateZ(org, Math.PI / 2);
          Assert.AreEqual(-1d, Math.Round(vr.X,8));
          Assert.AreEqual(1d, Math.Round(vr.Y,8));
      }
        [TestMethod]
        public void Vect2_trans_returnsVals()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 vt = v1.Translate(2, 3);
            Assert.AreEqual(3d, vt.X);
            Assert.AreEqual(4d, vt.Y);
        }
        [TestMethod]
        public void Vector2_dot_returnsVal()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(2, 3);
            double d = v1.Dot(v2);
            Assert.AreEqual(5d, d);

        }
        [TestMethod]
        public void Vector2_normalize_returnsVal()
        {
            Vector2 v2 = new Vector2(4, 3);
            v2.Normalize();
            Assert.AreEqual(0.8, v2.X);
            Assert.AreEqual(0.6, v2.Y);
        }
        [TestMethod]
        public void Vect2_add_returnsVal()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(2, 3);
            Vector2 v3 = v1 + v2;
            Assert.AreEqual(3d, v3.X);
            Assert.AreEqual(4d, v3.Y);
        }
        [TestMethod]
        public void Vect2_subtract_returnsVal()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(2, 3);
            Vector2 v3 = v1 - v2;
            Assert.AreEqual(-1d, v3.X);
            Assert.AreEqual(-2d, v3.Y);
        }
        [TestMethod]
        public void Vect2_equals_returnsTrue()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(1, 1);
            Assert.IsTrue(v1 == v2);
        }
        [TestMethod]
        public void Vect2_equals_returnsF()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(1, 2);
            Assert.IsFalse(v1 == v2);
        }
        [TestMethod]
        public void Vect2_notEquals_returnsFalse()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(1, 1);
            Assert.IsFalse(v1 != v2);
        }
        [TestMethod]
        public void Vect2_notEquals_returnTrue()
        {
            Vector2 v1 = new Vector2(1, 1);
            Vector2 v2 = new Vector2(1,2);
            Assert.IsTrue(v1 != v2);
        }
    }
}
