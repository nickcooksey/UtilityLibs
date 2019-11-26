using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;
namespace CNCLib 
{
    public class NcElement:INcElement
    {
        public int LineNumber { get; set; }
        public string Content { get; set; }
        public bool IsComment { get; set; }
        public NcElement()
        {
            Content = "";
        }
        public virtual string AsNcString(INcMachine ncMachine)
        {
            try
            {
                string line="";
                if(IsComment)
                {
                    line += ncMachine.MachineCode.ComStart;
                }
                line +=  ncMachine.MachineCode.LineNumberPrefix + LineNumber.ToString() + " " + Content;
                return line;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
