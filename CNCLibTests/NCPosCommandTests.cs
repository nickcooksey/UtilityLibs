using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNCLib;
using ICNCLib;

namespace CNCLibTests
{
    [TestClass]
    public class NCPositionCommandTests
    {
        [TestMethod]
        public void NCp_1ptctor_Valueok()
        {
            var mp1 = new MachinePosition();

            var f = new Feedrate(FeedrateUnits.InPerMin);


            int line = 1;
            var ncp = new NcPositionCommand(line, mp1, f, BlockType.LINEAR );
            Assert.IsNotNull(ncp);
            Assert.IsNotNull(ncp.Feedrate);
            Assert.IsNotNull(ncp.MachinePosition);
            Assert.AreEqual(1, ncp.LineNumber);

        }
        [TestMethod]
        public void NCp_1ptctorXaMachine_Valueok()
        {


            var f = new Feedrate(FeedrateUnits.InPerMin);
            f.Value = 10;

            var mp1 = new MachinePosition();
            mp1.X = -4;
            mp1.Adeg = 20;
            int line = 1;
            var ncp = new NcPositionCommand(line, mp1, f, BlockType.LINEAR );
            Assert.IsNotNull(ncp);
            Assert.IsNotNull(ncp.Feedrate);
            Assert.IsNotNull(ncp.MachinePosition);
            Assert.AreEqual(1, ncp.LineNumber);
            var ncMachine = new NcMachine(ControllerType.FAGOR8055, MachineGeometry.XA);
            string ncStr = ncp.AsNcString(ncMachine);
            StringAssert.Contains("N1 G90 G01 X-4.0000 A20.000 F10.00", ncStr);
        }
        [TestMethod]
        public void NCp_2ptCtor_ValueOK()
        {
            var mp1 = new MachinePosition(1, 1, 1, 1, 1);
            var mp2 = new MachinePosition(5, 6, 7, 8, 9);
            var f = new Feedrate(FeedrateUnits.InPerMin);
            f.Value = 10;
            
            var ncp = new NcPositionCommand(1, mp1,f, BlockType.LINEAR );
             
            var ncMachine = new NcMachine(ControllerType.FAGOR8055, MachineGeometry.XYZBC);

            string ncStr = ncp.AsNcString(ncMachine);

            Assert.IsNotNull(ncp);
            Assert.IsNotNull(ncp.Feedrate);
            Assert.IsNotNull(ncp.MachinePosition);
            Assert.AreEqual(1, ncp.LineNumber);
            StringAssert.Contains("N1 G90 G01 X1.0000 Y1.0000 Z1.0000 B1.000 C1.000 F10.00", ncStr);


        }
    }
}
