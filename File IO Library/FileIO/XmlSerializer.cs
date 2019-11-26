using System;

namespace FileIOLib
{
    //serializes and deserializes classes to xml files
    //classes and subclasses MUST have a parameterless constructor for this to work
    //otherwise error reflecting type error is thrown
    public class XmlSerializer<T>
    {
        /// <summary>
        /// opens searialized xml object and returns object
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <param name="fileName">filename of object</param>
        /// <returns></returns>
        public T OpenXML(string fileName)
        {
            try
            {
                T obj;

                if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
                    {
                        System.IO.TextReader reader = new System.IO.StreamReader(fs);
                        obj = (T)serializer.Deserialize(reader);
                    }

                }
                else
                {
                    obj = default(T);
                }
                return obj;
            }
            catch (InvalidOperationException)
            {
                return default(T);
            }

            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// saves object as serialized xml file
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object to save</param>
        /// <param name="fileName">filename to save</param>
        public void SaveXML(T obj, string fileName)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileName))
                {
                    serializer.Serialize(streamWriter, obj);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
