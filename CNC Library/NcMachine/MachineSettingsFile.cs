namespace CNCLib
{
    /// <summary>
    /// saves and opens machine settings file in xml 
    /// </summary>
    public class MachineSettingsFile
    {
        private static string xmlFileName = "MachineSettings.msx";
        public static MachineSettings Open()
        {

            return Open(xmlFileName);
        }
        public static MachineSettings Open(string fileName)
        {

            if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
            {
                var xmls = new FileIOLib.XmlSerializer<MachineSettings>();
                MachineSettings ms = xmls.OpenXML(fileName);
                if (ms == null)
                {
                    return new MachineSettings();
                }
                else
                {
                    return ms;
                }
            }
            else
            {
                return new MachineSettings();
            }
        }
        public static void Save(MachineSettings obj, string fileName)
        {
            var xmls = new FileIOLib.XmlSerializer<MachineSettings>();
            xmls.SaveXML(obj, fileName);
        }
        public static void Save(MachineSettings obj)
        {
            Save(obj, xmlFileName);
        }
    }
}
