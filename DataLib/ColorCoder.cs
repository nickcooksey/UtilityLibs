using IDataLib;
using System;
using System.Collections.Generic;

namespace DataLib
{
    public class ColorRange
    {
        private double Min { get; set; }
        private double Max { get; set; }
        private System.Drawing.Color Color { get; set; }
    }
    public class ColorCoder : IColorCoder
    {
        public COLORCODE COLORCODE { get; private set; }

        private double min, max;

        public ColorCoder(COLORCODE cOLORCODE)
        {
            COLORCODE = cOLORCODE;
            colorRanges = new List<ColorRange>();
        }
        public void SetValues(double min, double max)
        {
            try
            {
                this.min = min;
                this.max = max;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<ColorRange> colorRanges;
        public System.Drawing.Color MapColor(double value)
        {
            try
            {
                switch (COLORCODE)
                {
                    default:
                    case COLORCODE.RAINBOW:
                        return MapRainbowColor(value);
                    case COLORCODE.MONO:
                        return MapMonoColor();
                    case COLORCODE.TOL_RGB:
                        return MapToleranceRGBColor(value);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private System.Drawing.Color MapMonoColor()
        {
            try
            {
                return System.Drawing.Color.FromArgb(125, 125, 125);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private System.Drawing.Color MapGreenRedColor(double value)
        {
            try
            {
                if ((Math.Abs(value) > Math.Abs(max) || (Math.Abs(value) < Math.Abs(min))))
                {
                    return System.Drawing.Color.FromArgb(255, 125, 125);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(125, 255, 125);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private System.Drawing.Color MapToleranceRGBColor(double value)
        {
            try
            {
                if (Math.Abs(value) > Math.Abs(max))
                {
                    return System.Drawing.Color.FromArgb(255, 125, 125);
                }
                else if (Math.Abs(value) < Math.Abs(min))
                {
                    return System.Drawing.Color.FromArgb(125, 125, 255);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(125, 255, 125);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public double GetRedValue()
        {
            try
            {
                return min;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetYellowValue()
        {
            try
            {
                return 0.25 * (max - min) + min;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetGreenValue()
        {
            try
            {
                return 0.5 * (max - min) + min;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetAquaValue()
        {
            try
            {
                return 0.75 * (max - min) + min;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetBlueValue()
        {
            try
            {
                return max;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private System.Drawing.Color MapRainbowColor(double value)
        {
            try
            {
                // Convert into a value between 0 and 1023.
                int int_value = (int)(1023 * (value - min) / (max - min));
                byte red = 100;
                byte green = 100;
                byte blue = 100;
                // Map different color bands.
                if (int_value < 256)
                {
                    // Red to yellow. (255, 0, 0) to (255, 255, 0).
                    red = 255;
                    green = (byte)int_value;
                    blue = 0;
                }
                else if (int_value < 512)
                {
                    // Yellow to green. (255, 255, 0) to (0, 255, 0).
                    int_value -= 256;
                    red = (byte)(255 - int_value);
                    green = 255;
                    blue = 0;
                }
                else if (int_value < 768)
                {
                    // Green to aqua. (0, 255, 0) to (0, 255, 255).
                    int_value -= 512;
                    red = 0;
                    green = 255;
                    blue = (byte)int_value;
                }
                else
                {
                    // Aqua to blue. (0, 255, 255) to (0, 0, 255).
                    int_value -= 768;
                    red = 0;
                    green = (byte)(255 - int_value);
                    blue = 255;
                }
                return System.Drawing.Color.FromArgb(red, green, blue);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private System.Drawing.Color MapMonoRedColor(double value)
        {
            try
            {
                if (Math.Abs(value) > Math.Abs(max))
                {
                    return System.Drawing.Color.FromArgb(255, 125, 125);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(125, 125, 125);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
