using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class Circle2
    {
        public Vector2 Center { get; set; }
        public double Radius { get; set; }
        public Circle2(Vector2 center, double r)
        {
            Radius = r;
            Center = center;
        }
    }
    public class Arc : DwgEntity, IArc
    {
        protected bool closedArc;

        //properties

        public IVector3 Center { get; set; }
        public double Radius { get; set; }
        public double StartAngleRad { get; set; }
        public double EndAngleRad { get; set; }
        public static string Name = "Arc";
        public override string ToString()
        {
            return Name + ";" + Center.X + ";" + Center.Y + ";" + Center.Z + ";"
                + Radius + ";" + StartAngleRad + ";" + EndAngleRad + ";" + ClosedArc;
        }

        public double StartAngleDeg
        {
            get
            {
                var pt = new PointCyl(Radius, StartAngleRad, 0);
                return pt.ThetaDeg;
            }
        }
        public double EndAngleDeg
        {
            get
            {
                var pt = new PointCyl(Radius, EndAngleRad, 0);
                return pt.ThetaDeg;
            }
        }
        public bool ClosedArc
        {
            get { return closedArc; }
            set
            {
                closedArc = value;
                if (closedArc)
                {
                    StartAngleRad = 0;
                    EndAngleRad = 2 * Math.PI;
                }
            }
        }
        //read only properties

        public Vector3 StartPoint
        {
            get
            {
                return new Vector3(Center.X + Radius * Math.Cos(StartAngleRad),
                    Center.Y + Radius * Math.Sin(StartAngleRad), Center.Z + Radius);
            }
        }
        public Vector3 EndPoint
        {
            get
            {
                return new Vector3(Center.X + Radius * Math.Cos(EndAngleRad),
                    Center.Y + Radius * Math.Sin(EndAngleRad), Center.Z);


            }
        }

        public double SweepAngleDeg
        {
            get
            {
                var pt = new PointCyl(Radius, SweepAngleRad, 0);
                return pt.ThetaDeg;

            }
        }
        public double SweepAngleRad
        {
            get
            {
                while (EndAngleRad < StartAngleRad)
                {
                    EndAngleRad += 2 * Math.PI;
                }
                return EndAngleRad - StartAngleRad;
            }
        }
        public double Length
        {
            get
            {
                return Radius * SweepAngleRad;
            }
        }
        public Arc()
        {

            Center = new Vector3();
            Col = System.Drawing.Color.FromArgb(255, 255, 255);
        }
        public Arc(IVector3 center, double radius, double startAngle, double endAngle)
        {

            Center = new Vector3(center.X, center.Y, center.Z);
            Radius = radius;
            StartAngleRad = startAngle;
            EndAngleRad = endAngle;
            Col = System.Drawing.Color.FromArgb(255, 255, 255);
        }

        public Arc(IVector3 point1, IVector3 point2, IVector3 point3)
        {
            //three point formula from the following assuming in xy plane
            //http://mathworld.wolfram.com/Circle.html

            double a = threePtArc_A(point1, point2, point3);
            double d = threePtArc_D(point1, point2, point3);
            double e = threePtArc_E(point1, point2, point3);
            double f = threePtArc_F(point1, point2, point3);

            Center = getCenter(d, e, a);
            Radius = getRadius(d, e, f, a); ;

            StartAngleRad = Math.Atan2(point1.Y - Center.Y, point1.X - Center.X);
            EndAngleRad = Math.Atan2(point3.Y - Center.Y, point3.X - Center.X);

            Col = point1.Col;
        }


        public void ParseCircleDxf(List<string> fileSection)
        {
            try
            {
                ParseCommonDxf(fileSection);
                for (int i = 0; i < fileSection.Count; i++)
                {
                    var line = fileSection[i].Trim();

                    if (line == "10")
                    {
                        Center.X = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "20")
                    {
                        Center.Y = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "30")
                    {
                        Center.Z = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "40")
                    {
                        Radius = Convert.ToDouble(fileSection[i + 1]);
                    }
                }

                StartAngleRad = 0;
                EndAngleRad = Math.PI * 2;
                ClosedArc = true;


            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ParseArcDxf(List<string> fileSection)
        {
            try
            {
                double startDeg = 0;
                double endDeg = 0;
                ParseCircleDxf(fileSection);
                for (int i = 0; i < fileSection.Count - 1; i++)
                {
                    var line = fileSection[i].Trim();
                    if (line == "50")
                    {
                        startDeg = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "51")
                    {
                        endDeg = Convert.ToDouble(fileSection[i + 1]);
                    }
                }
                StartAngleRad = PointCyl.ToRadians(startDeg);
                EndAngleRad = PointCyl.ToRadians(endDeg);
                ClosedArc = false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override List<string> AsDXFString()
        {
            List<string> entityList = new List<string>();
            entityList.AddRange(DxfHeader());
            entityList.Add("10");
            entityList.Add(Center.X.ToString("f5"));
            entityList.Add("20");
            entityList.Add(Center.Y.ToString("f5"));
            entityList.Add("30");
            entityList.Add(Center.Z.ToString("f5"));
            entityList.Add("40");
            entityList.Add(Radius.ToString("f5"));
            entityList.Add("210");
            entityList.Add("0.0");
            entityList.Add("220");
            entityList.Add("0.0");
            entityList.Add("230");
            entityList.Add("1.0");
            entityList.Add("0");

            return entityList;
        }
        public IBoundingBox BoundingBox()
        {
            IBoundingBox ext = new BoundingBox();

            ext.Max.X = Center.X + Radius;
            ext.Min.X = Center.X - Radius;
            ext.Max.Y = Center.Y + Radius;
            ext.Min.Y = Center.Y - Radius;
            ext.Max.Z = Center.Z;
            ext.Min.Z = Center.Z;
            return ext;
        }
        public IArc Translate(IVector3 translation)
        {
            IArc arcTrans = new Arc();
            arcTrans.Center = Center.Translate(translation);
            arcTrans.StartAngleRad = StartAngleRad;
            arcTrans.EndAngleRad = EndAngleRad;
            arcTrans.Radius = Radius;
            return arcTrans;
        }
        public IArc RotateX(IVector3 rotationPt, double angleRad)
        {
            IArc arcRot = new Arc();
            arcRot.Center = Center.RotateX(rotationPt, angleRad);
            arcRot.StartAngleRad = StartAngleRad + angleRad;
            arcRot.EndAngleRad = EndAngleRad + angleRad;
            arcRot.Radius = Radius;
            return arcRot;
        }
        public IArc RotateY(IVector3 rotationPt, double angleRad)
        {
            IArc arcRot = new Arc();
            arcRot.Center = Center.RotateY(rotationPt, angleRad);
            arcRot.StartAngleRad = StartAngleRad + angleRad;
            arcRot.EndAngleRad = EndAngleRad + angleRad;
            arcRot.Radius = Radius;
            return arcRot;
        }
        public IArc RotateZ(IVector3 rotationPt, double angleRad)
        {
            IArc arcRot = new Arc();
            arcRot.Center = Center.RotateZ(rotationPt, angleRad);
            arcRot.StartAngleRad = StartAngleRad + angleRad;
            arcRot.EndAngleRad = EndAngleRad + angleRad;
            arcRot.Radius = Radius;
            return arcRot;
        }
        public IArc Clone()
        {
            Arc a = new Arc(Center, Radius, StartAngleRad, EndAngleRad);
            a.Col = Col;
            return a;
        }

        private double getRadius(double dParam, double eParam, double fParam, double aParam)
        {
            return Math.Sqrt(((dParam * dParam + eParam * eParam) / (4 * aParam * aParam)) - (fParam / aParam));
        }
        private IVector3 getCenter(double dParam, double eParam, double aParam)
        {
            double xCenter = -1 * dParam / 2 * aParam;
            double yCenter = -1 * eParam / 2 * aParam;
            return new Vector3(xCenter, yCenter, 0);
        }
        private double threePtArc_A(IVector3 Point1, IVector3 Point2, IVector3 pt3)
        {
            Matrix3x3 mat = new Matrix3x3(
                Point1.X, Point1.Y, 1,
                Point2.X, Point2.Y, 1,
                pt3.X, pt3.Y, 1);
            double a = mat.Determinant();
            return a;
        }
        private double threePtArc_D(IVector3 pt1, IVector3 pt2, IVector3 pt3)
        {
            Matrix3x3 mat = new Matrix3x3(
                pt1.X * pt1.X + pt1.Y * pt1.Y, pt1.Y, 1,
                pt2.X * pt2.X + pt2.Y * pt2.Y, pt2.Y, 1,
                pt3.X * pt3.X + pt3.Y * pt3.Y, pt3.Y, 1);
            double d = -1 * mat.Determinant();
            return d;
        }
        private double threePtArc_E(IVector3 pt1, IVector3 pt2, IVector3 pt3)
        {
            Matrix3x3 mat = new Matrix3x3(
                pt1.X * pt1.X + pt1.Y * pt1.Y, pt1.X, 1,
                pt2.X * pt2.X + pt2.Y * pt2.Y, pt2.X, 1,
                pt3.X * pt3.X + pt3.Y * pt3.Y, pt3.X, 1);
            double e = mat.Determinant();
            return e;
        }
        private double threePtArc_F(IVector3 pt1, IVector3 pt2, IVector3 pt3)
        {
            Matrix3x3 mat = new Matrix3x3(
                pt1.X * pt1.X + pt1.Y * pt1.Y, pt1.X, pt1.Y,
                pt2.X * pt2.X + pt2.Y * pt2.Y, pt2.X, pt2.Y,
                pt3.X * pt3.X + pt3.Y * pt3.Y, pt3.X, pt3.Y);
            double f = -1 * mat.Determinant();
            return f;
        }


        public IIntersection Intersects(ILine3 ray2)
        {
            try
            {
                IIntersection result = new Intersection3();
                Vector3 Point1;
                Intersection3 ir = new Intersection3();
                //transform arc and line to center arc at origin
                IVector3 trans = new Vector3(-1 * Center.X, -1 * Center.Y, 0);
                IArc arcT = Translate(trans);
                ILine3 lineT = ray2.Translate(trans);
                //parameterize line
                double dy = lineT.Point2.Y - lineT.Point1.Y;
                double dx = lineT.Point2.X - lineT.Point1.X;

                //calc quadratic solution
                double a = Math.Pow(dx, 2) + Math.Pow(dy, 2);
                double b = 2 * (dx) * (lineT.Point1.X - arcT.Center.X) + 2 * (dy) * (lineT.Point1.Y - arcT.Center.Y);
                double c = Math.Pow(lineT.Point1.X - arcT.Center.X, 2) + Math.Pow(lineT.Point1.Y - arcT.Center.Y, 2) - Math.Pow(arcT.Radius, 2);
                double discrim = Math.Pow(b, 2) - 4 * a * c;

                if (discrim >= 0)//check solution is real
                {
                    double tPos = (-b + Math.Sqrt(discrim)) / (2 * a);
                    double tNeg = (-b - Math.Sqrt(discrim)) / (2 * a);
                    double xpos = dx * tPos + lineT.Point1.X;
                    double ypos = dy * tPos + lineT.Point1.Y;
                    double xneg = dx * tNeg + lineT.Point1.X;
                    double yneg = dy * tNeg + lineT.Point1.Y;
                    double angneg = Math.Atan2(yneg, xneg);
                    double angpos = Math.Atan2(ypos, xpos);

                    if (angneg < 0) angneg += 2 * Math.PI;
                    if (angpos < 0) angpos += 2 * Math.PI;

                    //transform point back before returning
                    if ((angneg >= arcT.StartAngleRad) && (angneg <= arcT.EndAngleRad))
                    {

                        Point1 = new Vector3(xneg, yneg, 0);
                        var pt = Point1.Translate(new Vector3(Center.X, Center.Y, 0.0));
                        result = new Intersection3(pt, true);
                        result.Intersects = true;
                    }
                    if ((angpos >= arcT.StartAngleRad) && (angpos <= arcT.EndAngleRad))
                    {

                        Point1 = new Vector3(xpos, ypos, 0);
                        var pt = Point1.Translate(new Vector3(Center.X, Center.Y, 0.0));
                        result = new Intersection3(pt, true);
                        result.Intersects = true;
                    }

                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }

}
