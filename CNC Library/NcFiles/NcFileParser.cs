using GeometryLib;
using System;
using System.Collections.Generic;
using ICNCLib;

namespace CNCLib
{
    /// <summary>
    /// parses NC text file into a toolpath list
    /// </summary>
    public class NcFileParser
    {
        private string invFCode;
        private string n;
        private string g;
        private string x;
        private string y;
        private string z;
        private string i;
        private string j;
        private string k;
        private string a;
        private string b;
        private string c;
        private string m;
        private string f;
        private string[] splitters;
        private string[] pathGCodes;
        private string[] mCodes;
        private string[] pathStart;
        private string[] pathEnd;
      
        private ToolPath path;
        private bool jeton;
        public NcFileParser()
        {
            path = new ToolPath();
            splitters = new string[] { " " };
            pathGCodes = new string[] { "G0", "G00", "G1", "G01", "G2", "G02", "G3", "G03", "G4", "G04" };
            mCodes = new string[] { "M61", "M62", "M63", "M64", "M65", "M66" };
            pathStart = new string[] { "START", "M61 " };
            pathEnd = new string[] { "END", "M62" };
            invFCode = "G93";
            //_mcodeDictionary = new MCodeDictionary();
            //mCodes = machineMCodes;
            n = "N";

            g = "G";
            x = "X";
            y = "Y";
            z = "Z";
            i = "I";
            j = "J";
            k = "K";
            a = "A";
            b = "B";
            c = "C";
            m = "M";
            f = "F";
        }

