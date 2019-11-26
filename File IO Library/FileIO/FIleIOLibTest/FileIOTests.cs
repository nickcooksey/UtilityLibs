using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUtilities;
namespace FIleIOLibTest
{

    
    [TestClass]
    public class FileIOTests
    {
        List<string> fileContents;
        string fileName = "";
        [TestMethod]
        public void ReadDataTextFile_()
        {
            fileContents = FileIO.ReadDataTextFile(fileName);
            Assert.IsNotNull(fileContents);
        }
    }
}
