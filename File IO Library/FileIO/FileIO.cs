using System;
using System.Collections.Generic;

namespace FileIOLib
{
    public static class FileIO
    {
        public static string[] Splitter = { ":", ";", "," };
        public static string[] SplitCSVLine(string line)
        {
            var seps = new string[1] { "," };
            var words = line.Split(seps, StringSplitOptions.None);
            return words;
        }
        public static string[] Split(string line, string[] seperators)
        {
            var words = line.Split(seperators, StringSplitOptions.None);
            return words;
        }
        public static List<string> ReadTextFile(string fileName)
        {
            try
            {
                List<string> file = new List<string>();
                if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
                {
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            file.Add(line);
                        }
                    }
                }
                else
                {
                    throw new System.IO.FileNotFoundException("Unable to find " + fileName);
                }
                return file;
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///return <list> container of strings values from file with following format label delimiter value        
        public static List<string> ReadParamsTextFile(string fileName, char[] delimiters)
        {
            List<string> file = new List<string>(0);
            try
            {
                if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
                {
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
                    {
                        string line = "";

                        while ((line = sr.ReadLine()) != null)
                        {
                            int index = line.IndexOfAny(delimiters);
                            if (line != "")
                            {
                                file.Add(line.Substring(index + 1));
                            }
                        }
                    }
                }
                else
                {
                    throw new System.IO.FileNotFoundException("Unable to find "+fileName);
                }
                return file;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void Save(string[] file, string fileName)
        {
            try
            {
                if (fileName != "")
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
                    {
                        foreach (string line in file)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Filename cannot be empty");
                }

            }
            catch (System.IO.IOException)
            {
                throw new System.IO.IOException("File in use. Please close File and try again.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Save(List<String> file, string fileName)
        {
            try
            {
                if (fileName != "")
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
                    {
                        foreach (string line in file)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("filename cannot be empty.");
                }

            }
            catch (System.IO.IOException)
            {
                throw new System.IO.IOException("File in use. Please close File and try again.");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
