using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class Vector3 : DwgEntity, IVector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }


        public static string Name = "Vector3";


        public Vector3()
        {


            Col = System.Drawing.Color.FromArgb(255, 255, 255);

        }
        public Vector3(IVector3 vector3)
        {
            X = vector3.X;
            Y = vector3.Y;
            Z = vector3.Z;
            Col = vector3.Col;

        }
        public Vector3(IPointCyl ptIn)
        {
            X = (double)(ptIn.R * Math.Cos(ptIn.ThetaRad));
            Y = (double)(ptIn.R * Math.Sin(ptIn.ThetaRad));
            Z = (double)(ptIn.Z);
            Col = ptIn.Col;

        }
        public Vector3(double xIn, double yIn, double zIn, System.Drawing.Color c)
        {

            Init(xIn, yIn, zIn, c);

        }
        public Vector3(double xIn, double yIn, double zIn)
        {
            var c = System.Drawing.Color.FromArgb(255, 255, 255);
            Init(xIn, yIn, zIn, c);
        }
        private void Init(double xIn, double yIn, double zIn, System.Drawing.Color c)
        {
            X = xIn;
            Y = yIn;
            Z = zIn;

            Col = c;
        }
        public void ParseDXF(List<string> fileSection)
        {
            try
            {
                ParseCommonDxf(fileSection);
                for (int i = 0; i < fileSection.Count; i++)
                {
                    var line = fileSection[i].Trim();

                    if (line == "10")
                    {
                        X = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "20")
                    {
                        Y = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "30")
                    {
                        Z = Convert.ToDouble(fileSection[i + 1]);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override List<string> AsDXFString()
        {
            List<string> contents = new List<string>();
            contents.AddRange(DxfHeader());
            contents.Add("10");
            contents.Add(X.ToString("f5"));
            contents.Add("20");
            contents.Add(Y.ToString("f5"));
            contents.Add("30");
            contents.Add(Z.ToString("f5"));
            contents.Add("0");

            return contents;
        }
        public static Vector3 Origin { get { return new Vector3(0, 0, 0); } }
        public IBoundingBox BoundingBox()
        {
            IBoundingBox ext = new BoundingBox();

            return ext;
        }


        public void Normalize()
        {
            double l = Length;
            if (Length != 0)
            {
                X /= l;
                Y /= l;
                Z /= l;
            }

        }

        public IVector3 Cross(IVector3 v2)
        {
            double xm = Y * v2.Z - Z * v2.Y;
            double ym = Z * v2.X - X * v2.Z;
            double zm = X * v2.Y - Y * v2.X;
            Vector3 vOut = new Vector3(xm, ym, zm);
            return vOut;

        }
        public double Dot(IVector3 v2)
        {
            double result = (X * v2.X + Y * v2.Y + Z * v2.Z);
            return result;
        }

        public double DistanceTo(IVector3 p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - X, 2) + Math.Pow(p2.Y - Y, 2) + Math.Pow(p2.Z - Z, 2));
        }


        public IVector3 Translate(IVector3 translation)
        {
            return new Vector3(X + translation.X, Y + translation.Y, Z + translation.Z, Col);
        }
        public IVector3 RotateX(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptTrans = Translate(rotationPt.MultiplyBy(-1));
            Rotation rot = new Rotation();
            IVector3 ptRot = rot.AboutX(angleRad) * ptTrans;
            IVector3 ptOut = ptRot.Translate(rotationPt);
            ptOut.Col = Col;
            return ptOut;
        }
        public IVector3 RotateY(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptTrans = Translate(rotationPt.MultiplyBy(-1));
            Rotation rot = new Rotation();
            IVector3 ptRot = rot.AboutY(angleRad) * ptTrans;
            IVector3 ptOut = ptRot.Translate(rotationPt);
            ptOut.Col = Col;
            return ptOut;
        }
        public IVector3 RotateZ(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptTrans = Translate(rotationPt.MultiplyBy(-1));
            Rotation rot = new Rotation();
            IVector3 ptRot = rot.AboutZ(angleRad) * ptTrans;
            IVector3 ptOut = ptRot.Translate(rotationPt);
            ptOut.Col = Col;
            return ptOut;
        }
        public IVector3 Minus(IVector3 p2)
        {
            IVector3 pt = new Vector3();
            pt.X = X - p2.X;
            pt.Y = Y - p2.Y;
            pt.Z = Z - p2.Z;
            pt.Col = Col;
            return pt;
        }
        public IVector3 Plus(IVector3 p2)
        {
            IVector3 pt = new Vector3();
            pt.X = X + p2.X;
            pt.Y = Y + p2.Y;
            pt.Z = Z + p2.Z;
            pt.Col = Col;
            return pt;
        }
        public IVector3 MultiplyBy(double scalar)
        {
            IVector3 pt = new Vector3();
            pt.X = scalar * X;
            pt.Y = scalar * Y;
            pt.Z = scalar * Z;
            return pt;
        }
        public static string CSVHeader()
        {
            return "X,Y,Z";
        }
        public string AsCSV()
        {
            return string.Concat(X.ToString(), ",", Y.ToString(), ",", Z.ToString());
        }

        public bool Equals(Vector3 p2)
        {
            bool equal = false;

            if ((Math.Abs(X - p2.X) <= double.Epsilon) &&
                (Math.Abs(Y - p2.Y) <= double.Epsilon) &&
                (Math.Abs(Z - p2.Z) <= double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public bool NotEqualto(Vector3 p2)
        {
            bool equal = false;
            if ((Math.Abs(X - p2.X) >= double.Epsilon) ||
                (Math.Abs(Y - p2.Y) >= double.Epsilon) ||
                (Math.Abs(Z - p2.Z) >= double.Epsilon))
            {
                equal = true;
            }
            return equal;
        }
        public IVector3 Clone()
        {
            return new Vector3(X, Y, Z);
        }

        public IVector3 InterpolateX(double x, IVector3 pt0, IVector3 pt1)
        {
            try
            {
                var dx = pt1.X - pt0.X;
                var dz = pt1.Z - pt0.Z;
                var dy = pt1.Y - pt0.Y;
                if (dx == 0)
                {
                    return pt0;
                }
                else
                {
                    var proportion = (x - pt0.X) / dx;
                    var z = (proportion * dz) + pt0.Z;
                    var y = (proportion * dy) + pt0.Y;
                    return new Vector3(x, y, z);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Equals(IVector3 vector3)
        {
            return (X == vector3.X && Y == vector3.Y && Z == vector3.Z);
        }

        public bool NotEqualTo(IVector3 vector3)
        {
            return (X != vector3.X || Y != vector3.Y || Z != vector3.Z);
        }
    }
}
