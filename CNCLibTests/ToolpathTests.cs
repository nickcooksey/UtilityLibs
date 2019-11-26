using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;
using CNCLib;

namespace CNCLibTests
{
    [TestClass]
    public class ToolpathTests
    {
        [TestMethod]
        public void Toolpath_defaultConstructor_pathisEmpty()
        {
            ToolPath tp = new ToolPath();
            Assert.AreEqual(0, tp.Count);
        }
        [TestMethod]
        public void Toolpath_defaultConstructor_pathisNotNull()
        {
            ToolPath tp = new ToolPath();
            Assert.IsNotNull(tp);
        }
        [TestMethod]
        public void Toolpath_const_pathisOK()
        {
            string inputFile = "STRAIGHT-TEST-8-3-15.nc";
            var cncfp = new CNCFileParser();
            IToolpath tp = cncfp.CreatePath(inputFile);
            Assert.AreEqual(1.0, tp[0].Position.X);
            Assert.AreEqual(1.0, tp[0].Position.Y);
            Assert.AreEqual(2.0, tp[0].Position.Z);
        }
    }
}
