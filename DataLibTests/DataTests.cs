using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLib;
using IDataLib;
using System.Collections.Generic;
namespace DataLibTests
{
     
    [TestClass]
    public class MeasurementUnitTests
    {
        [TestMethod]
        public void MU_ctor()
        {
            var munit = new MeasurementUnit();
            munit.SetLengthUnit(LengthUnit.INCH);
            Assert.AreEqual(3.937e-5, munit.ConvToMicron, .000001);
            Assert.AreEqual("INCH", munit.Name);
            Assert.AreEqual(LengthUnit.INCH, munit.LengthUnits);

        }
        [TestMethod]
        public void MU_CtorwUnits()
        {
            var munit = new MeasurementUnit(LengthUnit.INCH);            
            Assert.AreEqual(3.937e-5, munit.ConvToMicron, .000001);
            Assert.AreEqual("INCH", munit.Name);
            Assert.AreEqual(LengthUnit.INCH, munit.LengthUnits);
        }
    }
    [TestClass]
    public class DisplayDataTests
    { 
        [TestMethod]
        public void DD_defCtor()
        {
            var dd = new DisplayData();
            Assert.IsNotNull(dd);

        }
        [TestMethod]
        public void DD_CtorColor()
        {
            var dd = new DisplayData("filename");
            dd.Color = System.Drawing.Color.AliceBlue;
            Assert.AreEqual(dd.Color, System.Drawing.Color.AliceBlue);
            Assert.AreEqual("filename", dd.FileName);

        }
        [TestMethod]
        public void DD_AddRange()
        {
            var dd = new DisplayData();
            var pts = new List<System.Drawing.PointF>();
            pts.Add(new System.Drawing.PointF(0, 0));
            pts.Add(new System.Drawing.PointF(0, 0));
            pts.Add(new System.Drawing.PointF(0, 0));
            dd.AddRange(pts);
            Assert.AreEqual(3, dd.Count);
        }
        [TestMethod]
        public void DD_Translate()
        {
            var dd = Init();
            dd.Translate(new System.Drawing.PointF(1, 2));
            Assert.AreEqual(2.0, dd[0].X);
            Assert.AreEqual(3.0, dd[0].Y);
        }
        [TestMethod]
        public void DD_trim()
        {
            var dd = new DisplayData();
            var p1 = new System.Drawing.PointF(-10, -10);
            dd.Add(p1);
            dd.Add(new System.Drawing.PointF(10, 10));

            dd.Add(new System.Drawing.PointF(1, 2));
            dd.Add(new System.Drawing.PointF(4, 3));
            var p2 = new System.Drawing.PointF(5, 5);
            dd.Add(p2);
            var r = new System.Drawing.RectangleF((float)-5.5,(float) -5.5, 11, 11);
            var trimmed = dd.TrimToWindow(r);
            Assert.AreEqual(3, trimmed.Count);
            Assert.IsFalse(trimmed.Contains(p1));
        }
        DisplayData Init()
        {
            var dd = new DisplayData();
            var pts = new List<System.Drawing.PointF>();
            pts.Add(new System.Drawing.PointF(1, 1));
            pts.Add(new System.Drawing.PointF(1, 2));
            pts.Add(new System.Drawing.PointF(2, 2));
            dd.AddRange(pts);
            return dd;
        }    


    }

