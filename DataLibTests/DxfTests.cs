using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLib;
using IDataLib;

namespace DataLibTests
{
    [TestClass]
    public class DxfTests
    {
        [TestMethod]
        public void Dxf_buildfromfile()
        {
            var filename = "linetest.dxf";
            var dxf = new DxfFile();
            dxf.BuildFromFile(filename);
            var ents = dxf.Entities;
            Assert.AreEqual(2, ents.Count);
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            bool isLine3=false;
            if(ents[0] is IGeometryLib.ILine3 line3)
            {
                x1 = line3.Point1.X;
                y1 = line3.Point1.Y;
                x2 = line3.Point2.X;
                y2 = line3.Point2.Y;
                isLine3 = true;
            }
            Assert.IsTrue(isLine3);
            Assert.AreEqual(1, x1);
            Assert.AreEqual(2, y1);
            Assert.AreEqual(2, x2);
            Assert.AreEqual(3, y2);
        }
        [TestMethod]
        public void DXF_saveOpen()
        {
            var filename = "linetest.dxf";
            var fileOut = "linetestOut.dxf";
            var dxf = new DxfFile();
            dxf.BuildFromFile(filename);
            dxf.Save(fileOut);
            var dxfNew = new DxfFile();
            dxfNew.BuildFromFile(fileOut);
            Assert.AreEqual(dxf.Entities.Count, dxfNew.Entities.Count);
            var bbIn = dxf.BoundingBox();
            var bbOut = dxfNew.BoundingBox();
            Assert.AreEqual(bbIn.Min.X, bbOut.Min.X);
            Assert.AreEqual(bbOut.Min.Y, bbOut.Min.Y);
            Assert.AreEqual(bbIn.Max.X, bbOut.Max.X);
            Assert.AreEqual(bbOut.Max.Y, bbOut.Max.Y);

        }
        [TestMethod]
        public void DXF_boundingbox()
        {
            var filename = "linetest.dxf";            
            var dxf = new DxfFile();
            dxf.BuildFromFile(filename);
            var bb = dxf.BoundingBox();
            Assert.AreEqual(1, bb.Min.X);
            Assert.AreEqual(4, bb.Max.Y);
        }

    }
}
