
using FileIOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace FileIOLibTests
{

    [TestClass]
    public class CSVFileTests
    {
        private string fileNameEmpty = "";

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void CSVFileToArray_FileIsEmpty_ReturnsEmptyArray()
        {
            string csvFilename = fileNameEmpty;
            double[,] data;
            var fp = new CSVFileParser();
            data = fp.ParseToDoubleArr(csvFilename, 0);
             
        }
        [TestMethod]
        public void CSVFileToArray_FileHasHeaderOnly_ReturnsEmptyArray()
        {
            string csvFilename = "fileWithOnlyHeader.csv";

            double[,] data;
            var fp = new CSVFileParser();
            data = fp.ParseToDoubleArr(csvFilename, 1);
            Assert.IsNotNull(data, "array is null");
            Assert.AreEqual(0, data.GetLength(0), "array dim0 should be 0");
            Assert.AreEqual(0, data.GetLength(1), "array dim1 should be 0");
        }
        [TestMethod]
        public void CSVFileToArray_FileHasContentOnly_ReturnsContent()
        {
            string csvFilename = "TextFile2.csv";

            double[,] data;
            var fp = new CSVFileParser();
            data = fp.ParseToDoubleArr(csvFilename, 0);
            Assert.IsNotNull(data);
            Assert.AreEqual(3, data.GetLength(0), "array dim0 should be 3");
            Assert.AreEqual(3, data.GetLength(1), "array dim1 should be 3");
            Assert.AreEqual(9, data.Length, "length");

            Assert.AreEqual(1.0, (double)data[0, 0], "r0,c0");

            Assert.AreEqual(4.0, (double)data[1, 0], " r1,c0");
            Assert.AreEqual(5.0, (double)data[1, 1], " r1,c1");
            Assert.AreEqual(7.0, (double)data[2, 0], " r2,c0");
            Assert.AreEqual(9.0, (double)data[2, 2], " r2,c2");
        }
        [TestMethod]
        public void CSVFileToArray_FileHasContentAndHeader_ArrayIsRightSize()
        {
            string csvFilename = "FileWithHeader.csv";
            double[,] data;
            var fp = new CSVFileParser();
            data = fp.ParseToDoubleArr(csvFilename, 1);
            Assert.IsNotNull(data);
            Assert.AreEqual(3, data.GetLength(0), "array dim0 should be 3");
            Assert.AreEqual(3, data.GetLength(1), "array dim1 should be 3");
            Assert.AreEqual(9, data.Length, "length");
        }
        [TestMethod]
        public void CSVFileToArray_FileHasContentAndHeader_ReturnsAllContent()
        {
            string csvFilename = "FileWithHeader.csv";
            double[,] data;
            var fp = new CSVFileParser();
            data = fp.ParseToDoubleArr(csvFilename, 1);
            Assert.IsNotNull(data);
            Assert.AreEqual(3, data.GetLength(0), "array dim0 should be 3");
            Assert.AreEqual(3, data.GetLength(1), "array dim1 should be 3");
            Assert.AreEqual(9, data.Length, "length");

            Assert.AreEqual(1.0, (double)data[0, 0], "r0,c0");
            Assert.AreEqual(4.0, (double)data[1, 0], " r1,c0");
            Assert.AreEqual(5.0, (double)data[1, 1], " r1,c1");
            Assert.AreEqual(7.0, (double)data[2, 0], " r2,c0");
            Assert.AreEqual(9.0, (double)data[2, 2], " r2,c2");
        }
    }
}
