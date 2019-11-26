using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;
namespace DataLib
{
    public class CylGridData : ICylGridData
    {
        public string FileName { get; set; }
        public System.Drawing.Color Color { get; set; }
        public int Count
        {
            get { return data.Count; }
        }
        public bool IsReadOnly { get { return false; } }

        public ICylData this[int index] { get { return data[index]; } set { data[index] = value; } }
        private List<ICylData> data;
        public CylGridData()
        {
            data = new List<ICylData>();
        }
        public CylGridData(string filename)
        {
            this.FileName = filename;
            data = new List<ICylData>();
        }
        IBoundingBox boundingBox;
        public IBoundingBox BoundingBox()
        {
            if (boundingBox == null)
                boundingBox = GetBB();
            return boundingBox;
        }
        private IBoundingBox GetBB()
        {
            try
            {
                double maxX = double.MinValue;
                double minX = double.MaxValue;
                double maxY = double.MinValue;
                double minY = double.MaxValue;
                double maxZ = double.MinValue;
                double minZ = double.MaxValue;
                foreach (var strip in data)
                {
                    foreach (var pt in strip)
                    {
                        double x =Math.Cos(pt.ThetaRad)* pt.R;
                        if (x > maxX)
                        {
                            maxX = x;
                        }
                        if (x < minX)
                        {
                            minX = x;
                        }
                        double y = Math.Sin(pt.ThetaRad)*pt.R;
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
                }


                var bb = new BoundingBox(minX, minY, minZ, maxX, maxY, maxZ);
                return bb;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// convert cylinder grid to cartesian grid
        /// </summary>
        /// <param name="correctedRingList"></param>
        /// <returns></returns>
        public CartGridData AsCartGridData()
        {
            try
            {
                var stripList = new CartGridData();
                foreach (var cylstrip in this)
                {
                    var strip = new CartData(cylstrip.FileName);
                    foreach (var ptCyl in cylstrip)
                    {
                        strip.Add(new Vector3(ptCyl));
                    }
                    stripList.Add(strip);
                }
                return stripList;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ICylData AsCylData()
        {

            var stripd = new CylData(this[0].FileName);
            foreach (var strip in this)
            {
                foreach (var pt in strip)
                {
                    var ptnew = new PointCyl(pt.R, pt.ThetaRad, pt.Z, pt.Col, pt.ID);
                    stripd.Add(ptnew);
                }
            }
            return stripd;
        }
        public ICartGridData Unroll(double scaling, double unrollRadius)
        {
            try
            {
                ICartGridData stripList = new CartGridData();
                int i = 0;
                double dTh = 0;
                if (Count > 1)
                {
                    var strip0 = this[0];
                    var strip1 = this[1];
                    dTh = strip1[0].ThetaRad - strip0[0].ThetaRad;
                }
                foreach (ICylData cylstrip in this)
                {
                    cylstrip.RotateAbtZ(-1 * i * dTh);
                    stripList.Add(cylstrip.Unroll(scaling, unrollRadius));
                    i++;
                }
                return stripList;
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
                    foreach (var strip in this)
                    {
                        for (int i = 1; i < strip.Count - 1; i++)
                        {
                            var line = new Line3(strip[i], strip[i + 1]);
                            line.Col = color;
                            entityList.Add(line);
                        }
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
        public int IndexOf(ICylData item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, ICylData item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(ICylData item)
        {
            data.Add(item);
        }
        public void AddRange(IEnumerable<ICylData> pointCyls)
        {
            data.AddRange(pointCyls);
        }
        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(ICylData item)
        {
            return data.Contains(item);
        }

        public void CopyTo(ICylData[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(ICylData item)
        {
            return data.Remove(item);
        }

        public IEnumerator<ICylData> GetEnumerator()
        {
            return new CylGridDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CylGridDataEnumerator(this);
        }

        


    }
   
}
