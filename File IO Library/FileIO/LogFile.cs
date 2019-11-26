using System;
using System.Collections.Generic;

namespace FileIOLib
{
    public sealed class LogFile
    {
        private static string _fileName;
        private static readonly LogFile _logfile = new LogFile();
        public string GetFileName()
        {
            return _fileName;
        }
        public static LogFile GetLogFile()
        {
            return _logfile;
        }
        public static LogFile GetNewLogFile()
        {
            string directory = "Log_files";
            _fileName = @"Log_Files\Logfile" + DateTime.Now.ToFileTime().ToString() + ".txt";
            System.IO.Directory.CreateDirectory(directory);

            return new LogFile(_fileName);
        }
        private LogFile(string filename)
        {
            _fileName = filename;
        }
        private LogFile()
        {
            _fileName = "LogFile.txt";
        }
        public void ClearLog()
        {
            System.IO.File.Delete(_fileName);

        }
        public List<string> GetContents()
        {
            try
            {
                var fileContents = new List<string>();
                if (System.IO.File.Exists(_fileName))
                    fileContents = FileIO.ReadTextFile(_fileName);

                return fileContents;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void SaveMessage(string message)
        {
            try
            {

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_fileName, append: true))
                {

                    sw.Write(System.DateTime.Now.ToShortDateString() + ":");
                    sw.WriteLine(System.DateTime.Now.ToLongTimeString());
                    sw.WriteLine(message);
                    sw.WriteLine("******");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void SaveMessage(Exception ex)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_fileName, append: true))
                {

                    sw.Write(System.DateTime.Now.ToShortDateString() + ":");
                    sw.WriteLine(System.DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Exception: " + ex.ToString());
                    if (ex.InnerException != null)
                    {
                        sw.WriteLine(ex.InnerException.ToString());
                    }
                    sw.WriteLine("StackTrace: " + ex.StackTrace);
                    sw.WriteLine("******");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