    [TestClass]
    public class CylDataTests
    { 
        [TestMethod]
        public void CylData_defCtor()
        {
            var cd = new CylData();
            Assert.IsNotNull(cd);
        }
        [TestMethod]
        public void DataTest_filenameok()
        {
            var filename = "filename1.csv";
            var cd = new CylData(filename);
            

            Assert.AreEqual(filename, cd.FileName);

        }
        [TestMethod]
        public void CylDataTest_lengthok()
        {
            var filename = "filename1.csv";
            var cd = new CylData(filename);
            cd.Add(new GeometryLib.PointCyl(0, 0, 0));
            Assert.AreEqual(1, cd.Count);
            
        }
        [TestMethod]
        public void Data_asCSV()
        {
            var cd = Init();           
            var cdAsCsv = cd.AsCSV();
            Assert.AreEqual("0,0,1", cdAsCsv[1]);
        }
        [TestMethod]
        public void CylD_boundingbox()
        {
            var cd = new CylData();
            cd.Add(new GeometryLib.PointCyl(1, 0, 0));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI, 1));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI/2, 1));
            var bb = cd.BoundingBox();
            Assert.AreEqual(1.0, bb.Max.X);
            Assert.AreEqual(-1.0, bb.Min.X);
            Assert.AreEqual(1.0, bb.Max.Z);
            Assert.AreEqual(1.0, bb.Max.Y);
        }
        CylData Init()
        {
            var cd = new CylData();
            var vlist = new List<IGeometryLib.IPointCyl>();
            var v1 = new GeometryLib.PointCyl(0, 0, 1);
            vlist.Add(v1);
            vlist.Add(v1);
            cd.AddRange(vlist);
            return cd;
        }

    }
    [TestClass]
    public class CylGridTests
    {
        [TestMethod]
        public void CylGD_ctor()
        {
            var cd = new CylGridData();
            Assert.IsNotNull(cd);
        }
        [TestMethod]
        public void CylGD_countOK()
        {
            var cgd = new CylGridData();
            var cd = new CylData();
            cd.Add(new GeometryLib.PointCyl(1, 0, 0));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI, 1));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI / 2, 1));
            cd.Add(new GeometryLib.PointCyl(1, 3*Math.PI / 2, 1));
            cgd.Add(cd);
            var cd2 = new CylData();
            cd2.Add(new GeometryLib.PointCyl(2, 0, 0));
            cd2.Add(new GeometryLib.PointCyl(2, Math.PI, 1));
            cd2.Add(new GeometryLib.PointCyl(2, Math.PI / 2, 1));
            cd2.Add(new GeometryLib.PointCyl(2, 3*Math.PI / 2, 1));
            cgd.Add(cd2);
            Assert.AreEqual(2, cgd.Count);
            Assert.AreEqual(4, cgd[0].Count);
            Assert.AreEqual(4, cgd[0].Count);
            Assert.AreEqual(4, cgd[1].Count);
        }
        [TestMethod]
        public void CylG_boundingbox()
        {
            var cgd = new CylGridData();
            var cd = new CylData();
            cd.Add(new GeometryLib.PointCyl(1, 0, 0));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI, 1));
            cd.Add(new GeometryLib.PointCyl(1, Math.PI / 2, 1));
            cd.Add(new GeometryLib.PointCyl(1, 3 * Math.PI / 2, 1));
            cgd.Add(cd);
            var cd2 = new CylData();
            cd2.Add(new GeometryLib.PointCyl(2, 0, 0));
            cd2.Add(new GeometryLib.PointCyl(2, Math.PI, 1));
            cd2.Add(new GeometryLib.PointCyl(2, Math.PI / 2, 1));
            cd2.Add(new GeometryLib.PointCyl(2, 3 * Math.PI / 2, 1));
            cgd.Add(cd2);
            var bb = cgd.BoundingBox();
            Assert.AreEqual(2.0, bb.Max.X);
            Assert.AreEqual(-2.0, bb.Min.X);
            Assert.AreEqual(2.0, bb.Max.Y);
            Assert.AreEqual(-2.0, bb.Min.Y);
            Assert.AreEqual(0, bb.Min.Z);
            Assert.AreEqual(1.0, bb.Max.Z);
        }
    }
    [TestClass]
    public class CartDataTests
    {
        [TestMethod]
        public void CartDataTest_defCtor()
        {
            var cd = new CartData();
            Assert.IsNotNull(cd);
        }
        [TestMethod]
        public void CartDataTest_filenameok()
        {
            var filename = "filename1.csv";
            var cd = new CartData(filename);     
            
            Assert.AreEqual(filename, cd.FileName);

        }
        [TestMethod]
        public void CartDataTest_lengthok()
        {
            var filename = "filename1.csv";
            var cd = new CartData(filename);
            cd.Add(new GeometryLib.Vector3(0, 0, 0));
            Assert.AreEqual(1, cd.Count);
          

        }
        [TestMethod]
        public void CartData_addRange()
        {
            var cd = new CartData();
            var vlist = new List<IGeometryLib.IVector3>();
            var v1 = new GeometryLib.Vector3(0, 0, 1);
            vlist.Add(v1);
            vlist.Add(v1);
            cd.AddRange(vlist);
            Assert.AreEqual(2, cd.Count);            
        }
        [TestMethod]
        public void CartData_asCSV()
        {
            var cd = new CartData();
            var vlist = new List<IGeometryLib.IVector3>();
            var v1 = new GeometryLib.Vector3(0, 0, 1);
            vlist.Add(v1);
            vlist.Add(v1);
            cd.AddRange(vlist);
            var cdAsCsv = cd.AsCSV();
            Assert.AreEqual("0,0,1", cdAsCsv[1]);
             
        }
        [TestMethod]
        public void CartData_asCylData()
        {
            var cd = new CartData();
            var vlist = new List<IGeometryLib.IVector3>();
            var v1 = new GeometryLib.Vector3(3, 4, 0);
            vlist.Add(v1);
            cd.AddRange(vlist);
            var cdAsCyl = cd.AsCylData();
            var ptCyl0= cdAsCyl[0];
            Assert.AreEqual(53.13010, ptCyl0.ThetaDeg, 1e-4);
            Assert.AreEqual(5, ptCyl0.R);
            Assert.AreEqual(0, ptCyl0.Z);
        }
        [TestMethod]
        public void Cartdata_asDisplayData()
        {
            var cd = new CartData();
            var vlist = new List<IGeometryLib.IVector3>();
            var v1 = new GeometryLib.Vector3(3, 4, 0);
            vlist.Add(v1);
            cd.AddRange(vlist);
            var cdAsDisplay = cd.AsDisplayData(IGeometryLib.ViewPlane.XY);
            
            Assert.AreEqual(3, cdAsDisplay[0].X);
            Assert.AreEqual(4, cdAsDisplay[0].Y);
        }
        [TestMethod]
        public void CartD_boundingbox()
        {
            var cd = new CartData();
            cd.Add(new GeometryLib.Vector3(-3, -4, 0));
            cd.Add(new GeometryLib.Vector3(3, 4, 0));
            cd.Add(new GeometryLib.Vector3(2, 5, 0));
            cd.Add(new GeometryLib.Vector3(3, 4, 0));
            var bb= cd.BoundingBox();
            Assert.AreEqual(-3.0, bb.Min.X);
            Assert.AreEqual(3.0, bb.Max.X);
            Assert.AreEqual(5.0, bb.Max.Y);
            Assert.AreEqual(-4.0, bb.Min.Y);
        }
    }
   
    [TestClass]
    public class CartGridTests
    {
        [TestMethod]
        public void CartDataTest_defCtor()
        {
            var cd = new CartGridData();
            Assert.IsNotNull(cd);
        }
        [TestMethod]
        public void CartDataTest_filenameok()
        {
            var filename = "filename1.csv";
            var cd = new CartGridData(filename);

            Assert.AreEqual(filename, cd.FileName);

        }
        [TestMethod]
        public void CartDataTest_lengthok()
        {
            var filename = "filename1.csv";
            var cd = new CartGridData(filename);
            var cart = new CartData();
            cd.Add(cart);
            cd.Add(cart);
            cd.Add(cart);           
            Assert.AreEqual(3, cd.Count);
        }
        [TestMethod]
        public void CartDataTest_Addrange()
        {
            var filename = "filename1.csv";
            var cd = new CartGridData(filename);
            var cart = new CartData();
            var list = new List<ICartData>();
            list.Add(cart);
            list.Add(cart);
            list.Add(cart);
            cd.AddRange(list);
            Assert.AreEqual(3, cd.Count);
        }

    }

}
