using GeometryLib;
using IGeometryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GeometryLibTests
{
    [TestClass]
    public class BoundingBoxTests
    {
        [TestMethod]
        public void BoundingBox_defconst_returnsEmptyObj()
        {
            BoundingBox ext = new BoundingBox();
            Assert.IsNotNull(ext);
        }
        [TestMethod]
        public void BoundingBox_valuesSet_returnsValues()
        {
            BoundingBox ext = new BoundingBox(-1, -2, -3, 1, 2, 3);
            Assert.IsNotNull(ext);
            Assert.AreEqual(-1d, ext.Min.X);
            Assert.AreEqual(2d, ext.Max.Y);
        }
        [TestMethod]
        public void BoundingBox_valuesSetOutOfOrder_returnsCorrectValues()
        {
            BoundingBox ext = new BoundingBox(1, 2, -3, -1, -2, 3);
            Assert.IsNotNull(ext);
            Assert.AreEqual(-1d, ext.Min.X);
            Assert.AreEqual(2d, ext.Max.Y);
        }
    }
    [TestClass]
    public class BoundingBoxBuilderTests
    {
        [TestMethod]
        public void BoundingBoxB_buildfromPts_returnsCorrectVals()
        {
            Vector3[] ptArray = new Vector3[3];
            ptArray[0] = new Vector3(-1, -2, -3);
            ptArray[1] = new Vector3(4, -4, 4);
            ptArray[2] = new Vector3(1, 2, 3);
            IBoundingBox ext = BoundingBoxBuilder.FromPtArray(ptArray);
            Assert.AreEqual(4d, ext.Max.X);
            Assert.AreEqual(-4d, ext.Min.Y);
            Assert.AreEqual(4d, ext.Max.Z);
        }
        public void BoundingBoxB_union_returnsCorrectVals()
        {
            Vector3[] ptArray = new Vector3[3];
            ptArray[0] = new Vector3(-1, -2, -3);
            ptArray[1] = new Vector3(4, -4, 4);
            ptArray[2] = new Vector3(1, 2, 3);
            Vector3[] ptArray2 = new Vector3[3];
            ptArray2[0] = new Vector3(4, 5, -3);
            ptArray2[1] = new Vector3(4, -4, 6);
            ptArray2[2] = new Vector3(-8, 2, 3);
            IBoundingBox ext1 = BoundingBoxBuilder.FromPtArray(ptArray);
            IBoundingBox ext2 = BoundingBoxBuilder.FromPtArray(ptArray2);
            IBoundingBox ext = BoundingBoxBuilder.Union(new List<IBoundingBox>() { ext1, ext2 });
            Assert.AreEqual(-8d, ext.Min.X);
            Assert.AreEqual(4d, ext.Max.X);
            Assert.AreEqual(5d, ext.Max.Y);
        }
    }
}
