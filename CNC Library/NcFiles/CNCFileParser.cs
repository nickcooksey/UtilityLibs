using FileIOLib;
using System;
using System.Collections.Generic;
using ICNCLib;

namespace CNCLib
{
    public class CNCFileParser:ICncFileParser
    {
        public IToolpath CreatePath(string fileName)
        {
            try
            {
                var file = new List<string>();
                NCFileType fileType = NCFileType.NCMachiningFile;

                if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
                {
                    file = FileIO.ReadTextFile(fileName);
                    fileType = selectFileType(fileName);
                }
                else
                {
                    throw new System.IO.FileNotFoundException("file cannot be found");
                }
                return CreatePath(file, fileName);

            }
            catch
            {
                throw;
            }

        }
        private IToolpath CreatePath(List<string> file, string filename)
        {
            try
            {
                IToolpath toolpath = new ToolPath();
                NCFileType fileType = selectFileType(filename);
                if (file.Count > 0)
                {
                    switch (fileType)
                    {
                        case NCFileType.NCIFile:
                            NciFileParser ncifile = new NciFileParser();
                            toolpath = ncifile.ParsePath(file);
                            break;

                        case NCFileType.NCMachiningFile:

                            NcFileParser ncfile = new NcFileParser();
                            toolpath = ncfile.ParsePath(file);
                            break;
                    }
                }
                else
                {
                    throw new Exception("NC file is empty");
                }
                return toolpath;
            }
            catch (Exception)
            {

                throw;
            }

        }
      
        private static List<string> ncFileExtensions = new List<string>();
        private static string nciFileExt = "NCI";
        private static NCFileType selectFileType(string fileName)
        {
            string fileExt = System.IO.Path.GetExtension(fileName);
            fileExt = fileExt.ToUpper();
            if (fileExt.Contains(nciFileExt))
                return NCFileType.NCIFile;
            else
                return NCFileType.NCMachiningFile;
        }

    }
}
