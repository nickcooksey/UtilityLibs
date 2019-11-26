using System.Collections.Generic;
using System.Text;
using ICNCLib;

namespace CNCLib
{
    /// <summary>
    /// builds NC file from a modelPath or toolpath list
    /// </summary>
    public class NcFileBuilder
    {
        private INcMachine ncMachine;
        private IMachineCode machineCodes;
        private int currentLineNumber;
        private bool invertF;
        public NcFileBuilder(INcMachine machine)
        {
            ncMachine = machine;
            this.machineCodes = machine.MachineCode;
            currentLineNumber = machine.MachineCode.StartingLineNumber;
        }
        public List<string> Build(ToolPath path, bool invertFeedrates, bool fixRollovers, bool fixWrapArounds, List<string> header)
        {
            invertF = invertFeedrates;
            var file = new List<string>();
            file.AddRange(header);
            foreach (var pe in path)
            {
                file.Add(BuildLine(pe));
            }
            return file;
        }
        public List<string> Build(ToolPath path, bool invertFeedrates, List<string> header)
        {
            invertF = invertFeedrates;
            var file = new List<string>();
            file.AddRange(header);
            foreach (var pe in path)
            {
                file.Add(BuildLine(pe));
            }
            return file;
        }

        private string BuildLine(IPathEntity  pe)
        {
            string line = "";
            switch (pe.Type)
            {
                case BlockType.CCWARC:
                case BlockType.CWARC:                
                case BlockType.LINEAR:
                case BlockType.RAPID:
                    line = AddMove(pe);
                    break;
                case BlockType.COMMAND:
                    line = AddCommand(pe);
                    break;
                case BlockType.COMMENT:
                    line = AddComment(pe);
                    break;
                case BlockType.DELAY:                    
                    line = AddDelay(pe);
                    break;               
                 
            }
            return line;
        }

        private string AddDelay(IPathEntity pe)
        {
            var dpe = pe as DelayPathEntity;
            string delayString = machineCodes.DelayAmountPrefix + (dpe.Delay  * machineCodes.DelayScaleFactor).ToString(machineCodes.DelayStringFormat);
            return machineCodes.Delay  + delayString;
        }

        private string AddComment(IPathEntity pe)
        {
            var comment = pe.Comment;
            if (comment.Length > machineCodes.CommentMaxLength)
            {
                comment = comment.Substring(0, machineCodes.CommentMaxLength);
            }
            return machineCodes.ComStart + comment + machineCodes.ComEnd;

        }

        private string AddCommand(IPathEntity pe)
        {
            //TODO return mcode from string and state
            return "Command Not implemented";
            // return machine.getMcode(m, state);                        
        }

        private List<string> AddFooter()
        {
            List<string> footer = new List<string>();
            footer.Add(machineCodes.EndofProg);
            return footer;
        }

        private List<string> AddHeader(string ProgName, string[] comments)
        {
            foreach (char badC in machineCodes.ForbiddenChars)
            {
                ProgName.Replace(badC, '-');
            }
            List<string> header = new List<string>();
            string line = machineCodes.HeaderStart + ProgName + machineCodes.HeaderEnd;
            header.Add(line);

            return header;
        }

        private string MiscCommand(string command)
        {
            foreach (char badC in machineCodes.ForbiddenChars)
            {
                command.Replace(badC, '-');
            }
            return command;
        }
        private string AddMove(IPathEntity pe)
        {

            StringBuilder line = new StringBuilder();
            AppendLineNumber(ref line);
            AppendGCode(pe.Type, ref line);
            AppendPositions(pe, ref line);
            if (pe.Type != BlockType.RAPID)
            {
                AppendFeedrate(pe.Feedrate, ref line);
            }

            return line.ToString();
        }
        private void AppendLineNumber(ref StringBuilder line)
        {
            line.Append(machineCodes.LineNumberPrefix + machineCodes.StartingLineNumber + machineCodes.Sp);
        }
        private void AppendGCode(BlockType t, ref StringBuilder line)
        {
            if (invertF && (t == BlockType.LINEAR  ))
            {
                line.Append(machineCodes.InverseFeed  + machineCodes.Sp);
            }
            switch (t)
            {
                case BlockType.CCWARC:
                    line.Append(machineCodes.CcwArc  + machineCodes.Sp);
                    break;
                case BlockType.CWARC:
                    line.Append(machineCodes.CwArc  + machineCodes.Sp);
                    break;
                 
                case BlockType.LINEAR:
                    line.Append(machineCodes.LinearMove  + machineCodes.Sp);
                    break;
                case BlockType.RAPID:
                    line.Append(machineCodes.Rapid  + machineCodes.Sp);
                    break;
            }
        }
        private void AppendPositions(IPathEntity pe, ref StringBuilder line)
        {

            line.Append("X");
            line.Append(pe.Position.X.ToString(machineCodes.LinearAxisFormat) + machineCodes.Sp);
            line.Append("Y");
            line.Append(pe.Position.Y.ToString(machineCodes.LinearAxisFormat) + machineCodes.Sp);
            line.Append("Z");
            line.Append(pe.Position.Z.ToString(machineCodes.LinearAxisFormat) + machineCodes.Sp);
            line.Append("B");
            line.Append(pe.Position.Bdeg.ToString(machineCodes.RotaryAxisFormat) + machineCodes.Sp);
            line.Append("C");
            line.Append(pe.Position.Cdeg.ToString(machineCodes.RotaryAxisFormat) + machineCodes.Sp);

        }
        
        private void AppendFeedrate(IFeedrate f, ref StringBuilder line)
        {
            line.Append(machineCodes.FeedratePrefix + f.Value.ToString(machineCodes.FeedrateFormat));
        }
    }
}
