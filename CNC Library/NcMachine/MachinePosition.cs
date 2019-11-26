using GeometryLib;
using ICNCLib;

namespace CNCLib
{
    public class MachinePosition : IMachinePosition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Adeg { get; set; }
        public double Bdeg { get; set; }
        public double Cdeg { get; set; }


        public MachinePosition(IMachinePosition p)
        {
            Adeg = p.Adeg;
            Bdeg = p.Bdeg;
            Cdeg = p.Cdeg;
            X = p.X;
            Y = p.Y;
            Z = p.Z;

        }

        public MachinePosition(double x, double y, double z, double bDegs, double cDegs)
        {


            Bdeg =bDegs;
            Cdeg = cDegs;
            this.X = x;
            this.Y = y;
            this.Z = z;

        }
        public MachinePosition()
        {
          

        }
        public double DistanceTo(IMachinePosition pos)
        {
            var v1 = new Vector3(X, Y, Z);
            var v2 = new Vector3(pos.X, pos.Y, pos.Z);
            double d = v1.DistanceTo(v2);
            return d;
        }
        string rotaryFormat;
        string linearFormat;
        BlockType blockType;
        public string AsNcString(INcMachine ncMachine,IMachinePosition prevPosition,BlockType blockType, MoveType moveType)
        {
            rotaryFormat =  ncMachine.MachineCode.RotaryAxisFormat;
            linearFormat = ncMachine.MachineCode.LinearAxisFormat;
            this.blockType = blockType;
            if(moveType== MoveType.RELATIVE)
            {
                return AsRelativeMovePosition(ncMachine,prevPosition);
            }
            else
            {
                return AsAbsoluteMovePosition(ncMachine);
            }
        }

        string AsAbsoluteMovePosition(INcMachine ncMachine)
        {
            string line = "";
            line += ncMachine.MachineCode.LinearMove;
            switch (ncMachine.MachineGeometry)
            {
                case MachineGeometry.XA:
                    line =  "X" + X.ToString(linearFormat) + " " + "A" + Adeg.ToString(rotaryFormat);
                    break;
                case MachineGeometry.XYZ:
                    line = "X" + X.ToString(linearFormat) + " " + "Y" + Y.ToString(linearFormat) + " " + "Z" + Z.ToString(linearFormat);
                    break;
                case MachineGeometry.XYZBC:
                    line = "X" + X.ToString(linearFormat) + " " + "Y" + Y.ToString(linearFormat) + " " + "Z" + Z.ToString(linearFormat)
                        + " " + "B" + Bdeg.ToString(rotaryFormat) + " " + "C" + Cdeg.ToString(rotaryFormat);
                    break;
            }

            return line;
        }
         string AsRelativeMovePosition (INcMachine ncMachine,IMachinePosition previousPosition)
        {
            string line = "";
            switch (ncMachine.MachineGeometry)
            {
                case MachineGeometry.XA:
                    line = "X" + (X-previousPosition.X).ToString(linearFormat) + " " 
                        + "A" + (Adeg-previousPosition.Adeg).ToString(rotaryFormat);
                    break;
                case MachineGeometry.XYZ:
                    line = line = "X" + (X - previousPosition.X).ToString(linearFormat) + " "
                        + "Y" + (Y - previousPosition.Y).ToString(linearFormat) + " " 
                        + "Z" + (Z - previousPosition.Z).ToString(linearFormat);
                    break;
                case MachineGeometry.XYZBC:
                    line = line = "X" + (X - previousPosition.X).ToString(linearFormat) + " "
                        + "Y" + (Y - previousPosition.Y).ToString(linearFormat) + " " 
                        + "Z" + (Z - previousPosition.Z).ToString(linearFormat) + " " 
                        + "B" + (Bdeg - previousPosition.Bdeg).ToString(rotaryFormat)+" "
                        + "C" + (Cdeg - previousPosition.Cdeg).ToString(rotaryFormat);
                    break;
            }

            return line;
        }
        public void BuildFromString(string line)
        {
            var words = line.Split(',');
            foreach(var word in words)
            {
                int valueIndex = word.IndexOf('=')+1;
                string valueStr = word.Substring(valueIndex);
                if(word.Contains("X") && valueIndex>0)
                {                    
                    if(double.TryParse(valueStr, out double x))
                    {
                        X = x;
                    }
                }
                if (word.Contains("Y") && valueIndex > 0)
                {
                    if (double.TryParse(valueStr, out double y))
                    {
                        Y = y;
                    }
                }
                if (word.Contains("Z") && valueIndex > 0)
                {
                    if (double.TryParse(valueStr, out double z))
                    {
                        Z = z;
                    }
                }
                if (word.Contains("A") && valueIndex > 0)
                {
                    if (double.TryParse(valueStr, out double a))
                    {
                        Adeg = a;
                    }
                }
                if (word.Contains("B") && valueIndex > 0)
                {
                    if (double.TryParse(valueStr, out double b))
                    {
                        Bdeg = b;
                    }
                }
                if (word.Contains("C") && valueIndex > 0)
                {
                    if (double.TryParse(valueStr, out double c))
                    {
                        Cdeg = c;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "X=" + X.ToString() + "," + "Y=" + Y.ToString() + "," + "Z=" + Z.ToString() + "," + "A=" + Adeg.ToString() +
                "," + "B=" + Bdeg.ToString() + "," + "C=" + Cdeg.ToString();
        }
    }




}
