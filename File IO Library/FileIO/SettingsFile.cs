using System;

namespace FileIOLib
{
    public abstract class SettingsFile<T>:IFileIOLib.ISettingsFile<T>
    {

        public static string Extension;
        protected static string DefaultFileName;

        /// <summary>
        /// opens xml serialized file and returns obj
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T Open(string fileName)
        {
            try
            {
                
                if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
                {
                  return OpenJson(fileName);
                }
                else
                {
                    throw new ArgumentException(fileName + " File not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        public void Save(T obj,string filename)
        {
            try
            {
                if (obj == null )
                {
                    throw new ArgumentException("Could not save undefined object");
                }
                else
                {
                    if (string.IsNullOrEmpty(filename))
                    {
                        throw new ArgumentException("Could not save file without filename.");
                    }
                    else
                    { 
                        var file = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                        using (var writer = new System.IO.StreamWriter(filename))
                        {
                            writer.Write(file);
                        }
                    }
                }                    
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private T OpenJson(string filename)
        {
            try
            {
                T obj;
                using (var reader = new System.IO.StreamReader(filename))
                {
                    string file = reader.ReadToEnd();
                    obj=   Newtonsoft.Json.JsonConvert.DeserializeObject<T>(file);
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
     
    }
}
