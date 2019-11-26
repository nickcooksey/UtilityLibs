using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace DataLib
{



    public class CylData : ICylData
    {
        private List<IPointCyl> data;
        public double MinRadius { get; set; }
        public string FileName { get; set; }
        public System.Drawing.Color Color { get; set; }

        public int Count { get { return data.Count; } }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IPointCyl this[int index] { get { return data[index]; } set { data[index] = value; } }

        private DisplayData displayData;

        public CylData(string filename)
        {
            Color = System.Drawing.Color.Blue;
            FileName = filename;
            data = new List<IPointCyl>();
        }
        public CylData()
        {
            Color = System.Drawing.Color.Blue;
            FileName = "";
            data = new List<IPointCyl>();
        }


        private string CSVHeader()
        {
            return PointCyl.CSVHeader();

        }

        public List<string> AsCSV()
        {
            try
            {
                var file = new List<string>();
                file.Add(CSVHeader());
                foreach (var pt in this)
                {
                    string s = string.Concat(pt.R.ToString(), ",", pt.ThetaDeg.ToString(), ",", pt.Z.ToString());
                    file.Add(s);
                }
                return file;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void TrimWidth(double minAngleRads,double maxAngleRads)
        {
            try
            {
                var winData = new CylData();
                foreach(var pt in data)
                {
                    if(pt.ThetaRad>=minAngleRads && pt.ThetaRad<=maxAngleRads)
                    {
                        winData.Add(pt);
                    }
                }
                Clear();
                AddRange(winData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IList<IPointCyl> TrimToWindow(System.Drawing.RectangleF rectangleF, ViewPlane viewPlane)
        {
            try
            {
                var winData = new CylData();
                var dd = AsDisplayData(viewPlane);
                foreach (var pt in dd)
                {
                    if(rectangleF.Contains(pt))
                    //if (pt.X >= minX && pt.X <= maxX && pt.Y >= minY && pt.Y < maxY)
                    {
                        switch (viewPlane)
                        {
                            case ViewPlane.THETAR:
                                winData.Add(new PointCyl(pt.Y, PointCyl.ToRadians(pt.X), 0));
                                break;
                            case ViewPlane.ZR:
                                winData.Add(new PointCyl(pt.Y, 0, pt.X));
                                break;
                            case ViewPlane.XY:
                                break;
                            case ViewPlane.XZ:
                                break;
                            case ViewPlane.YZ:
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
        public IDisplayData AsPolarViewDisplayData()
        {
            try
            {
                displayData = new DisplayData(FileName);
                displayData.Color = Color;
                foreach (IPointCyl v in this)
                {
                    displayData.Add(new PointF((float)(Math.Cos(v.ThetaRad) * v.R), (float)(Math.Sin(v.ThetaRad) * v.R)));
                }
                return displayData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICylData CenterToFirstGrooveMidpoint()
        {
            try
            {
                int midCount = (int)Math.Round(data.Count / 2.0);
                var minMax = GetMinMaxR();

                var rAve = (minMax.Item1 + minMax.Item2) / 2.0;
                var values = new List<double>();
                for (int i = 0; i < data.Count; i++)
                {
                    values.Add(data[i].R);
                }
                var interIndex = GetIntersectionIndicesAt(rAve, values);
                int iMid = (interIndex[interIndex.Count - 1] + interIndex[0]) / 2;
                double thetaMid = data[iMid].ThetaRad;

                var transData = new CylData(FileName);
                foreach (var pt in data)
                {
                    transData.Add(new PointCyl(pt.R, pt.ThetaRad - thetaMid, pt.Z));
                }
                return transData;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private List<int> GetIntersectionIndicesAt(double testValue, List<double> points)
        {
            try
            {
                var indexList = new List<int>();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    if ((testValue >= points[i] && testValue < points[i + 1])
                        || (testValue < points[i] && testValue >= points[i + 1]))
                    {
                        indexList.Add(i);
                    }
                }
                return indexList;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ICartData Unroll(double scaling, double unrollRadius)
        {
            try
            {
                var strip = new CartData(FileName);
                for (int i = 0; i < Count; i++)
                {
                    strip.Add(this[i].UnrollCylPt(scaling, unrollRadius));
                }
                return strip;
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
        public IDisplayData AsDisplayData(ViewPlane viewplane)
        {
            try
            {
                displayData = new DisplayData(FileName);
                displayData.Color = Color;
                foreach (PointCyl v in this)
                {
                    switch (viewplane)
                    {
                        case ViewPlane.ZR:
                            displayData.Add(new PointF((float)v.Z, (float)v.R));
                            break;
                        case ViewPlane.THETAR:
                        default:
                            displayData.Add(new PointF((float)(v.ThetaDeg), (float)v.R));

                            break;
                    }

                }

                displayData.SortByX();

                return displayData;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Translate(IVector3 translation)
        {
            try
            {
                var cartData = this.AsCartData();
                cartData.Translate(translation);

                var transData = new List<IPointCyl>();
                foreach (var pt in this)
                {
                    transData.Add(pt.Translate(translation));
                }
                Clear();
                AddRange(transData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICartData AsCartData()
        {
            try
            {
                var cartData = new CartData(FileName);
                var pts = new List<IVector3>();
                foreach (var pt in this)
                {
                    var newPt = new Vector3(pt);
                    pts.Add(newPt);
                }
                cartData.AddRange(pts);
                return cartData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void RotateByThetaRad(double phaseShiftRad)
        {
            foreach (var pt in this)
            {
                var thetaShift = (pt.ThetaRad + phaseShiftRad) % (Math.PI * 2.0);
                pt.ThetaRad = thetaShift;
            }
            SortByTheta();
        }

        private int GetMinRadiusIndex()
        {
            try
            {
                double minR = double.MaxValue;
                int minPtIndex = 0;

                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].R < minR)
                    {
                        minPtIndex = i;
                        minR = this[i].R;
                    }
                }
                return minPtIndex;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IPointCyl GetMinRadiusPt()
        {
            try
            {
                return this[GetMinRadiusIndex()];
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void RotateAbtZ(double thetaRad)
        {
            try
            {

                foreach (var pt in this)
                {
                    pt.ThetaRad += thetaRad;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetMinAveRadius(int searchWindowHalfW)
        {
            try
            {
                var minPtIndex = GetMinRadiusIndex();
                int startSearch = Math.Max(0, minPtIndex - searchWindowHalfW);
                int endSearch = Math.Min(Count - 1, minPtIndex + searchWindowHalfW);
                double aveCt = 0;
                double sumR = 0;
                for (int j = startSearch; j < endSearch; j++)
                {
                    sumR += this[j].R;
                    aveCt++;
                }
                sumR /= aveCt;
                return sumR;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Tuple<double, double> GetMinMaxR()
        {
            try
            {
                double maxYData = double.MinValue;
                double minYData = double.MaxValue;
                foreach (var pt in this)
                {
                    if (pt.R > maxYData)
                    {
                        maxYData = pt.R;
                    }
                    if (pt.R < minYData)
                    {
                        minYData = pt.R;
                    }
                }
                return new Tuple<double, double>(minYData, maxYData);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public double GetAveR()
        {
            try
            {
                double ave = 0;
                foreach (var pt in this)
                {
                    ave += pt.R;
                }
                ave /= Count;
                return ave;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SortByR()
        {
            var rList = new List<double>();
            var ptList = new List<IPointCyl>();
            foreach (var pt in this)
            {
                rList.Add(pt.R);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(rList.ToArray(), arr);
            this.Clear();
            this.AddRange(arr);
        }
        public void SortByZ()
        {
            var zList = new List<double>();
            var ptList = new List<IPointCyl>();
            foreach (var pt in this)
            {
                zList.Add(pt.Z);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(zList.ToArray(), arr);
            this.Clear();
            this.AddRange(arr);
        }
        public void SortByTheta()
        {
            var thetaList = new List<double>();
            var ptList = new List<IPointCyl>();
            foreach (var pt in this)
            {
                thetaList.Add(pt.ThetaRad);
                ptList.Add(pt);
            }
            var arr = ptList.ToArray();
            Array.Sort(thetaList.ToArray(), arr);
            this.Clear();
            this.AddRange(arr);
        }

        private IBoundingBox _boundingBox;
        public IBoundingBox BoundingBox()
        {
            if(_boundingBox==null)
            {
                _boundingBox = GetBoundingBox();
            }
            return _boundingBox;
        }
        IBoundingBox GetBoundingBox()
        {
            try
            {
                double maxX = double.MinValue;
                double minX = double.MaxValue;
                double maxY = double.MinValue;
                double minY = double.MaxValue;
                double maxZ = double.MinValue;
                double minZ = double.MaxValue;
                foreach (var pt in this)
                {
                    double x = pt.R*Math.Cos(pt.ThetaRad);
                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    double y = pt.R*Math.Sin(pt.ThetaRad);
                    if (y > maxY)
                    {
                        maxY = y;
                    }
                    if (y < minY)
                    {
                        minY = y;
                    }
                    double z = pt.Z;
                    if (z > maxZ)
                    {
                        maxZ = z;
                    }
                    if (z < minZ)
                    {
                        minZ = z;
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

        public void AddRange(IEnumerable<IPointCyl> points)
        {
            data.AddRange(points);
        }

        public int IndexOf(IPointCyl item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, IPointCyl item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(IPointCyl item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(IPointCyl item)
        {
            return data.Contains(item);
        }

        public void CopyTo(IPointCyl[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(IPointCyl item)
        {
            return data.Remove(item);
        }

        public IEnumerator<IPointCyl> GetEnumerator()
        {
            return new CylDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CylDataEnumerator(this);
        }
    }
    
}
