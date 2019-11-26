using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class Vector2 : DwgEntity, IVector2
    {
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                length = 0;
            }

        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                length = 0;
            }

        }

        public double Length
        {
            get
            {
                if (length == 0)
                {
                    length = (double)Math.Sqrt((double)(x * x + y * y));
                }
                return length;
            }
        }

        private double length;
        private double x;
        private double y;

        public static string Name = "Vector2";
        public override string ToString()
        {
            return Name + ";" + x + ";" + y;
        }


        public Vector2()
        {

            x = 0;
            y = 0;
            Col = System.Drawing.Color.White;
        }
        public Vector2(IVector2 Vector2)
        {
            x = Vector2.X;
            y = Vector2.Y;

            Col = Vector2.Col;
        }
        public Vector2(IPointCyl ptIn)
        {
            x = (double)(ptIn.R * Math.Cos(ptIn.ThetaRad));
            y = (double)(ptIn.R * Math.Sin(ptIn.ThetaRad));

            Col = ptIn.Col;

        }
        public Vector2(double xIn, double yIn, System.Drawing.Color c)
        {
            x = xIn;
            y = yIn;


            Col = c;

        }
        public Vector2(double xIn, double yIn)
        {
            x = xIn;
            y = yIn;


            Col = System.Drawing.Color.White;

        }
        public override List<string> AsDXFString()
        {
            try
            {
                List<string> contents = new List<string>();
                contents.AddRange(DxfHeader());
                contents.Add("10");
                contents.Add(X.ToString("f5"));
                contents.Add("20");
                contents.Add(Y.ToString("f5"));
                contents.Add("30");
                contents.Add("0.0");
                contents.Add("0");

                return contents;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public BoundingBox BoundingBox()
        {
            BoundingBox ext = new BoundingBox();
            return ext;
        }
        public void Normalize()
        {
            double l = Length;
            if (l != 0)
            {
                x /= l;
                y /= l;

            }
            length = 0;
        }


        public double Dot(IVector2 v2)
        {
            double result = (x * v2.X + y * v2.Y);
            return result;
        }

        public double DistanceTo(IVector2 p2)
        {
            return (double)Math.Sqrt(Math.Pow((double)(p2.X - x), 2) + Math.Pow((double)(p2.Y - y), 2));
        }

        public IVector2 Translate(IVector3 translation)
        {
            return new Vector2(x + translation.X, y + translation.Y, Col);
        }

        public IVector2 RotateZ(IVector3 rotationPt, double angleRad)
        {
            IVector2 ptTrans = Translate(rotationPt.MultiplyBy(-1));
            Rotation rot = new Rotation();
            var sinR = Math.Sin(angleRad);
            var cosR = Math.Cos(angleRad);
            Vector2 ptRot = new Vector2(ptTrans.X * cosR - ptTrans.Y * sinR, ptTrans.X * sinR + ptTrans.Y * cosR);
            IVector2 ptOut = ptRot.Translate(rotationPt);
            ptOut.Col = Col;
            return ptOut;
        }
        public IVector2 Minus(IVector2 p2)
        {
            Vector2 pt = new Vector2();
            pt.X = X - p2.X;
            pt.Y = Y - p2.Y;

            pt.Col = Col;
            return pt;
        }
        public IVector2 Plus(IVector2 p2)
        {
            Vector2 pt = new Vector2();
            pt.X = X + p2.X;
            pt.Y = Y + p2.Y;

            pt.Col = Col;
            return pt;
        }
        public IVector2 MultiplyBy(double scalar)
        {
            Vector2 pt = new Vector2();
            pt.X = scalar * X;
            pt.Y = scalar * Y;

            return pt;
        }

        public bool Equals(IVector2 p2)
        {
            bool equal = false;

            if ((Math.Abs(X - p2.X) <= double.Epsilon) &&
                (Math.Abs(Y - p2.Y) <= double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public bool NotEqualTo(IVector2 p2)
        {
            bool equal = false;
            if ((Math.Abs(X - p2.X) >= double.Epsilon) ||
                (Math.Abs(Y - p2.Y) >= double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public IVector2 Clone()
        {
            return new Vector2(x, y, Col);

        }

        public IVector2 RotateX(IVector3 rotationPt, double angleRad)
        {
            throw new NotImplementedException();
        }

        public IVector2 RotateY(IVector3 rotationPt, double angleRad)
        {
            throw new NotImplementedException();
        }

        IBoundingBox IGeometryRoutines<IVector2>.BoundingBox()
        {
            throw new NotImplementedException();
        }
    }
}
