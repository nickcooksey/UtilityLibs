using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace DataLib
{

    public class CartData : ICartData
    {
        private List<IVector3> data;
        public int Count
        {
            get { return data.Count; }
        }
        public bool IsReadOnly { get { return false; } }

        public IVector3 this[int index] { get { return data[index]; } set { data[index] = value; } }
        public CartData()
        {
            Color = System.Drawing.Color.Blue;
            FileName = "";
            data = new List<IVector3>();
        }
        public CartData(string filename)
        {
            FileName = filename;
            Color = System.Drawing.Color.Blue;
            data = new List<IVector3>();
        }

        private string CSVHeader()
        {
            return Vector3.CSVHeader();

        }
        public List<string> AsCSV()
        {
            var lines = new List<string>();
            lines.Add(CSVHeader());
            foreach (var pt in data)
            {
                lines.Add(pt.AsCSV());
            }
            return lines;
        }

        public IList<IVector3> TrimToWindow(RectangleF r, ViewPlane viewPlane)
        {
            try
            {
                var winData = new List<IVector3>();
                var dd = AsDisplayData(viewPlane);
                foreach (var pt in dd)
                {
                    if(r.Contains(pt))                    
                    {
                        switch (viewPlane)
                        {
                            case ViewPlane.XY:
                                winData.Add(new Vector3(pt.X, pt.Y, 0));
                                break;
                            case ViewPlane.XZ:
                                winData.Add(new Vector3(pt.X, 0, pt.Y));
                                break;
                            case ViewPlane.YZ:
                                winData.Add(new Vector3(0, pt.X, pt.Y));
                                break;
                        }
                    }
                }
                return winData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IDisplayData AsDisplayData(ViewPlane viewplane)
        {
            try
            {
                BuildDisplayData(viewplane);

                return displayData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<IDwgEntity> AsDxfData(System.Drawing.Color color)
        {
            try
            {
                try
                {
                    var entityList = new List<IDwgEntity>();

                    for (int i = 1; i < Count - 1; i++)
                    {
                        var line = new Line3(this[i], this[i + 1]);
                        line.Col = color;
                        entityList.Add(line);
                    }

                    return entityList;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public System.Drawing.Color Color { get; set; }
        public string FileName { get; set; }



        double GetDataRotation(IVector3 pt1, IVector3 pt2)
        {
            return Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X);
        }

        public ICylData AutoFitToCircle2Pts(RectangleF searchBox1, RectangleF searchBox2,double knownRadius)
        {
            try
            {
                var ptList1 = GetPtsContainedIn(searchBox1);
                var ptList2 = GetPtsContainedIn(searchBox2);
                
                if(ptList1.Count==0 || ptList2.Count==0)
                {
                    throw new ArgumentException("No points found in one or more search boxes.");
                }
                IVector2 pt1Y = GetMaxYPt(ptList1);
                IVector2 pt2Y = GetMaxYPt(ptList2);
               
                return FitToCircle2Pts(pt1Y, pt2Y, knownRadius);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICylData AutoFitToCircle3Pts(RectangleF searchBox1, RectangleF searchBox2 )
        {
            try
            {
                var ptList1 = GetPtsContainedIn(searchBox1);
                var ptList2 = GetPtsContainedIn(searchBox2);

                if (ptList1.Count == 0 || ptList2.Count == 0)
                {
                    throw new ArgumentException("No points found in one or more search boxes.");
                }
                IVector2 pt1Y = GetMaxYPt(ptList1);
                IVector2 pt2Y = GetMaxYPt(ptList2);
                IVector2 pt3Y = GetMinYPt(ptList2);
                return FitToCircle3Pts(pt1Y, pt2Y, pt3Y);

            }
            catch (Exception)
            {

                throw;
            }
        }
        List<IVector3> GetPtsContainedIn(RectangleF searchBox)
        {
            try
            {
                var ptList = new List<IVector3>();
                foreach (var pt in data)
                {
                    var ptf = new PointF((float)pt.X, (float)pt.Y);
                    if (searchBox.Contains(ptf))
                    {
                        ptList.Add(pt);
                    }
                     
                }
                return ptList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        Vector2 GetMaxYPt(List<IVector3> ptList)
        {
            try
            {
                double maxy = double.MinValue;
                double x = 0;

                foreach (var pt in ptList)
                {
                    if (pt.Y > maxy)
                    {
                        maxy = pt.Y;
                        x = pt.X;
                    }
                }
                return new Vector2(x, maxy);
            }
            catch (Exception)
            {

                throw;
            }
        }
        Vector2 GetMinYPt(List<IVector3> ptList)
        {
            try
            {
                double miny = double.MaxValue;
                double x = 0;

                foreach (var pt in ptList)
                {
                    if (pt.Y < miny)
                    {
                        miny = pt.Y;
                        x = pt.X;
                    }
                }
                return new Vector2(x, miny);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICylData FitToCircle2Pts(IVector2 pt1, IVector2 pt2, double fitRadius)
        {
            try
            {
                var centers = GetCircleCenters(pt1, pt2, fitRadius);
                IVector2 center = new Vector2();
                if (centers[0].Y > centers[1].Y)
                {
                    center = centers[0];
                }
                else
                {
                    center = centers[1];
                }
                return TranslateToCenter(center);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public ICylData FitToCircle3Pts(IVector2 pt1, IVector2 pt2, IVector2 pt3)
        {
            try
            {
                var circ = FitCirleToThreePoints(pt1, pt2, pt3);
                return TranslateToCenter(circ.Center);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICylData FitToCircle2Pts(double x1,double x2,double knownRadius)
        {
            try
            {
                var v1 = GetIntersectionAtX(x1);
                var v2 = GetIntersectionAtX(x2);
                IVector2 iv1 = new Vector2(v1.X, v1.Y);
                IVector2 iv2 = new Vector2(v2.X, v2.Y);
                return FitToCircle2Pts(iv1, iv2, knownRadius);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private IVector2[] GetCircleCenters(IVector2 p1, IVector2 p2, double radius)
        {
            try
            {
                if (radius < 0) throw new ArgumentException("Negative radius.");
                if (radius == 0)
                {
                    if (p1 == p2) return new[] { p1 };
                    else throw new InvalidOperationException("No circles.");
                }
                if (p1 == p2) throw new InvalidOperationException("Infinite number of circles.");

                double sqDistance = Math.Pow(p1.DistanceTo(p2), 2);
                double sqDiameter = 4 * radius * radius;
                if (sqDistance > sqDiameter) throw new InvalidOperationException("Points are too far apart.");

                var midPoint = new Vector2((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                if (sqDistance == sqDiameter) return new[] { midPoint };

                double d = Math.Sqrt(radius * radius - sqDistance / 4);
                double distance = Math.Sqrt(sqDistance);
                double ox = d * (p2.X - p1.X) / distance, oy = d * (p2.Y - p1.Y) / distance;
                return new[] {
                    new Vector2(midPoint.X - oy, midPoint.Y + ox),
                    new Vector2(midPoint.X + oy, midPoint.Y - ox)
                };
            }
            catch (Exception)
            {

                throw;
            }

        }

        private ICylData TranslateToCenter(IVector2 center)
        {
            try
            {
                var translation = new Vector3(-1.0 * center.X, -1.0 * center.Y, 0);
                CylData cylData = new CylData(this.FileName);
                foreach (var pt in data)
                {
                    var pttrans = pt.Translate(translation);
                    PointCyl pointCyl = new PointCyl(pttrans);

                    cylData.Add(pointCyl);
                }
                return cylData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Circle2 FitCirleToThreePoints(IVector2 p1, IVector2 p2, IVector2 p3)
        {
            try
            {
                double x1 = (p1.X + p2.X) / 2.0;
                double y1 = (p1.Y + p2.Y) / 2.0;
                double dy1 = p2.X - p1.X;
                double dx1 = -1 * (p2.Y - p1.Y);
                double x2 = (p3.X + p2.X) / 2.0;
                double y2 = (p3.Y + p2.Y) / 2.0;
                double dy2 = p3.X - p2.X;
                double dx2 = -1 * (p3.Y - p2.Y);
                var line1 = new Line2(new Vector2(x1, y1), new Vector2(x1 + dx1, y1 + dy1));
                var line2 = new Line2(new Vector2(x2, y2), new Vector2(x2 + dx2, y2 + dy2));
                var intersection = line1.Intersects(line2);
                if (!(intersection.Intersects))
                {
                    throw new Exception("Points are colinear, Couldn't find arc");
                }
                else
                {
                    var center = new Vector2(intersection.Point.X, intersection.Point.Y);
                   
                    var dx = center.X - p1.X;
                    var dy = center.Y - p1.Y;
                    var r = Math.Sqrt(dx * dx + dy * dy);
                    if (double.IsNaN(center.X) || double.IsNaN(center.Y) || double.IsNaN(r))
                    {
                        throw new ArgumentException("Unable to calculate center or radius of fitting circle");
                    }
                    return new Circle2(center, r);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RotateDataToLine(IVector3 pt1, IVector3 pt2)
        {
            try
            {
                
                var _dataRotationRad = GetDataRotation(pt1, pt2);
                var origin = new Vector3(0, 0, 0);
                var rotStart = pt1.RotateZ(origin, -1 * _dataRotationRad);
                var rotEnd = pt2.RotateZ(origin, -1 * _dataRotationRad);
                var vTrans = new Vector3(0, -1 * rotEnd.Y, 0);
                var rotData = new CartData();
                foreach (IVector3 pt in data)
                {
                    var ptRot = pt.RotateZ(origin, -1 * _dataRotationRad);
                    var ptTrans = ptRot.Translate(vTrans);
                    rotData.Add(ptTrans);
                }
                data.Clear();
                data.AddRange(rotData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Tuple<double, double> GetMinMaxY()
        {
            double maxYData = double.MinValue;
            double minYData = double.MaxValue;
            foreach (var pt in data)
            {
                if (pt.Y > maxYData)
                {
                    maxYData = pt.Y;
                }
                if (pt.Y < minYData)
                {
                    minYData = pt.Y;
                }
            }
            return new Tuple<double, double>(minYData, maxYData);
        }
        public ICartData MirrorYAxis()
        {
            try
            {
                var cd = new CartData();
                foreach (IVector3 pt in data)
                {
                    cd.Add(new Vector3(pt.X *-1, pt.Y, pt.Z));
                }
                return cd;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public ICartData MirrorXAxis()
        {
            try
            {
                var cd = new CartData();
                foreach (Vector3 pt in data)
                {
                    cd.Add(new Vector3(pt.X , pt.Y*-1, pt.Z));
                    
                }
                return cd;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public ICartData CenterToMidpoint()
        {
            int midCount = (int)Math.Round(data.Count / 2.0);
            var minMax = GetMinMaxY();
            var midYData = (minMax.Item1 + minMax.Item2) / 2.0;
            double x1 = 0;
            for (int i = 1; i < midCount; i++)
            {
                if ((data[i - 1].Y < midYData && data[i].Y > midYData)
                    || (data[i - 1].Y > midYData && data[i].Y < midYData))
                {
                    x1 = (data[i - 1].X + data[i].X) / 2.0;
                    break;
                }
            }
            double x2 = 0;
            for (int i = midCount; i < data.Count; i++)
            {
                if ((data[i - 1].Y < midYData && data[i].Y > midYData)
                    || (data[i - 1].Y > midYData && data[i].Y < midYData))
                {
                    x2 = (data[i - 1].X + data[i].X) / 2.0;
                    break;
                }
            }
            double midX = (x1 + x2) / 2.0;
            ICartData transData = new CartData(FileName);
            foreach (Vector3 pt in data)
            {
                transData.Add(new Vector3(pt.X - midX, pt.Y, pt.Z));
            }
            return transData;
        }
        public ICylData AsCylData()
        {
            try
            {
                var cylData = new CylData(FileName);
                var pts = new List<IPointCyl>();
                foreach (var pt in data)
                {
                    var newPt = new PointCyl(pt);
                    pts.Add(newPt);
                }
                cylData.AddRange(pts);
                return cylData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void SortByZ()
        {
            var sortList = new List<double>();
            var ptList = new List<IVector3>();
            foreach (var pt in data)
            {
                sortList.Add(pt.Z);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(sortList.ToArray(), arr);
            Clear();
            AddRange(arr);
        }
        public void SortByY()
        {
            var sortList = new List<double>();
            var ptList = new List<IVector3>();
            foreach (var pt in data)
            {
                sortList.Add(pt.Y);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(sortList.ToArray(), arr);
            Clear();
            AddRange(arr);
        }
        public void SortByX()
        {
            var sortList = new List<double>();
            var ptList = new List<IVector3>();
            foreach (var pt in data)
            {
                sortList.Add(pt.X);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(sortList.ToArray(), arr);
            Clear();
            AddRange(arr);
        }
        public void Translate(IVector3 translation)
        {
            try
            {
                var newPts = new List<IVector3>();
                foreach (IVector3 pt in data)
                {
                    var newPt = pt.Translate(translation);
                    newPts.Add(newPt);
                }
                Clear();
                AddRange(newPts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void RotateZ(double angleRad, Vector3 aboutPt)
        {
            try
            {
                var newPts = new List<IVector3>();
                foreach (IVector3 pt in data)
                {
                    var rotPt = pt.RotateZ(aboutPt, angleRad);
                    newPts.Add(rotPt);
                }
                this.Clear();
                this.AddRange(newPts);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private IDisplayData displayData;

        private void BuildDisplayData(ViewPlane viewPlane)
        {
            try
            {
                var pts = new List<PointF>();
                displayData = new DisplayData(FileName);

                displayData.Color = Color;

                foreach (IVector3 v in data)
                {
                    switch (viewPlane)
                    {
                        case ViewPlane.THETAR:
                            var ptc = new PointCyl(v);
                            pts.Add(new PointF((float)ptc.ThetaDeg, (float)ptc.R));
                            break;
                        case ViewPlane.XZ:
                            pts.Add(new PointF((float)v.X, (float)v.Z));
                            break;
                        case ViewPlane.YZ:
                            pts.Add(new PointF((float)v.Y, (float)v.Z));
                            break;
                        case ViewPlane.XY:
                        default:
                            pts.Add(new PointF((float)v.X, (float)v.Y));
                            break;
                    }

                }
                displayData.AddRange(pts);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(IVector3 vector3)
        {
            data.Add(vector3);
        }

        public void AddRange(IEnumerable<IVector3> vector3)
        {
            data.AddRange(vector3);
        }

        public void Clear()
        {
            data.Clear();
        }

        public IBoundingBox BoundingBox()
        {

            if (_boundingBox == null)
            {
                _boundingBox = GetBoundingBox();

            }
            return _boundingBox;

        }

        private BoundingBox GetBoundingBox()
        {
            try
            {
                double maxX = double.MinValue;
                double minX = double.MaxValue;
                double maxY = double.MinValue;
                double minY = double.MaxValue;
                double maxZ = double.MinValue;
                double minZ = double.MaxValue;
                foreach (var pt in data)
                {
                    double x = pt.X;
                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    double y = pt.Y;
                    if (y > maxY)
                    {
                        maxY = y;
                    }
                    if (y < minY)
                    {
                        minY = y;
                    }
                    if (pt.Z > maxZ)
                    {
                        maxZ = pt.Z;
                    }
                    if (pt.Z < minZ)
                    {
                        minZ = pt.Z;
                    }
                }

                var bb = new BoundingBox(minX, minY, minZ, maxX, maxY, maxZ);
                return bb;
            }
            catch (Exception)
            {

                throw;
            }
        }
        ICartData ICartData.Translate(IVector3 vector3)
        {
            var cd = new CartData(this.FileName);
            foreach(var v in data)
            {
                cd.Add(v.Translate(vector3));
            }
            return cd;
        }

       

        public int IndexOf(IVector3 item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, IVector3 item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public bool Contains(IVector3 item)
        {
            return data.Contains(item);
        }

        public void CopyTo(IVector3[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(IVector3 item)
        {
            return data.Remove(item);
        }

        public IEnumerator<IVector3> GetEnumerator()
        {
            return new CartDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CartDataEnumerator(this);
        }

        public ICartData Unroll(double unrollRadius)
        {
            return this;
        }

        public IVector3 GetIntersectionAtX(double x)
        {
            IVector3 result = new Vector3();
            for (int i = 0; i < Count - 1; i++)
            {
                if ((x > this[i].X && x <= this[i + 1].X) || (x <= this[i].X && x > this[i + 1].X))
                {
                    var v = new Vector3();
                    result = v.InterpolateX(x, this[i], this[i + 1]);
                }
            }
            return result;
        }

        private IBoundingBox _boundingBox;


    }
    
}
