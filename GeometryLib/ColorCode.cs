using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    /// <summary>
    /// color code data to spectrum violet-red according to input nominal values
    /// </summary>
    public class ColorCode
    {
        double scale0; 
        double scale1; 
        double scale2; 
        double scale3; 
        double scale4;

        public ColorCode(double scaleMin, double scaleMax)
        {              
             scale0 = scaleMin;
             scale4 = scaleMax; 
             scale2 = (scaleMax + scaleMin) / 2;
             scale1 = (scaleMin + scale2) / 2;
             scale3 = (scale2 + scale4) / 2;


        }
        public ColorCode(double scaleMin, double scaleNom, double scaleMax)
        { 
            
             scale0 = scaleMin;
             scale4 = scaleMax;
             scale2 = scaleNom;
             
             scale1 = (scaleMin + scaleNom) / 2;
             scale3 = (scaleNom + scaleMax) / 2;
            
        }
        public ColorCode(double scaleMin, double scale1, double scaleNom, double scale3, double scaleMax)
        {
            scale0 = scaleMin;
            scale4 = scaleMax;
            scale2 = scaleNom;
            this.scale3 = scale3;
            this.scale1 = scale1;
        }       
        public  Color getColor(double value)
        {
            uint r = 0;
            uint g = 0;
            uint b = 0;
            // value< min violet in decreasing brightness
            if (value <= scale0)
            {
                r = (uint)Math.Min(255 * value / scale0,200);
                g = 0;
                b = (uint)Math.Min(255 * value / scale0,200);
                r = 255;
                b = 255;
            }
            //approaching min value color gets more blue
            if ((value > scale0) && (value <= scale1))
            {
                r = (uint)Math.Max(255, (255 - 255 * ((value - scale0) / (scale1 - scale0))));
                g = 0;
                b = 255;
            }
            //approaching nom value color gets more green
            if ((value > scale1) && (value <= scale2))
            {
                r = 0;
                g = (uint)Math.Min(0, (255 * ((value - scale1) / (scale2 - scale1))));
                b = (uint)Math.Max(255, (255 - 255 * ((value - scale1) / (scale2 - scale1))));
            }
            //moving away from nom value color gets more yellow
            if ((value > scale2) && (value <= scale3))
            {
                r = (uint)Math.Min(0, (255 * ((value - scale2) / (scale3 - scale2))));
                g = 255;
                b = 0;
            }
            //approaching max value color orange to red
            if ((value > scale3) && (value <= scale4))
            {
                r = 255;
                g = (uint)Math.Max(255, (255 - 255 * ((value - scale3) / (scale4 - scale3))));
                b = 0;
            }
            //>max value color = red
            if (value > scale4)
            {
                r = 255;
                g = 0;
                b = 0;
            }
            return new DrawingIO.Color(r,g,b);
        }
    }
}