        public ToolPath ParsePath(List<string> file)
        {
            jeton = false;
            int pathNumber = 0;
            foreach (string line in file)
            {
                BlockType blockT;

                if (!jeton)
                    jeton = IsPathStart(line);
                else
                    jeton = !IsPathEnd(line);

                if (IsPath(line, out blockT))
                {
                    IPathEntity pe = parseString(line, blockT, jeton);
                     

                    path.Add(pe);
                    pathNumber++;
                }
            }
            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].JetOn && !path[i - 1].JetOn)
                {
                    path[i - 1].JetOn = true;
                    path[i - 1].Feedrate.Value = path[i].Feedrate.Value;
                }
            }
            //calc arc sweep angle and travel time for each segment


           CalcPathLengths();
            FillMissingCoords();
            return path;
        }
        private bool IsPath(string ncLine, out BlockType blockT)
        {
            int result;
            bool isAPath = false;
            string[] splitLine = ncLine.Split(splitters, StringSplitOptions.None);
            string trimStr = "";
            blockT = BlockType.LINEAR;
            foreach (string splitStr in splitLine)
            {

                if (ContainsPathCode(splitStr, out g))
                {
                    trimStr = splitStr.Substring(splitStr.IndexOf(g) + 1);
                    trimStr = trimStr.Trim();
                    if (Int32.TryParse(trimStr, out result))
                    {
                        switch (result)
                        {
                            case 0:
                                isAPath = true;
                                blockT = BlockType.RAPID;
                                break;

                            case 1:
                                isAPath = true;
                                blockT = BlockType.LINEAR;
                                break;

                            case 2:
                                isAPath = true;
                                blockT = BlockType.CWARC;
                                break;

                            case 3:
                                isAPath = true;
                                blockT = BlockType.CCWARC;
                                break;

                            case 4:
                                isAPath = true;
                                blockT = BlockType.DELAY;
                                break;
                        }
                    }
                }
            }
            return isAPath;
        }

        private IPathEntity parseString(string line, BlockType blockT, bool jetIsOn)
        {
            bool invertedFeed = IsFeedrateInverted(line);

            switch (blockT)
            {
                case BlockType.RAPID:
                    return ParseLine(line, blockT, jetIsOn, invertedFeed);
                case BlockType.LINEAR:
                    return ParseLine(line, blockT, jetIsOn, invertedFeed);
                case BlockType.CCWARC:
                    return ParseArc(line, blockT, jetIsOn, invertedFeed);
                case BlockType.CWARC:
                    return ParseArc(line, blockT, jetIsOn, invertedFeed);
                case BlockType.DELAY:
                    return ParseDelay(line, blockT, jetIsOn, invertedFeed);
                default:
                    return new PathEntity();
            }
        }
        private bool IsPathEnd(string line)
        {
            return ContainsCode(pathEnd, line);
        }
        private bool IsPathStart(string line)
        {
            return ContainsCode(pathStart, line);
        }
        private bool ContainsCode(string[] codes, string line)
        {
            string lu = line.ToUpper();
            foreach (string code in codes)
            {
                string cu = code.ToUpper();
                if (lu.Contains(cu))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFeedrateInverted(string line)
        {
            if (line.Contains(invFCode))
            {
                return true;
            }
            return false;
        }
        private void CalcPathLengths()
        {
             
            double cumulativeTime = 0;
            for (int i = 1; i < path.Count; i++)
            {
                path[i].Length = path[i].Position.DistanceTo(path[i - 1].Position);
                if (path[i] is ArcPathEntity)
                {
                    ArcPathEntity arc = path[i] as ArcPathEntity;
                    CalcSweepAngle(ref arc, path[i - 1].Position);

                }
                path[i].TravelTime = path[i].Feedrate.MoveTimeSeconds(path[i].Length);
                cumulativeTime += path[i].TravelTime;
                path[i].CumulativeTime = cumulativeTime;
            }
        }
        private void FillMissingCoords()
        {
            for (int i = 1; i < path.Count; i++)
            {
                if ((path[i].Type != BlockType.RAPID) && (!path[i].ContainsF))
                    path[i].Feedrate.Value = path[i - 1].Feedrate.Value;
                if (!path[i].ContainsX)
                    path[i].Position.X = path[i - 1].Position.X;
                if (!path[i].ContainsY)
                    path[i].Position.Y = path[i - 1].Position.Y;
                if (!path[i].ContainsZ)
                    path[i].Position.Z = path[i - 1].Position.Z;
            }
        }
        //private List<MCode> Mcodes(string ncLine)
        //{
        //    List<MCode> mcodeList = new List<MCode>();
        //    string[] splitLine = ncLine.Split(splitters, StringSplitOptions.None);
        //    foreach (string splitStr in splitLine)
        //    {
        //        string s = splitStr.ToUpper();
        //        if (s.Contains(m))
        //        {
        //            string num = s.Substring(s.IndexOf('M') + 1);
        //            int mnum = -1;
        //            if (int.TryParse(num, out mnum))
        //            {

        //            }
        //        }
        //    }
        //    return mcodeList;
        //}
        private bool ContainsPathCode(string s, out string gcode)
        {
            s = s.ToUpper();
            gcode = "";
            foreach (string code in pathGCodes)
            {
                if (s.Contains(code))
                {
                    gcode = code;
                    return true;
                }
            }
            return false;
        }
        private bool ContainsMCode(string s, out string mcode)
        {
            s = s.ToUpper();
            mcode = "";
            foreach (string code in mCodes)
            {
                if (s.Contains(code))
                {
                    mcode = code;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// return true and blocktype if line is a g-code line
        /// </summary>
        /// <param name="ncLine">line from program</param>
        /// <param name="blockT">block type</param>
        /// <returns>true if line is part of path</returns>

        private void CalcSweepAngle(ref ArcPathEntity arc, IMachinePosition startPoint)
        {
            Vector3 vRadius = new Vector3();
            double startAngle = 0;
            double endAngle = 0;
            double radius = 0;
            double sweep = 0;

            double dxprev = 0;
            double dyprev = 0;
            double dzprev = 0;

            Vector3 vEnd = new GeometryLib.Vector3(arc.Position.X, arc.Position.Y, arc.Position.Z);
            Vector3 vStart = new Vector3(startPoint.X, startPoint.Y, startPoint.Z);
            switch (arc.ArcType)
            {
                case ArcSpecType.IJKAbsolute:

                    arc.CenterPoint = new GeometryLib.Vector3(arc.Position.X - arc.Icoordinate, arc.Position.Y - arc.Jcoordinate, arc.Position.Z - arc.Kcoordinate);
                    arc.Radius = vEnd.Length;
                    radius = arc.Radius;
                    dxprev = startPoint.X - arc.Icoordinate;
                    dyprev = startPoint.Y - arc.Jcoordinate;
                    dzprev = startPoint.Z - arc.Kcoordinate;

                    break;
                case ArcSpecType.Radius:

                    break;
                case ArcSpecType.NCI:
                    vRadius = new Vector3(arc.Position.X - arc.CenterPoint.X, arc.Position.Y - arc.CenterPoint.Y, arc.Position.Z - arc.CenterPoint.Z);
                    arc.Radius = vRadius.Length;
                    break;
                default:
                case ArcSpecType.IJKRelative:
                    vRadius = new GeometryLib.Vector3(arc.Icoordinate, arc.Jcoordinate, arc.Kcoordinate);
                    arc.Radius = vRadius.Length;
                    arc.CenterPoint = new Vector3(startPoint.X + arc.Icoordinate, startPoint.Y + arc.Jcoordinate, startPoint.Z + arc.Kcoordinate);
                    break;


            }
            switch (arc.ArcPlane)
            {
                case ArcPlane.XY:
                    startAngle = Math.Atan2(vStart.Y - arc.CenterPoint.Y, vStart.X - arc.CenterPoint.X);
                    endAngle = Math.Atan2(vEnd.Y - arc.CenterPoint.Y, vEnd.X - arc.CenterPoint.X);
                    break;
                case ArcPlane.XZ:
                    startAngle = Math.Atan2(vStart.Z - arc.CenterPoint.Z, vStart.X - arc.CenterPoint.X);
                    endAngle = Math.Atan2(vEnd.Z - arc.CenterPoint.Z, vEnd.X - arc.CenterPoint.X);
                    break;
                case ArcPlane.YZ:
                    startAngle = Math.Atan2(vStart.Z - arc.CenterPoint.Z, vStart.Y - arc.CenterPoint.Y);
                    endAngle = Math.Atan2(vEnd.Z - arc.CenterPoint.Z, vEnd.Y - arc.CenterPoint.Y);
                    break;

            }
            sweep = endAngle - startAngle;

            if (arc.Type == BlockType.CWARC)
            {
                sweep = 2 * Math.PI - sweep;
                if (sweep > 0)
                {
                    sweep = -1 * sweep;
                }
            }
            else
            {
                if (sweep < 0)
                {
                    sweep = Math.Abs(sweep);
                }
            }

            if (arc.FullArcFlag)
            {
                sweep = 2 * Math.PI;
            }

            arc.SweepAngle = sweep;
            arc.StartAngleRad = startAngle;
        }
        /// <summary>
        /// parse string line from nc file into path ARC entity
        /// </summary>
        /// <param name="ncLine">line from NC file</param>
        /// <param name="blockT">Block type</param>
        /// <returns>ArcPathEntity</returns>
        private ArcPathEntity ParseArc(string ncLine, BlockType blockT, bool jeton, bool invertedF)
        {
            var ent = new ArcPathEntity(blockT);
            ent.JetOn = jeton;
             
            string[] splitLine = ncLine.Split(splitters, StringSplitOptions.None);

            foreach (string str in splitLine)
            {
                if (str.Contains(n))
                {
                    ent.LineNumber = ParseInt(str, n);
                }
                if (str.Contains(x))
                {
                    ent.Position.X = ParseDouble(str, x);
                    ent.ContainsX = true;
                }
                if (str.Contains(y))
                {
                    ent.Position.Y = ParseDouble(str, y);
                    ent.ContainsY = true;
                }
                if (str.Contains(z))
                {
                    ent.Position.Z = ParseDouble(str, z);
                    ent.ContainsZ = true;
                }
                if (str.Contains(i))
                {
                    ent.Icoordinate = ParseDouble(str, i);
                }
                if (str.Contains(j))
                {
                    ent.Jcoordinate = ParseDouble(str, j);
                }
                if (str.Contains(k))
                {
                    ent.Kcoordinate = ParseDouble(str, k);
                }
                if (str.Contains(f))
                {
                   
                    ent.ContainsF = true;

                    ent.Feedrate = GetFeedrate(str, invertedF);
                }
            }
            ent.Type = blockT;

            return ent;
        }
        Feedrate GetFeedrate(string str,bool invertedF)
        {
            try
            {
                var Value = ParseDouble(str, f);
                Feedrate feedrate;
                if (invertedF)
                {
                    feedrate = new Feedrate(FeedrateUnits.SecPerMove);
                }
                else
                {
                    feedrate = new Feedrate(FeedrateUnits.InPerMin);
                }
                feedrate.Value = Value;
                return feedrate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private DelayPathEntity ParseDelay(string ncLine, BlockType blockT, bool jeton, bool invertedF)
        {
            var dp = new DelayPathEntity();
            string[] splitLine = ncLine.Split(splitters, StringSplitOptions.None);
            foreach (string str in splitLine)
            {
                if (str.Contains(n))
                {
                    dp.LineNumber = ParseInt(str, n);
                }
                if (str.Contains(k))
                {
                    dp.Delay = ParseDouble(str, k);
                }
            }
            return dp;
        }
        /// <summary>
        /// parse string line from nc file into path LINE entity
        /// </summary>
        /// <param name="ncLine">line from NC file</param>
        /// <param name="blockT">Block type</param>
        /// <returns>LinePathEntity</returns>
        private IPathEntity ParseLine(string ncLine, BlockType blockT, bool jeton, bool invertedF)
        {
            IPathEntity ent = new LinePathEntity(blockT);
            string[] splitLine = ncLine.Split(splitters, StringSplitOptions.None);
            ent.JetOn = jeton;
            
            ent.InputString = ncLine;

            foreach (string str in splitLine)
            {
                if (str.Contains(n))
                {
                    ent.LineNumber = ParseInt(str, n);
                }
                else if (str.Contains(x))
                {
                    ent.Position.X = ParseDouble(str, x);
                    ent.ContainsX = true;
                }
                else if (str.Contains(y))
                {
                    ent.Position.Y = ParseDouble(str, y);
                    ent.ContainsY = true;
                }
                else if (str.Contains(z))
                {
                    ent.Position.Z = ParseDouble(str, z);
                    ent.ContainsZ = true;
                }
                else if (str.Contains(b))
                {
                    ent.Position.Bdeg = ParseDouble(str, b);
                    ent.Type = BlockType.LINEAR;
                }
                else if (str.Contains(c))
                {
                    ent.Position.Cdeg = ParseDouble(str, c);
                    ent.Type = BlockType.LINEAR;
                }
                else  if (str.Contains(f))
                {
                    ent.Feedrate = GetFeedrate(str, invertedF);
                    ent.ContainsF = true;
                }


            }

            if (ent.Type == BlockType.LINEAR)
            {
                Vector3 pt = new Vector3(0, 0, 1);
                Vector3 origin = new Vector3(0, 0, 0);
                pt.RotateY(origin, PointCyl.ToRadians(ent.Position.Bdeg));
                pt.RotateZ(origin, PointCyl.ToRadians(ent.Position.Cdeg));
                ent.JetVector = pt;
            }
            else
            {
                ent.JetVector = new Vector3(0, 0, 1);
            }
            ent.Type = blockT;

            return ent;
        }
        private int ParseInt(string line, string word)
        {
            int r = 0;

            if (Int32.TryParse(line.Substring((line.IndexOf(word) + 1)), out r))
                return r;
            else
                return 0;
        }
        private double ParseDouble(string line, string word)
        {
            double r = 0;

            if (double.TryParse(line.Substring((line.IndexOf(word) + 1)), out r))
                return r;
            else
                return 0;
        }
    }


}
