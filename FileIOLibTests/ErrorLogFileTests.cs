using FileIOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FileIOLibTests
{
    [TestClass]
    public class ErrorLogFileTests
    {
        private void createException(string testmessage)
        {
            try
            {

                throw new NullReferenceException(testmessage);
            }
            catch (Exception ex)
            {

                LogFile log = LogFile.GetLogFile();
                log.SaveMessage(ex);
                log.SaveMessage(testmessage);

            }
        }

        [TestMethod]
        public void LogFile_SaveMessage_MessageIsException_SavesExceptionMessage()
        {

            string testmessage = "Test message";
            createException(testmessage);
            LogFile log = LogFile.GetLogFile();
            string errorlogfilename = log.GetFileName();
            List<string> fileContents = FileIO.ReadTextFile(errorlogfilename);
            bool contentFound = false;
            foreach (string line in fileContents)
            {
                contentFound = line.Contains(testmessage);
                if (contentFound) break;
            }


            Assert.IsTrue(contentFound, testmessage + " not found");

        }
        [TestMethod]
        public void LogFilie_SaveMessage_MessageIsException_SavesException()
        {

            string testmessage = "Test message";
            createException(testmessage);
            LogFile log = LogFile.GetLogFile();
            string errorlogfilename = log.GetFileName();
            List<string> fileContents = FileIO.ReadTextFile(errorlogfilename);
            bool contentFound = false;
            foreach (string line in fileContents)
            {
                contentFound = line.Contains("System.NullReferenceException");
                if (contentFound) break;
            }

            Assert.IsTrue(contentFound, "exception not found");

        }
        [TestMethod]
        public void LogFile_SaveMessage_MessageIsText_savesText()
        {
            string testmessage = "test message";
            LogFile log = LogFile.GetLogFile();
            log.SaveMessage(testmessage);
            string errorlogfilename = log.GetFileName();
            List<string> fileContents = FileIO.ReadTextFile(errorlogfilename);
            bool contentFound = fileContents.Contains(testmessage);

            Assert.IsTrue(contentFound, testmessage + " not found");
        }
        [TestMethod]
        public void LogFile_Ctor_logsAreEqual()
        {
            LogFile log1 = LogFile.GetLogFile();
            LogFile log2 = LogFile.GetLogFile();
            string testmessage1 = "test message1";
            string testmessage2 = "test message2";
            string fileName = log1.GetFileName();

            log1.SaveMessage(testmessage1);
            log2.SaveMessage(testmessage2);
            List<string> fileContents = FileIO.ReadTextFile(fileName);
            bool content1Found = fileContents.Contains(testmessage1);
            bool content2Found = fileContents.Contains(testmessage2);
            Assert.AreEqual(log1, log2);
            Assert.IsTrue(content1Found);
            Assert.IsTrue(content2Found);
        }
        [TestMethod]
        public void LogFile_clearLog_logisClear()
        {
            LogFile log = LogFile.GetLogFile();
            string testmessage1 = "test message1";
            log.SaveMessage(testmessage1);

            string fileName = log.GetFileName();
            List<string> fileContents = log.GetContents();
            bool contentFound = fileContents.Contains(testmessage1);
            Assert.IsTrue(contentFound, "before clearing");
            log.ClearLog();
            fileContents = log.GetContents();
            contentFound = fileContents.Contains(testmessage1);
            Assert.IsFalse(contentFound, "after clearing");
        }
    }
}
