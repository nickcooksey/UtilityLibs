using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNCLib;
using ICNCLib;
namespace CNCLibTests
{
    [TestClass]
    public class CommandDictTests
    {
        [TestMethod]
        public void CD_Ctor_notNull()
        {
            var cd = new CommandDictionary();
            Assert.IsNotNull(cd);
        }
        [TestMethod]
        public void CD_GetValueFromEnum_valueOK()
        {
            var cd = new CommandDictionary();
            var command = cd.AsNcString(MachineCommandType.JET_ON);
            StringAssert.Contains("JETON_Mcode", command);
        }
        [TestMethod]
        public void CD_GetValueFromStr_ValueOK()
        {
            var cd = new CommandDictionary();
            var command = cd.AsNcString("JET_ON");
            StringAssert.Contains("JETON_Mcode", command);
        }
        [TestMethod]
        public void CD_Getbadstring_UnknownCommand()
        {
            var cd = new CommandDictionary();
            string p = "probe";
            var command = cd.AsNcString(p);
            StringAssert.Contains(command, p+"_UnknownCommand");
        }
    }
}
