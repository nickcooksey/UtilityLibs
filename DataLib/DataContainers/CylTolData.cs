using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DataLib.DataContainers;


namespace DataLib
{
   

    public class CylTolData : ICylTolData
    {
        


        public System.Drawing.Color Color { get; set; }
        public string FileName { get;  set; }
        public double MinRadius { get; set; }

        private List<IPointCylTol> data;

        public CylTolData()
        {
            data = new List<IPointCylTol>();
            Color = System.Drawing.Color.Blue;
            FileName = "";
        }
        public CylTolData(string filename)
        {
            data = new List<IPointCylTol>();
            Color = System.Drawing.Color.Blue;
            FileName = filename;
        }
        public void ShiftByTheta(double thetaRad)
        {
            try
            {
                foreach (var pt in this)
                {
                    if (pt.ThetaRad < thetaRad)
                        pt.ThetaRad += Math.PI * 2.0;
                }
                SortByTheta();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ICylData AsCylData(TolType tolType)
        {
            try
            {
                var pts = new CylData();
                foreach (var v in this)
                    switch (tolType)
                    {
                        case TolType.MAX:
                            pts.Add(new PointCyl(v.RMax, v.ThetaRad, v.Z));
                            break;
                        case TolType.MIN:
                            pts.Add(new PointCyl(v.RMin, v.ThetaRad, v.Z));
                            break;
                        case TolType.NOM:
                            pts.Add(new PointCyl(v.RNom, v.ThetaRad, v.Z));
                            break;
                    }
                return pts;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public DisplayData AsDisplayData(ViewPlane viewplane, TolType tolType)
        {

            var displayData = new DisplayData(FileName);
            switch (tolType)
            {
                case TolType.MAX:
                    displayData.Color = Color.Red;
                    break;
                case TolType.MIN:
                    displayData.Color = Color.Blue;
                    break;
                case TolType.NOM:
                    displayData.Color = Color.Green;
                    break;
            }
            displayData.Color = Color;
            foreach (var v in this)
            {
                switch (viewplane)
                {
                    case ViewPlane.ZR:
                        switch (tolType)
                        {
                            case TolType.MAX:
                                displayData.Add(new PointF((float)v.Z, (float)v.RMax));
                                break;
                            case TolType.MIN:
                                displayData.Add(new PointF((float)v.Z, (float)v.RMin));
                                break;
                            case TolType.NOM:
                                displayData.Add(new PointF((float)v.Z, (float)v.RNom));
                                break;
                        }

                        break;
                    case ViewPlane.THETAR:
                    default:
                        switch (tolType)
                        {
                            case TolType.MAX:
                                displayData.Add(new PointF((float)(v.ThetaDeg), (float)v.RMax));
                                break;
                            case TolType.MIN:
                                displayData.Add(new PointF((float)(v.ThetaDeg), (float)v.RMin));
                                break;
                            case TolType.NOM:
                                displayData.Add(new PointF((float)(v.ThetaDeg), (float)v.RNom));
                                break;
                        }

                        break;
                }

            }

            return displayData;
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


        public int Count { get { return data.Count; } }

        public bool IsReadOnly { get { return false; } }

        public IPointCylTol this[int index] { get { return data[index]; } set { data[index] = value; } }

        public CartData AsCartData()
        {
            try
            {
                var cartData = new CartData(FileName);
                var pts = new List<Vector3>();
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
                var thetaShift = (pt.ThetaRad + phaseShiftRad);
                pt.ThetaRad = thetaShift;
            }
            SortByTheta();
        }
        public Tuple<double, double> GetMinMaxR()
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
        public void SortByR()
        {
            var rList = new List<double>();
            var ptList = new List<IPointCylTol>();
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
            var ptList = new List<IPointCylTol>();
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
            var ptList = new List<IPointCylTol>();
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

        public void AddRange(IEnumerable<IPointCylTol> vector3)
        {
            data.AddRange(vector3);
        }

        public IList<IPointCylTol> TrimToWindow(System.Drawing.RectangleF rectangleF, ViewPlane viewPlane)
        {
            try
            {
                var winData = new List<IPointCylTol>();
                var dd = AsDisplayData(viewPlane);
                IPointCylTol trimPt;
                foreach (var pt in dd)
                {
                    if(rectangleF.Contains(pt))
                    //if (pt.X >= minX && pt.X <= maxX && pt.Y >= minY && pt.Y < maxY)
                    {
                        switch (viewPlane)
                        {
                            case ViewPlane.THETAR:
                                trimPt = new PointCylTol()
                                {
                                    R = pt.Y,
                                    ThetaRad = PointCyl.ToRadians(pt.X),
                                    Z = 0
                                };
                                winData.Add(trimPt);
                                break;
                            case ViewPlane.ZR:
                                trimPt = new PointCylTol()
                                {
                                    R = pt.Y,
                                    ThetaRad = 0,
                                    Z = pt.X
                                };
                                winData.Add(trimPt);
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

        public IDisplayData AsDisplayData(ViewPlane viewplane)
        {
            throw new NotImplementedException();
        }

        public List<string> AsCSV()
        {
            throw new NotImplementedException();
        }
        IBoundingBox boundingBox;
        public IBoundingBox BoundingBox()
        {
            if (boundingBox == null)
                boundingBox = GetBoundingBox();
            return boundingBox;

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
                    double x = pt.RMax * Math.Cos(pt.ThetaRad);
                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    double y = pt.RMax * Math.Sin(pt.ThetaRad);
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
        public int IndexOf(IPointCylTol item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, IPointCylTol item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(IPointCylTol item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(IPointCylTol item)
        {
            return data.Contains(item);
        }

        public void CopyTo(IPointCylTol[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(IPointCylTol item)
        {
            return data.Remove(item);
        }

        public IEnumerator<IPointCylTol> GetEnumerator()
        {
            return new CylTolDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CylTolDataEnumerator(this);
        }
    }
   
}
