using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNCLib;
using ICNCLib;

namespace CNCLibTests
{
    [TestClass]
    public class NcElementTests
    {
        [TestMethod]
        public void NcE_DefCtor_notNull()
        {
            var nce = new NcElement();
            Assert.IsNotNull(nce);
            Assert.IsNotNull(nce.Content);
            Assert.AreEqual(0, nce.LineNumber);
            
        }

    }
   
}
