using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class Line2 : DwgEntity, ILine2
    {
        public IVector2 Point1 { get; set; }
        public IVector2 Point2 { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow((Point2.X - Point1.X), 2) +
                    Math.Pow((Point2.Y - Point1.Y), 2));
            }
        }
        public double SlopeXY
        {
            get
            {
                double denom = Point2.X - Point1.X;
                double slope;
                if (denom == 0)
                {
                    slope = double.NaN;

                }
                else
                {
                    slope = (Point2.Y - Point1.Y) / denom;
                }

                return slope;
            }

        }
        public Line2 Translate(IVector2 translation)
        {
            Line2 lineOut = new Line2(Point1.Plus(translation), Point2.Plus(translation));
            lineOut.Col = Col;
            return lineOut;
        }

        public Line2 RotateZ(IVector3 rotationPt, double angleRad)
        {
            Line2 lineRot = new Line2();
            lineRot.Point1 = Point1.RotateZ(rotationPt, angleRad);
            lineRot.Point2 = Point2.RotateZ(rotationPt, angleRad);
            lineRot.Col = Col;
            return lineRot;
        }

        public Line2 Clone()
        {
            return new Line2(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }
        public Line2()
        {
            Point1 = new Vector2();
            Point2 = new Vector2();

            Col = System.Drawing.Color.White;

        }
        public Line2(IVector2 Point1, IVector2 Point2)
        {
            this.Point1 = new Vector2(Point1);
            this.Point2 = new Vector2(Point2);



            Col = new System.Drawing.Color();
        }
        public Line2(double x1In, double y1In, double x2In, double y2In)
        {
            Point1 = new Vector2(x1In, y1In);
            Point2 = new Vector2(x2In, y2In);


            Col = new System.Drawing.Color();
        }
        public override List<string> AsDXFString()
        {
            try
            {
                var contents = DxfHeader();
                contents.Add(" 10");
                contents.Add(Point1.X.ToString("f5"));
                contents.Add(" 20");
                contents.Add(Point1.Y.ToString("f5"));
                contents.Add(" 30");
                contents.Add("0");
                contents.Add(" 11");
                contents.Add(Point2.X.ToString("f5"));
                contents.Add(" 21");
                contents.Add(Point2.Y.ToString("f5"));
                contents.Add(" 31");
                contents.Add("0");

                return contents;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double ThetaRadians()
        {
            return Math.Atan2(Point2.Y - Point1.Y, Point2.X - Point1.X);
        }

        public IIntersection Intersects(ILine2 ray2)
        {
            try
            {
                var result = new Intersection3();
                var ptOut = new Vector2();

                if (SlopeXY == ray2.SlopeXY)
                {
                    result.Intersects = false;

                }
                else
                {

                    double denom = (Point1.X - Point2.X) * (ray2.Point1.Y - ray2.Point2.Y)
                        - (ray2.Point1.X - ray2.Point2.X) * (Point1.Y - Point2.Y);
                    double xOut = 0;
                    double yOut = 0;
                    double line1Det = Point1.X * Point2.Y - Point2.X * Point1.Y;
                    double line2Det = ray2.Point1.X * ray2.Point2.Y - ray2.Point2.X * ray2.Point1.Y;
                    xOut = (line1Det * (ray2.Point1.X - ray2.Point2.X) - line2Det * (Point1.X - Point2.X)) / denom;
                    yOut = (line1Det * (ray2.Point1.Y - ray2.Point2.Y) - line2Det * (Point1.Y - Point2.Y)) / denom;
                    result = new Intersection3(new Vector3(xOut, yOut, 0), true);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
    public class Line3 : DwgEntity, ILine3
    {
        public static string Name = "Line";

        public IVector3 Point1 { get; set; }
        public IVector3 Point2 { get; set; }
        public Line3()
        {
            Point1 = new Vector3();
            Point2 = new Vector3();
            Init();
        }


        public Line3(IVector3 Point1, IVector3 Point2)
        {
            this.Point1 = new Vector3(Point1);
            this.Point2 = new Vector3(Point2);
            Col = new System.Drawing.Color();
            Init();
        }
        public Line3(double x1In, double y1In, double z1In, double x2In, double y2In, double z2In)
        {
            Point1 = new Vector3(x1In, y1In, z1In);
            Point2 = new Vector3(x2In, y2In, z2In);
            Col = new System.Drawing.Color();
            Init();
        }
        public Line3(IPointCyl pt1, IPointCyl pt2)
        {
            try
            {
                double x1 = pt1.R * Math.Cos(pt1.ThetaRad);
                double y1 = pt1.R * Math.Sin(pt1.ThetaRad);
                double z1 = pt1.Z;
                double x2 = pt2.R * Math.Cos(pt2.ThetaRad);
                double y2 = pt2.R * Math.Sin(pt2.ThetaRad);
                double z2 = pt2.Z;

                Col = pt1.Col;
                Point1 = new Vector3(x1, y1, z1);
                Point2 = new Vector3(x2, y2, z2);
                Init();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Line3(ILine3 lineIn)
        {
            Point1 = new Vector3(lineIn.Point1);
            Point2 = new Vector3(lineIn.Point2);
            Col = lineIn.Col;
            Init();
        }

        private void Init()
        {
            Col = System.Drawing.Color.White;
            LayerName = "0";
            this.acDbName = "AcDbLine";
            this.entityName = "LINE";
            this.byLayer = false;
            this.colorNumber = "255";
            this.hardPointerID = "1";
        }
        public override string ToString()
        {
            return Name + ";" + Point1.X + ";" + Point1.Y + ";" + Point1.Z
                + ";" + Point1.X + ";" + Point1.Y + ";" + Point1.Z;
        }


        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow((Point2.X - Point1.X), 2) +
                    Math.Pow((Point2.Y - Point1.Y), 2) + Math.Pow((Point2.Z - Point1.Z), 2));
            }
        }
        public double SlopeXY
        {
            get
            {
                double denom = Point2.X - Point1.X;
                double slope;
                if (denom == 0)
                {
                    slope = double.NaN;

                }
                else
                {
                    slope = (Point2.Y - Point1.Y) / denom;
                }

                return slope;
            }

        }

        public List<IVector3> BreakMany(double breakLen)
        {
            try
            {
                var points = new List<IVector3>();

                if (Length <= breakLen)
                {
                    points.Add(Point1);
                    points.Add(Point2);
                    return points;
                }

                int parseCount = (int)Math.Round(Length / breakLen);
                if (parseCount == 0)
                    parseCount = 1;

                IVector3 delta = Point2.Minus(Point1);

                double dpc = parseCount;
                for (int i = 0; i <= parseCount; i++)
                {
                    double di = i;

                    IVector3 v = Point1.Plus(delta.MultiplyBy(di / dpc));
                    points.Add(v);
                }
                return points;
            }
            catch (Exception)
            {

                throw;
            }


        }


        public IIntersection Intersects(ILine3 ray2)
        {
            try
            {
                var result = new Intersection3();
                var ptOut = new Vector2();

                if (SlopeXY == ray2.SlopeXY)
                {
                    result.Intersects = false;

                }
                else
                {

                    double denom = (Point1.X - Point2.X) * (ray2.Point1.Y - ray2.Point2.Y)
                        - (ray2.Point1.X - ray2.Point2.X) * (Point1.Y - Point2.Y);
                    double xOut = 0;
                    double yOut = 0;
                    double line1Det = Point1.X * Point2.Y - Point2.X * Point1.Y;
                    double line2Det = ray2.Point1.X * ray2.Point2.Y - ray2.Point2.X * ray2.Point1.Y;
                    xOut = (line1Det * (ray2.Point1.X - ray2.Point2.X) - line2Det * (Point1.X - Point2.X)) / denom;
                    yOut = (line1Det * (ray2.Point1.Y - ray2.Point2.Y) - line2Det * (Point1.Y - Point2.Y)) / denom;
                    result = new Intersection3(new Vector3(xOut, yOut, 0), true);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void ParseDxf(List<string> fileSection)
        {
            try
            {
                Col = System.Drawing.Color.Red;
                ParseCommonDxf(fileSection);
                for (int i = 0; i < fileSection.Count - 1; i++)
                {
                    var line = fileSection[i].Trim();

                    if (line == "10")
                    {
                        Point1.X = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "20")
                    {
                        Point1.Y = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "30")
                    {
                        Point1.Z = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "11")
                    {
                        Point2.X = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "21")
                    {
                        Point2.Y = Convert.ToDouble(fileSection[i + 1]);
                    }
                    if (line == "31")
                    {
                        Point2.Z = Convert.ToDouble(fileSection[i + 1]);
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
            try
            {
                var contents = new List<string>();
                contents.AddRange(DxfHeader());
                contents.Add(" 10");
                contents.Add(Point1.X.ToString("f5"));
                contents.Add(" 20");
                contents.Add(Point1.Y.ToString("f5"));
                contents.Add(" 30");
                contents.Add(Point1.Z.ToString("f5"));
                contents.Add(" 11");
                contents.Add(Point2.X.ToString("f5"));
                contents.Add(" 21");
                contents.Add(Point2.Y.ToString("f5"));
                contents.Add(" 31");
                contents.Add(Point2.Z.ToString("f5"));

                return contents;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public double PsiRadians()
        {
            return Length == 0 ? 0 : Math.Acos((Point2.Z - Point1.Z) / Length);
        }
        public double ThetaRadians()
        {
            return Math.Atan2(Point2.Y - Point1.Y, Point2.X - Point1.X);
        }
        public IBoundingBox BoundingBox()
        {
            var pts = new IVector3[] { Point1, Point2 };
            IBoundingBox ext = BoundingBoxBuilder.FromPtArray(pts);

            return ext;
        }
        public ILine3 Translate(IVector3 translation)
        {
            ILine3 lineOut = new Line3(Point1.Plus(translation), Point2.Plus(translation));
            lineOut.Col = Col;
            return lineOut;
        }
        public ILine3 RotateX(IVector3 rotationPt, double angleRad)
        {
            ILine3 lineRot = new Line3();
            lineRot.Point1 = Point1.RotateX(rotationPt, angleRad);
            lineRot.Point2 = Point2.RotateX(rotationPt, angleRad);
            lineRot.Col = Col;
            return lineRot;
        }
        public ILine3 RotateY(IVector3 rotationPt, double angleRad)
        {
            ILine3 lineRot = new Line3();
            lineRot.Point1 = Point1.RotateY(rotationPt, angleRad);
            lineRot.Point2 = Point2.RotateY(rotationPt, angleRad);
            lineRot.Col = Col;
            return lineRot;
        }
        public ILine3 RotateZ(IVector3 rotationPt, double angleRad)
        {
            Line3 lineRot = new Line3();
            lineRot.Point1 = Point1.RotateZ(rotationPt, angleRad);
            lineRot.Point2 = Point2.RotateZ(rotationPt, angleRad);
            lineRot.Col = Col;
            return lineRot;
        }

        public ILine3 Clone()
        {
            ILine3 line3 = new Line3(Point1.X, Point1.Y, Point1.Z,
                Point2.X, Point2.Y, Point2.Z);
            return line3;
        }

    }
}
