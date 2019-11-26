using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICNCLib;
using CNCLib;

namespace CNCLibTests
{
    [TestClass]
    public class CNCFileParserTests
    {

        [TestMethod]
        public void CNCFileParser_ParseNCIFile_pathNotNull()
        {
            string inputFile = "SPLINE-CHANNEL-CONTOURPATH.NCI";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputFile);
            Assert.IsNotNull(toolpath);
        }
        [TestMethod]
        public void CNCFileParser_ParseNCIFile_toolpathIsNotZerolength()
        {
            string inputFile = "SPLINE-CHANNEL-CONTOURPATH.NCI";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputFile);
            int count = toolpath.Count;
            Assert.IsFalse(count == 0);
        }
        [TestMethod]
        public void CNCFileParser_ParseNCIFile_toolpathhasCorrectValues()
        {
            string inputFile = "SPLINE-CHANNEL-CONTOURPATH.NCI";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputFile);

            Assert.AreEqual(-3.876529, toolpath[0].Position.X, "Xentity0");
            Assert.AreEqual(0.05, toolpath[0].Position.Y, "Yentity0");
            Assert.AreEqual(3.307008, toolpath[0].Position.Z, "Zentity0");
            Assert.AreEqual(-2, toolpath[0].Feedrate.Value, "Fentity0");
            Assert.IsFalse(toolpath[0].JetOn, "Jentity0");

            Assert.AreEqual(-3.876529, toolpath[1].Position.X, "Xentity1");
            Assert.AreEqual(0.05, toolpath[1].Position.Y, "Yentity1");
            Assert.AreEqual(1.407008, toolpath[1].Position.Z, "Zentity1");
            Assert.AreEqual(-2, toolpath[1].Feedrate.Value, "Fentity1");
            Assert.IsFalse(toolpath[1].JetOn, "Jentity1");
            Assert.AreEqual(BlockType.RAPID, toolpath[1].Type, "type 1");

            Assert.AreEqual(-3.565848, toolpath[3].Position.X, "Xentity3");
            Assert.AreEqual(0.05, toolpath[3].Position.Y, "Yentity3");
            Assert.AreEqual(1.357217, toolpath[3].Position.Z, "Zentity3");
            Assert.AreEqual(10, toolpath[3].Feedrate.Value, "Fentity3");
            Assert.IsTrue(toolpath[3].JetOn, "Jentity3");
            Assert.AreEqual(BlockType.LINEAR, toolpath[3].Type, "type 3");
        }

        [TestMethod]
        public void CNCFileParser_ParseNCFile_PathNotNull()
        {
            string inputFile = "2-CELL-TEST.2.nc";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputFile);
            Assert.IsNotNull(toolpath);

        }
        [TestMethod]
        public void CNCFileParser_fileIsGood_toolpathIsNotZerolength()
        {
            string inputFile = "2-CELL-TEST.2.nc";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputFile);
            int count = toolpath.Count;
            Assert.AreNotEqual(0, toolpath.Count);
        }
        
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void CNCparser_toolpathIsNull_throwsException()
        {
            string inputPath = null;
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputPath);

            
        }
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void CNCparser_toolpathIsEmpty_ToolPathIsNotNull()
        {
            string inputPath = "";
            var cncfp = new CNCFileParser();
            IToolpath toolpath = cncfp.CreatePath(inputPath);
            
        }
        [TestMethod]
        public void CNCFileParser_ParseNCFile_toolpathhasCorrectValues()
        {
            string inputFile = "SPLINE-CHANNEL-CONTOURPATH.NC";
            var cncfp = new CNCFileParser();
            IToolpath toolpath =cncfp.CreatePath(inputFile);
            int last = toolpath.Count - 1;
            string position;

            Assert.AreEqual(0, toolpath[0].Position.X, "x entity0");
            Assert.AreEqual(0.0, toolpath[0].Position.Y, "y entity0");
            Assert.AreEqual(3.307, toolpath[0].Position.Z, "z entity0");
            Assert.AreEqual(0, toolpath[0].Feedrate.Value, "f entity0");
            Assert.IsFalse(toolpath[0].JetOn, "j entity0");

            position = "1";
            Assert.AreEqual(-3.8765, toolpath[1].Position.X, "x entity1");
            Assert.AreEqual(0.05, toolpath[1].Position.Y, "y entity1");
            Assert.AreEqual(3.307, toolpath[1].Position.Z, "z entity1");
            Assert.AreEqual(0, toolpath[1].Feedrate.Value, "f entity1");
            Assert.IsFalse(toolpath[1].JetOn, "entity1");
            Assert.AreEqual(BlockType.RAPID, toolpath[1].Type, "type 1");

            position = "2";
            Assert.AreEqual(-3.8765, toolpath[2].Position.X, "X entity 2");
            Assert.AreEqual(0.05, toolpath[2].Position.Y, "Y entity 2");
            Assert.AreEqual(1.407, toolpath[2].Position.Z, "Z entity2");
            Assert.AreEqual(0, toolpath[2].Feedrate.Value, "F entity2");
            Assert.IsFalse(toolpath[2].JetOn, "J entity3");
            Assert.AreEqual(BlockType.RAPID, toolpath[2].Type, "type 2");

            position = " 6";
            Assert.AreEqual(-3.5658, toolpath[6].Position.X, "X entity" + position);
            Assert.AreEqual(0.051, toolpath[6].Position.Y, "Y entity" + position);
            Assert.AreEqual(1.3572, toolpath[6].Position.Z, "Z entity" + position);
            Assert.AreEqual(10, toolpath[6].Feedrate.Value, "F entity" + position);
            Assert.IsTrue(toolpath[6].JetOn, "J entity" + position);
            Assert.AreEqual(BlockType.LINEAR, toolpath[6].Type, "type " + position);

            position = "last-1";

            Assert.AreEqual(4.6218, toolpath[last - 1].Position.X, "X entity" + position);
            Assert.AreEqual(0.05, toolpath[last - 1].Position.Y, "Y entity" + position);
            Assert.AreEqual(1.0623, toolpath[last - 1].Position.Z, "Z entity" + position);
            Assert.AreEqual(10, toolpath[last - 1].Feedrate.Value, "F entity " + position);
            Assert.IsFalse(toolpath[last - 1].JetOn, "J entity" + position);
            Assert.AreEqual(BlockType.DELAY, toolpath[last - 1].Type, "type " + position);

            position = "last";
            Assert.AreEqual(4.6218, toolpath[last].Position.X, "X entity" + position);
            Assert.AreEqual(0.05, toolpath[last].Position.Y, "Y entity" + position);
            Assert.AreEqual(3.307, toolpath[last].Position.Z, "Z entity" + position);
            Assert.AreEqual(0, toolpath[last].Feedrate.Value, "F entity " + position);
            Assert.IsFalse(toolpath[last].JetOn, "J entity" + position);
            Assert.AreEqual(BlockType.RAPID, toolpath[last].Type, "type " + position);
        }
    }
}
