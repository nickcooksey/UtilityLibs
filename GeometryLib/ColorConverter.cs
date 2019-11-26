using System;

namespace GeometryLib
{
    public class ColorConverter
    {/// <summary>
     /// Convert to RGB Color from DXF color
     /// </summary>
     /// <param name="c">DXF Color 0-7</param>
     /// <returns></returns>
        public static System.Drawing.Color ToColor(int c)
        {
            System.Drawing.Color color = new System.Drawing.Color();
            switch (c)
            {
                case 0://Black = 0,
                    color = System.Drawing.Color.Black;
                    break;
                case 1://      Red = 1,
                    color = System.Drawing.Color.Red;
                    break;
                case 2: //      Yellow = 2,
                    color = System.Drawing.Color.Yellow;
                    break;
                case 3://      Green = 3,
                    color = System.Drawing.Color.Green;
                    break;
                case 4://      Cyan = 4,
                    color = System.Drawing.Color.Cyan;
                    break;
                case 5://      Blue = 5,
                    color = System.Drawing.Color.Blue;
                    break;
                case 6://      Magenta = 6,
                    color = System.Drawing.Color.Magenta;
                    break;
                case 7://      White = 7,
                    color = System.Drawing.Color.White;
                    break;
                case 8://      Grey = 8,   
                default:
                    color = System.Drawing.Color.Gray;
                    break;
            }
            return color;
        }
        public static int GetDxfCustomColorNumber(System.Drawing.Color color)
        {
            try
            {
                var dxfColor = GetDxfCustomColor(color);
                return dxfColor.ToArgb();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static System.Drawing.Color GetDxfCustomColor(System.Drawing.Color color)
        {
            try
            {
                System.Drawing.Color dxfColor;

                byte red = color.R;
                byte green = color.G;
                byte blue = color.B;
                byte a = 0;
                dxfColor = System.Drawing.Color.FromArgb(a, red, green, blue);
                return dxfColor;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static System.Drawing.Color ToColor(byte red, byte green, byte blue)
        {
            return System.Drawing.Color.FromArgb(red, green, blue);
        }

    }
}
