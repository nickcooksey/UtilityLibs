using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    public class Point2D : DwgEntity     
    {
        double x;
        double y;
        

        
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        
        public Extents Extents
        {
            get
            {
                Extents ext = new Extents();
                ext.XMin = x;
                ext.YMin = y;
                ext.XMax = x;
                ext.YMax = y;
                ext.ZMax = 0;
                ext.ZMin = 0;

                return ext;
            }
        }

        public Point2D()
        {
            this.Type = EntityType.Point;
        }
        public Point2D(double xIn, double yIn)
        {
            this.Type = EntityType.Point;
            this.x = xIn;
            this.y = yIn;
        }
        public double DistanceTo(Point2D p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - this.x), 2) + Math.Pow((p2.Y - this.y), 2) ); 
        }
        public Point2D Transform(double dx, double dy)
        {
            Point2D pt = new Point2D();
            pt.X = this.x + dx;
            pt.Y = this.y + dy;
            
            return pt;
        }
        public Point2D RotateXY(Point2D pt, double angleDeg)
        {
            Point2D ptTrans = this.Transform(-1 * pt.X, -1 * pt.Y);

            Point2D ptRot = new Point2D();
            ptRot.X = ptTrans.x * Math.Cos(angleDeg * Math.PI / 180) + ptTrans.y * Math.Sin(angleDeg * Math.PI / 180);
            ptRot.Y = ptTrans.x * Math.Cos(angleDeg * Math.PI / 180) - ptTrans.y * Math.Sin(angleDeg * Math.PI / 180);
            Point2D ptOut = ptRot.Transform(pt.X, pt.Y);
            return ptRot;
        }
    }
}
