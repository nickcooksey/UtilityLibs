using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace GeometryLibTests
{
    [TestClass]
    public class Vector3Tests
    {
        [TestMethod]
        public void Vector3_defConst_returnsOrigin()
        {
            IVector3 pt = new Vector3();
            Assert.AreEqual(0d, pt.X, "x");
            Assert.AreEqual(0d, pt.Y, "y");
            Assert.AreEqual(0d, pt.Z, "z");

        }
        [TestMethod]
        public void vector3_constFromCylpt_returnVal()
        {
            IPointCyl ptc = new PointCyl(2, Math.PI / 2, 3);
            IVector3 v = new Vector3(ptc);
            Assert.AreEqual(0, Math.Round(v.X, 5));
            Assert.AreEqual(2, Math.Round(v.Y, 5));
            Assert.AreEqual(3, Math.Round(v.Z, 5));

        }
        [TestMethod]
        public void vector3_constFromVect_returnsVal()
        {
            IVector3 v1 = new Vector3(1, 2, 3);
            IVector3 v2 = new Vector3(v1);
            Assert.AreEqual(1, v2.X);
            Assert.AreEqual(2, v2.Y);
            Assert.AreEqual(3, v2.Z);

        }
        [TestMethod]
        public void vector3_translate_returnsVal()
        {
            IVector3 pt = new Vector3(1, 1, 1);
            IVector3 ptTrans = pt.Translate(new Vector3(1, 2, 3));
            Assert.AreEqual(2.0d, ptTrans.X, "x");
            Assert.AreEqual(3.0d, ptTrans.Y, "y");
            Assert.AreEqual(4d, ptTrans.Z, "z");
            Assert.AreEqual(pt.Col, ptTrans.Col);
        }
        [TestMethod]
        public void vector3_rotateX_returnsVal()
        {
            IVector3 pt = new Vector3(1, 1, 1);
            IVector3 ptCRot = new Vector3(0, 0, 0);

            IVector3 ptRotate = pt.RotateX(ptCRot, Math.PI);
            Assert.AreEqual(pt.Col, ptRotate.Col);
            Assert.AreEqual(1d, Math.Round(ptRotate.X, 6), "x");
            Assert.AreEqual(-1d, Math.Round(ptRotate.Y, 10), "y");
            Assert.AreEqual(-1d, Math.Round(ptRotate.Z, 10), "z");
        }
        [TestMethod]
        public void vector3_rotateY_returnsVal()
        {
            IVector3 pt = new Vector3(1, 1, 1);
            IVector3 ptCRot = new Vector3(0, 0, 0);

            IVector3 ptRotate = pt.RotateY(ptCRot, Math.PI);
            Assert.AreEqual(pt.Col, ptRotate.Col);
            Assert.AreEqual(-1, Math.Round(ptRotate.X, 6), "x");
            Assert.AreEqual(1, Math.Round(ptRotate.Y, 10), "y");
            Assert.AreEqual(-1d, Math.Round(ptRotate.Z, 10), "z");
        }
        [TestMethod]
        public void vector3_rotateZ_returnsVal()
        {
            IVector3 pt = new Vector3(1, 1, 1);
            IVector3 ptCRot = new Vector3(0, 0, 0);

            IVector3 ptRotate = pt.RotateZ(ptCRot, Math.PI);
            Assert.AreEqual(pt.Col, ptRotate.Col);
            Assert.AreEqual(-1d, Math.Round(ptRotate.X, 6), .001, "x");
            Assert.AreEqual(-1d, Math.Round(ptRotate.Y, 10), .001, "y");
            Assert.AreEqual(1d, Math.Round(ptRotate.Z, 10), .001, "z");
        }
        [TestMethod]
        public void Vector3_dot_returnsVal()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(2, 3, 1);
            double d = v1.Dot(v2);
            Assert.AreEqual(6d, d);

        }
        [TestMethod]
        public void vector3_cross_returnsval()
        {
            IVector3 v1 = new Vector3(1, 3, 2);
            IVector3 v2 = new Vector3(1, 2, 3);

            IVector3 vcross = v1.Cross(v2);


            Assert.AreEqual(5.0, vcross.X);
            Assert.AreEqual(-1.0, vcross.Y);
            Assert.AreEqual(-1.0, vcross.Z);

        }
        [TestMethod]
        public void Vector3_normalize_returnsVal()
        {
            IVector3 v2 = new Vector3(4, 3, 0);
            v2.Normalize();
            Assert.AreEqual(0.8, v2.X);
            Assert.AreEqual(0.6, v2.Y);
        }
        [TestMethod]
        public void Vect3_add_returnsVal()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(2, 3, 1);
            IVector3 v3 = v1.Plus(v2);
            Assert.AreEqual(3d, v3.X);
            Assert.AreEqual(4d, v3.Y);
        }
        [TestMethod]
        public void Vect3_subtract_returnsVal()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(2, 3, 1);
            IVector3 v3 = v1.Minus(v2);
            Assert.AreEqual(-1d, v3.X);
            Assert.AreEqual(-2d, v3.Y);
        }
        [TestMethod]
        public void Vect3_equals_returnsTrue()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(1, 1, 1);
            Assert.IsTrue(v1.Equals(v2));
        }
        [TestMethod]
        public void Vect3_equals_returnsF()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(1, 2, 1);
            Assert.IsFalse(v1.Equals( v2));
        }
        [TestMethod]
        public void Vect3_notEquals_returnsFalse()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(1, 1, 1);
            Assert.IsFalse(v1.NotEqualTo( v2));
        }
        [TestMethod]
        public void Vect3_notEquals_returnTrue()
        {
            IVector3 v1 = new Vector3(1, 1, 1);
            IVector3 v2 = new Vector3(1, 2, 1);
            Assert.IsTrue(v1.NotEqualTo( v2));
        }
    }
}
