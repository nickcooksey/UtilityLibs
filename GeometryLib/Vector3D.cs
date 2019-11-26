using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    public class Vector3D : DwgEntity
    {

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            }
        }
        public Extents Extents
        {
            get
            {
                Extents ext = new Extents();
                ext.XMin = Math.Min(X,0);
                ext.YMin = Math.Min(Y,0);
                ext.XMax = Math.Max(X,0);
                ext.YMax = Math.Max(Y,0);
                ext.ZMax = Math.Max(Z,0);
                ext.ZMin = Math.Min(Z, 0);

                return ext;
            }
        }
        public static Vector3D XAxis
        {
            get
            {
                return new Vector3D(1, 0, 0);
            }
        }
        public static Vector3D YAxis
        {
            get
            {
                return new Vector3D(0, 1, 0);
            }
        }
        public static Vector3D ZAxis
        {
            get
            {
                return new Vector3D(0, 0, 1);
            }
        }
        public Vector3D(double xin, double yin, double zin)
        {
            this.Type = EntityType.Vector;            
            X = xin;            
            Y = yin;            
            Z = zin;

        }
        public Vector3D(Vector3 pt)
        {
            this.Type = EntityType.Vector;
            X = pt.X;
            Y = pt.Y ;
            Z = pt.Z ;

        }
        public Vector3D()
        {
            this.Type = EntityType.Vector;
            X = 0;
            Y = 0;
            Z = 0;
            
        }
        public void normalize()
        {
            double l = this.Length;
            this.X /= l;
            this.Y /= l;
            this.Z /= l;
        }
        
        public Vector3D cross(Vector3D v2)
        {
            double xm = this.Y*v2.Z - this.Z*v2.Y;
			double ym = this.Z*v2.X - this.X*v2.Z;
			double zm = this.X*v2.Y - this.Y*v2.X;
            Vector3D vOut = new Vector3D(xm,ym,zm);
            return vOut;

        }
        public double dot(Vector3D v2)
        {
            double result =  (this.X * v2.X + this.Y * v2.Y + this.Z * v2.Z);
            return result;
        }
        public static Vector3D operator -(Vector3D p1, Vector3D p2)
        {
            Vector3D pt = new Vector3D();
            pt.X = p1.X - p2.X;
            pt.Y = p1.Y - p2.Y;
            pt.Z = p1.Z - p2.Z;
            return pt;
        }
        public static Vector3D operator +(Vector3D p1, Vector3D p2)
        {
            Vector3D pt = new Vector3D();
            pt.X = p1.X + p2.X;
            pt.Y = p1.Y + p2.Y;
            pt.Z = p1.Z + p2.Z;
            return pt;
        }
        public static Vector3D operator /(Vector3D p1, double denom)
        {
            Vector3D pt = new Vector3D();
            pt.X = p1.X / denom;
            pt.Y = p1.Y / denom;
            pt.Z = p1.Z / denom;
            return pt;
        }
        public static Vector3D operator *(Vector3D p1, double scalar)
        {
            Vector3D pt = new Vector3D();
            pt.X = p1.X * scalar;
            pt.Y = p1.Y * scalar;
            pt.Z = p1.Z * scalar;
            return pt;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Vector3D p1, Vector3D p2)
        {
            bool equal = false;
            if ((Math.Abs(p1.X - p2.X) < double.Epsilon) &&
                (Math.Abs(p1.Y - p2.Y) < double.Epsilon) &&
                (Math.Abs(p1.Z - p2.Z) < double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public static bool operator !=(Vector3D p1, Vector3D p2)
        {
            bool equal = false;
            if ((Math.Abs(p1.X - p2.X) > double.Epsilon) ||
                (Math.Abs(p1.Y - p2.Y) > double.Epsilon) ||
                (Math.Abs(p1.Z - p2.Z) > double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public Vector3D Translate(double dx, double dy, double dz)
        {
           return new Vector3D( this.X + dx,this.Y + dy,this.Z + dz);          
        }
        public Vector3D RotateXY(Vector3 rotationPt, double angleRad)
        {
            Vector3D ptTrans = this;
            ptTrans.Translate(-1 * rotationPt.X, -1 * rotationPt.Y, -1 * rotationPt.Z);

            Vector3D ptRot = new Vector3D();
            
            double sin = Math.Sin(angleRad);
            double cos = Math.Cos(angleRad);
            ptRot.X = ptTrans.X * cos - ptTrans.Y * sin;
            ptRot.Y = ptTrans.X * sin + ptTrans.Y * cos;
            ptRot.Z = ptTrans.Z;
            return ptRot.Translate(rotationPt.X, rotationPt.Y, rotationPt.Z);
            
        }
    }
}
