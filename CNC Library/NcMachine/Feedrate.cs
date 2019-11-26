using ICNCLib;
using System;

namespace CNCLib
{
    
    public class Feedrate:IFeedrate
    {
        public bool Inverted { get; private set; }
        public double Value { get; set; }
        public FeedrateUnits Units { get; private set; }
         
        
        public Feedrate(FeedrateUnits units)
        {
            SetUnits(units);

        }
        public Feedrate(Feedrate f)
        {             
            SetUnits( f.Units);
            Value = f.Value;
        }
        void SetUnits(FeedrateUnits feedrateUnits)
        {
            try
            {
                Units = feedrateUnits;
                switch (Units)
                {
                    case FeedrateUnits.InverseMins:
                    case FeedrateUnits.MinPerMove:
                    case FeedrateUnits.SecPerMove:
                        Inverted = true;
                        break;
                    case FeedrateUnits.MmPerSec:
                    case FeedrateUnits.InPerSec:
                    case FeedrateUnits.MmPerMin:
                    case FeedrateUnits.InPerMin:
                    default:
                        Inverted = false;
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string AsNcString(INcMachine ncMachine)
        {
            try
            {
                string format =  ncMachine.MachineCode.FeedrateFormat;
                return ncMachine.MachineCode.FeedratePrefix + Value.ToString(format);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public void SetInverseFeedrate(double moveLength,double time)
        {
            try
            {
                if(moveLength==0 || time ==0)
                {
                    throw new ArgumentException("Move Length or time cannot be zero");
                }
                switch (Units)
                {
                    case FeedrateUnits.InverseMins:
                        Value = 1 / time;
                        break;
                    case FeedrateUnits.SecPerMove:
                    case FeedrateUnits.MinPerMove:
                        Value = time;
                        break;
                    
                }

                Value = time / moveLength;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public double MoveTimeSeconds(double moveLength)
        {
            try
            {
                double result = 0;
                if (Value != 0)
                {
                    switch (Units)
                    {
                        case FeedrateUnits.MinPerMove:
                            result = (moveLength * Value) / 60;
                            break;
                        case FeedrateUnits.SecPerMove:
                            result = Value;
                            break;
                        case FeedrateUnits.MmPerSec:
                        case FeedrateUnits.InPerSec:
                            result = moveLength / Value;
                            break;
                        case FeedrateUnits.MmPerMin:
                        case FeedrateUnits.InPerMin:
                        default:
                            result = (moveLength / Value) * 60;
                            break;
                    }
                }
                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
          
        }
    }
}
