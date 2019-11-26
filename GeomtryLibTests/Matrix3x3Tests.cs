using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometryLibTests
{
    /// <summary>
    /// Summary description for Matrix3x3Tests
    /// </summary>
    [TestClass]
    public class Matrix3x3Tests
    {
        [TestMethod]
        public void Matrix3x3_scalarMultiply_returnM()
        {
            Matrix3x3 m = Matrix3x3.Identity();

            Matrix3x3 mOut = m * 3.0;

            Assert.AreEqual(3.0, mOut.Matrix[0, 0], .0001);
            Assert.AreEqual(3.0, mOut.Matrix[1, 1], .0001);
            Assert.AreEqual(3.0, mOut.Matrix[2, 2], .0001);
            Assert.AreEqual(0, mOut.Matrix[0, 1], .0001);
            Assert.AreEqual(0, mOut.Matrix[1, 0], .0001);
            Assert.AreEqual(0, mOut.Matrix[2, 1], .0001);
        }
        [TestMethod]
        public void Matrix3x3_vectorMultiply_returnsV()
        {
            Matrix3x3 m = new Matrix3x3(1, 1, 1, 2, 2, 2, 3, 3, 3);
            IVector3 v = new Vector3(1, 2, 3);

            IVector3 vOut = m * v;

            Assert.AreEqual(6d, vOut.X, .0001);
            Assert.AreEqual(12d, vOut.Y, .0001);
            Assert.AreEqual(18d, vOut.Z, .0001);
        }
        [TestMethod]
        public void Matrix3x3_matrixMuliply_returnsM()
        {
            Matrix3x3 m1 = new Matrix3x3(1, 1, 1, 2, 2, 2, 3, 3, 3);
            Matrix3x3 m2 = new Matrix3x3(1, 1, 1, 2, 2, 2, 3, 3, 3);

            Matrix3x3 mOut = m1 * m2;

            Assert.AreEqual(6d, mOut.Matrix[0, 0], .0001);
            Assert.AreEqual(12d, mOut.Matrix[1, 1], .0001);
            Assert.AreEqual(18d, mOut.Matrix[2, 2], .0001);
        }
        [TestMethod]
        public void Matrix3x3_determinant()
        {
            Matrix3x3 m1 = new Matrix3x3(1, 2, 3, 1, 1, 1, 3, 3, 3);
            double d = m1.Determinant();
            Assert.AreEqual(0, d, .0001);
        }

    }
}
