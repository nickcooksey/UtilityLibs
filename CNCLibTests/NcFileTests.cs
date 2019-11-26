using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;
using CNCLib;

namespace CNCLibTests
{ 
    
    [TestClass]
    public class NcFileTests
    {
        [TestMethod]
        public void NcFile_defCtor_notnull()
        {
            var ncfile = new NcFile();
            Assert.IsNotNull(ncfile);
            Assert.AreEqual(0, ncfile.Count);
        }
        [TestMethod]
        public void NcFile_multiLine_countOK()
        {
            var ncfile = new NcFile();
            var mp1 = new MachinePosition() { X = 1, Adeg = 0 };
            var mp2 = new MachinePosition() { X = 1, Adeg = 360 };
            var f1 = new Feedrate(FeedrateUnits.InPerMin) { Value = 20 };
            var f2 = new Feedrate(FeedrateUnits.InverseMins) { Value = 2 };
            var ncp1 = new NcPositionCommand(10, mp1, f1,BlockType.LINEAR );
            var ncp2 = new NcPositionCommand(20, mp2, f2, BlockType.LINEAR);
            var ncMachine = new NcMachine(ControllerType.FAGOR8055, MachineGeometry.XA);
            ncfile.Add(ncp1);
            ncfile.Add(ncp2);
            var file = ncfile.AsNcTextFile(ncMachine);
            Assert.AreEqual(2, ncfile.Count);
            Assert.AreEqual(5, file.Count);

        }
        [TestMethod]
        public void NcFile_multiLine_valuesOK()
        {
            var ncfile = new NcFile();
            var mp1 = new MachinePosition() { X = 1, Adeg = 0 };
            var mp2 = new MachinePosition() { X = 1, Adeg = 360 };
            var f1 = new Feedrate(FeedrateUnits.InPerMin) { Value = 20 };
            var f2 = new Feedrate(FeedrateUnits.InverseMins) { Value = 2 };
            var ncp1 = new NcPositionCommand(10, mp1, f1, BlockType.LINEAR );
            var ncp2 = new NcPositionCommand(20, mp2, f2, BlockType.LINEAR );
            var ncMachine = new NcMachine(ControllerType.FAGOR8055, MachineGeometry.XA);
            ncfile.Add(ncp1);
            ncfile.Add(ncp2);
            var ncText = ncfile.AsNcTextFile(ncMachine);
            var ncfileLine0 = ncText[0];
            var ncfileLine1 = ncText[1];
            var ncfileLine2 = ncText[2];
            var line0 = ";Title";
            var line1 = "N10 G90 G01 X1.0000 A0.000 F20.00";
            var line2 = "N20 G32 G90 G01 X1.0000 A360.000 F2.00";
            StringAssert.Contains(ncfileLine0, line0);
            StringAssert.Contains(ncfileLine1, line1);
            StringAssert.Contains(ncfileLine2, line2);

        }
    }
}
