using System;
using ICNCLib;

namespace CNCLib
{



    /// <summary>
    /// contains rotary axis properties used to calc encoder counts for probe triggering
    /// </summary>
    public class RotaryAxis : Axis
    {

        /// <summary>
        /// converts counts to degrees
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public double PositionDeg(double counts)
        {
            double degs = (counts - encoderOffset) / encoderCtsPerUnit;
            while (degs < 0)
            {
                degs += 360;
            }
            degs %= 360;
            return degs;
        }
        /// <summary>
        /// converts degs to counts
        /// </summary>
        /// <param name="degs"></param>
        /// <returns></returns>
        public double PositionCounts(double degs)
        {
            while (degs < 0)
            {
                degs += 360;
            }
            degs %= 360;

            return Math.Round((degs * encoderCtsPerUnit) + encoderOffset);

        }
        public RotaryAxis(int axisNumber, string name, AxisType type, string plcVariable, uint encoderCtsPerRev, uint encoderOffset)
            : base(axisNumber, name, type, plcVariable)
        {
            this.encoderOffset = encoderOffset;
            encoderCtsPerUnit = encoderCtsPerRev;
            init();
        }
        public RotaryAxis(int axisNumber, string name, AxisType type, string plcVariable)
            : base(axisNumber, name, type, plcVariable)
        {
            encoderOffset = 0;
            encoderCtsPerUnit = 1080000;
            init();
        }
        public RotaryAxis()
        {
            encoderCtsPerUnit = 0;
            encoderOffset = 0;
            Type = AxisType.Rotary;
            init();
        }
        void init()
        {
            DecimalPlaces = 3;
        }
    }
}
