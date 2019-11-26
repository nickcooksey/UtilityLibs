using ICNCLib;

    namespace CNCLib
{
    public class CNCMachineCodeFile
    {
        private static string xmlFileName = "CNCMachineCode.xml";
        public static CNCMachineCode Open()
        {

            if (xmlFileName != null && xmlFileName != "" && System.IO.File.Exists(xmlFileName))
            {
                var xmls = new FileIOLib.XmlSerializer<CNCMachineCode>();
                CNCMachineCode mc = xmls.OpenXML(xmlFileName);
                if (mc == null)
                {
                    return new CNCMachineCode();
                }
                else
                {
                    return mc;
                }
            }

            return new CNCMachineCode();

        }
        public static void Save(CNCMachineCode obj)
        {
            var xmls = new FileIOLib.XmlSerializer<CNCMachineCode>();
            xmls.SaveXML(obj, xmlFileName);
        }
    }
}
