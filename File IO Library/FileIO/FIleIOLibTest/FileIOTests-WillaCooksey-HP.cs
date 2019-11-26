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
        string filename2 = "TextFile1.txt";
        [TestMethod]
        public void ReadDataFileTest()
        {
            fileContents = FileIO.ReadDataTextFile(fileName);
            Assert.IsNotNull(fileContents);
        }
        [TestMethod]
        public void ReadDataTextFile_()
        {

            fileContents = FileIO.ReadDataTextFile(filename2);
            Assert.IsNotNull(fileContents);
            Assert.AreEqual(fileContents[0], "line1=1");
            Assert.AreEqual(fileContents[1], "line2=2");
        }
        [TestMethod]
        public void ReadDataParamfile()
        {
            char[] delims = new char[1]{'='};
            fileContents = FileIO.ReadParamsTextFile(filename2, true, delims);
            Assert.IsNotNull(fileContents);
            Assert.AreEqual(3, fileContents.Count, "file length");
            Assert.AreEqual( "1",fileContents[0],"line0");
            Assert.AreEqual( "2",fileContents[1],"line1");
        }

        string csvfilename = "TextFile2.csv";
        [TestMethod]
        public void ReadCsvFile()
        {
            object[,] data;
            data = CSVFile.ToArray(csvfilename);
            Assert.IsNotNull(data);
            Assert.AreEqual(9,data.Length, "length");
            Assert.AreEqual(2,data.GetUpperBound(0),"0 dim");
            Assert.AreEqual(2,data.GetUpperBound(1), "1 dim");
            Assert.AreEqual(1.0,(double)data[0, 0], "0,0");
            Assert.AreEqual(5.0,(double)data[1, 1],"1,1");
            Assert.AreEqual(9.0,(double)data[2, 2], "2,2");    
        }

    }
}
