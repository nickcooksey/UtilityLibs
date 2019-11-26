using ICNCLib;

namespace CNCLib
{

    /// <summary>
    /// holds string values for various CNC G code words 
    /// </summary>
    public class CNCMachineCode:IMachineCode
    {
        public string InverseFeed { get; set; }
        public string Rapid { get; set; }
        public string LinearMove { get; set; }
        public string FiveAxis { get; set; }
        public string CwArc { get; set; }
        public string CcwArc { get; set; }
        public string Delay { get; set; }
        public string Absolute { get; set; }
        public string Relative { get; set; }
        public int StartingLineNumber { get; set; }
        public int LineNumberInc { get; set; }
        public int RotaryAxisPrecision { get; set; }
        public  int LinearAxisPrecision { get; set; }
        public int FeedratePrecision { get; set; }
        public  string LinearAxisFormat { get; set; }
        public string RotaryAxisFormat { get; set; }
        public string FeedrateFormat { get; set; }
        public string LineNumberPrefix { get; set; }
        public string DelayAmountPrefix { get; set; }
        public string FeedratePrefix { get; set; }
        public string EndofProg { get; set; }
       
        public string[] AxisNames { get; set; }
        public int AxisCount { get; set; }
        public string Sp { get; set; }
        public string ComStart { get; set; }
        public string ComEnd { get; set; }
        public string HeaderStart { get; set; }
        public string HeaderEnd { get; set; }
        public int CommentMaxLength { get; set; }
        public char[] ForbiddenChars { get; set; }
        public double DelayScaleFactor { get; set; }
        public string DelayStringFormat { get; set; }

        public string McodeFilename { get; set; }
        public  FeedrateUnits FeedrateUnits { get; set; }
        public  FeedrateUnits InverseFeedUnits { get; set; }

        //  private MCodeDictionary _mCodeDictionary;

        public CNCMachineCode()
        {
            LoadDefMachineSettings();
            // _mCodeDictionary = new MCodeDictionary();

        }
        public CNCMachineCode(string machineFileName)
        {
            LoadMachineFile(machineFileName);
            // _mCodeDictionary = new MCodeDictionary(McodeFilename);

        }

        private void LoadMachineFile(string fileName)
        {
            if (fileName != null && fileName != "" && System.IO.File.Exists(fileName))
            {
                CNCMachineCodeFile.Open();
            }
            else
            {
                LoadDefMachineSettings();
            }

        }
        private void LoadDefMachineSettings()
        {
            InverseFeed = "G32";
            Rapid = "G0";
            LinearMove = "G01";
            FiveAxis = "G01";
            CwArc = "G02";
            CcwArc = "G03";
            Delay = "G04";
            Relative = "G91";
            Absolute = "G90";
            LineNumberPrefix = "N";
            DelayAmountPrefix = "K";
            FeedratePrefix = "F";
            EndofProg = "M30";
            
           
            Sp = " ";
            ComStart = ";";
            ComEnd = "";
            HeaderStart = "(";
            HeaderEnd = ",MX)";
            CommentMaxLength = 20;
            DelayScaleFactor = 100;
            DelayStringFormat = "N0";
            McodeFilename = "";
            AxisNames = new string[] { "X", "Y", "Z","A", "B", "C" };
            ForbiddenChars = new char[] { ' ', '/' };
          
            RotaryAxisPrecision = 3;
            LinearAxisPrecision = 4;
            FeedratePrecision = 2;
            StartingLineNumber = 100;
            LineNumberInc = 2;
            RotaryAxisFormat = "f" + RotaryAxisPrecision.ToString();
            FeedrateFormat = "f"+FeedratePrecision.ToString();
            LinearAxisFormat = "f"+LinearAxisPrecision.ToString();
            FeedrateUnits = FeedrateUnits.InPerMin;
            InverseFeedUnits = FeedrateUnits.InverseMins;
        }
       
    }
}
