using ICNCLib;

namespace CNCLib
{
    public class LinearAxis : Axis
    {


        public double PositionInch(double counts)
        {
            double pos = (counts - encoderOffset) / encoderCtsPerUnit;
            return pos;
        }
        public double PositionCounts(double positionInch)
        {
            double counts = (positionInch * encoderCtsPerUnit) + encoderOffset;
            return counts;
        }
        public LinearAxis(int axisNumber, string name, string plcVariable, uint encoderCtsPerInch, uint encoderOffset)
            : base(axisNumber, name, AxisType.Linear, plcVariable)
        {
            encoderCtsPerUnit = encoderCtsPerInch;
            this.encoderOffset = encoderOffset;
            init();
        }
        public LinearAxis(int axisNumber, string name, string plcVariable)
            : base(axisNumber, name, AxisType.Linear, plcVariable)
        {
            init();
        }
        public LinearAxis() : base(0, "", AxisType.Linear, "")
        {
            init();
        }
        void init()
        {
            DecimalPlaces = 4;
        }
    }
}
