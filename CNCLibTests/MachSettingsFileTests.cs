using CNCLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCLibTests
{
    [TestClass]
    public class MachineSettingFileTest
    {
        private string testFileName = "MachineSetttingTestFile.msx";
        [TestMethod]
        public void MSFTests_saveFile_SaveOK()
        {
            MachineSettings ms = new MachineSettings();

            MachineSettingsFile.Save(ms, testFileName);
        }
        [TestMethod]
        public void MSFTests_saveFileToDefName_SaveOK()
        {
            MachineSettings ms = new MachineSettings();

            MachineSettingsFile.Save(ms);
        }
        [TestMethod]
        public void MSFTests_openTestFile_valuesOK()
        {
            MachineSettings msOpen = MachineSettingsFile.Open(testFileName);
            Assert.AreEqual("o5", msOpen.StartInspectionTrigger);
            Assert.AreEqual("o5", msOpen.InPositionTrigger);
            Assert.AreEqual(50, msOpen.MsPerReading);
            Assert.IsFalse(msOpen.CncIsAttached);


        }

        [TestMethod]
        public void MSFTests_saveAndRetrieveFile_fileOK()
        {
            MachineSettings ms = new MachineSettings();

            MachineSettingsFile.Save(ms);
            MachineSettings msOpen = MachineSettingsFile.Open();
            Assert.AreEqual(ms.AngleEpsilon, msOpen.AngleEpsilon);
            Assert.AreEqual(ms.AxisCount, msOpen.AxisCount);
            Assert.AreEqual(ms.CncIsAttached, msOpen.CncIsAttached);
            Assert.AreEqual(ms.Controller, msOpen.Controller);
        }
    }
}
