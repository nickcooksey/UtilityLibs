using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNCLib;
using GeometryLib;
namespace CNCLibTests
{
    [TestClass]
    public class MachinePositionTests
    {
        [TestMethod]
        public void MachinePosition_defConst_builtOK()
        {
            MachinePosition mp = new MachinePosition();
            Assert.IsNotNull(mp);
            Assert.AreEqual(0, mp.X);
            Assert.AreEqual(0, mp.Y);
            Assert.AreEqual(0, mp.Z);
            Assert.AreEqual(0, mp.Adeg);
            Assert.AreEqual(0, mp.Bdeg);
            Assert.AreEqual(0, mp.Cdeg);
        }
        [TestMethod]
        public void MachinePosition_disttoSelf_distisZero()
        {
            MachinePosition mp1 = new MachinePosition();
            
            double dist = mp1.DistanceTo(mp1);
            Assert.AreEqual(0, dist);
        }
        [TestMethod]
        public void MachinePosition_distToPt_distisOK()
        {
            MachinePosition mp1 = new MachinePosition(0,0,0);
            MachinePosition mp2 = new MachinePosition(1,1,1);

            double dist = mp1.DistanceTo(mp2);
            Assert.AreEqual(Math.Sqrt(3), dist);
        }
        [TestMethod]
        public void MachinePosition_distTo5axisPt_distisOK()
        {
            MachinePosition mp1 = new MachinePosition(0, 0, 0,45,45);
            MachinePosition mp2 = new MachinePosition(1, 1, 1,0,0);
            
            double dist = mp1.DistanceTo(mp2);
            Assert.AreEqual(Math.Sqrt(3), dist);
        }
        [TestMethod]
        public void MachinePosition_distTo5axisOnlyPt_distisOK()
        {
            MachinePosition mp1 = new MachinePosition(0, 0, 0, 45, 45);
            MachinePosition mp2 = new MachinePosition(0, 0, 0, 0, 0);

            double dist = mp1.DistanceTo(mp2);
            Assert.AreEqual(0, dist);
        }
        [TestMethod]
        public void MachinePosition_setPosition_PosIsOK()
        {
            MachinePosition mp1 = new MachinePosition();
            

            mp1.Bdeg = 45;
            double btest = mp1.Bdeg;
            Assert.AreEqual(45, btest);
            mp1.Cdeg = 45;
            double ctest = mp1.Cdeg;
            Assert.AreEqual(45, ctest);
        }
    }
}
