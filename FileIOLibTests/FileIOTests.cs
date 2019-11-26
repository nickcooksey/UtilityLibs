using FileIOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace FileIOLibTests
{

    [TestClass]
    public class FileIOTests
    {
        private string fileNameEmpty = "";
        private string filenameParamFile = "TextFile1.txt";
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void ReadDataTextFile_FileIsEmpty_ReturnsEmptyList()
        {
            List<string> fileContents;
            fileContents = FileIO.ReadTextFile(fileNameEmpty);           
            Assert.IsNotNull(fileContents);
            Assert.AreEqual(0, fileContents.Count);
        }
        [TestMethod]
        public void ReadDataTextFile_FileHasContent_ContentIsOK()
        {
            List<string> fileContents;
            fileContents = FileIO.ReadTextFile(filenameParamFile);
            Assert.AreEqual(fileContents[0], "line1=1");

        }
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void ReadParamsTextFile_FileIsEmpty_ReturnsEmptyList()
        {
            List<string> fileContents;
            char[] delims = new char[1] { '=' };
            fileContents = FileIO.ReadParamsTextFile(fileNameEmpty, delims);
             


        }
        [TestMethod]
        public void ReadParamsTextFile_FileHasContent_ReturnsContent()
        {
            List<string> fileContents;
            char[] delims = new char[1] { '=' };
            fileContents = FileIO.ReadParamsTextFile(filenameParamFile, delims);
            Assert.AreEqual(3, fileContents.Count, "file length");
            Assert.AreEqual("1", fileContents[0], "line0_OK");
            Assert.AreEqual("2", fileContents[1], "line1_OK");
        }


    }
}
