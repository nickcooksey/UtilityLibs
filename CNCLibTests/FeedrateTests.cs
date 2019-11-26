using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;
using CNCLib;
namespace CNCLibTests
{
    [TestClass]
    public class FeedrateTests
    {
        [TestMethod]
        public void Feedrate_ctor_notnull()
        {
            var f = new Feedrate(FeedrateUnits.InPerMin);
                       
            Assert.IsNotNull(f);
            Assert.IsFalse(f.Inverted);
            Assert.AreEqual(0, f.Value);
        }
        [TestMethod]
        public void Feedrate_invertedFeed_feedOK()
        {
            var f = new Feedrate(FeedrateUnits.MinPerMove);
           
            f.Value = 100;
            Assert.IsNotNull(f);
            Assert.IsTrue(f.Inverted);
            Assert.AreEqual(100, f.Value);
        }   
    }
}
