using CNCLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;

namespace CNCLibTests
{
    [TestClass]
    public class LinearAxisTests
    {
        [TestMethod]
        public void LinearAxis_DefConst_builtOK()
        {
            LinearAxis la = new LinearAxis();
            uint t = 0;
            Assert.AreEqual(t, la.EncoderCtsPerRev);
            Assert.AreEqual(AxisType.Linear, la.Type);
            Assert.AreEqual("Axis0", la.Name);
        }
        [TestMethod]
        public void LinearAxis_defLinear_builtOK()
        {
            LinearAxis la = new LinearAxis(1, "test1", "plcX");
            uint t = 0;
            
            Assert.AreEqual(AxisType.Linear, la.Type);

        }
        
        
         
    }
}
