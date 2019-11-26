using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNCLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;

namespace CNCLibTests
{
    [TestClass ]
    public class PositionTests
    {
        [TestMethod]
        public void MachinePosition_defctor()
        {
            var mp = new MachinePosition();
            Assert.AreEqual(0, mp.X);
            
        }
        [TestMethod]
        public void MP_ctorSet_valuesok()
        {
            var mp = new MachinePosition(1, 2, 3, 4, 5);
            Assert.AreEqual(1.0, mp.X);
            Assert.AreEqual(2.0, mp.Y);
            Assert.AreEqual(3.0, mp.Z);
            Assert.AreEqual(4.0, mp.Bdeg);
            Assert.AreEqual(5.0, mp.Cdeg);
            Assert.AreEqual(0, mp.Adeg);
        }
       
        [TestMethod]
        public void MP_ctorCopy_valuesok()
        {
            var mp1 = new MachinePosition(1, 2, 3, 4, 5);
            var mp = new MachinePosition(mp1);
            Assert.AreEqual(1.0, mp.X);
            Assert.AreEqual(2.0, mp.Y);
            Assert.AreEqual(3.0, mp.Z);
            Assert.AreEqual(4.0, mp.Bdeg);
            Assert.AreEqual(5.0, mp.Cdeg);
            Assert.AreEqual(0, mp.Adeg);
            Assert.AreNotSame(mp, mp1);
        }
        [TestMethod]
        public void MachPosition_distance()
        {
            var mp1 = new MachinePosition();
            var mp2 = new MachinePosition() { X = 3, Y = 4 };
            var d = mp1.DistanceTo(mp2);
            Assert.AreEqual(5, d);
        }
        [TestMethod]
        public void MachPos_BuildfromStr()
        {
            var mp = new MachinePosition();
            mp.BuildFromString("X=4,Y=0,Z=0,A=21600,B=0,C=0");
            Assert.AreEqual(4.0, mp.X,.001);
            Assert.AreEqual(21600.0, mp.Adeg,.001);
        }
    }
}
