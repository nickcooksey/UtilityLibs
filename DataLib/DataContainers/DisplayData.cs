using IDataLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DataLib.DataContainers;

namespace DataLib
{


    public class DisplayData : IDisplayData
    {
        public string FileName { get; set; }
        public System.Drawing.Color Color { get; set; }

        private List<System.Drawing.PointF> data;

        public double DataViewLocation { get; set; }


        public string ShortFileName
        {
            get
            {
                return System.IO.Path.GetFileName(FileName);
            }
        }

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly { get { return false; } }

        public PointF this[int index] { get { return data[index]; } set { data[index] = value; } }
        public DisplayData()
        {
            Color = System.Drawing.Color.Blue;
            data = new List<System.Drawing.PointF>();
            boundingRect = new RectangleF();
        }
        public DisplayData(string filename)
        {
            FileName = filename;
            Color = System.Drawing.Color.Blue;
            data = new List<System.Drawing.PointF>();
            boundingRect = new RectangleF();

        }
        Tuple<double, double> GetMinMaxY()
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
        public void SortByX()
        {
            try
            {
                var xList = new List<double>();
                foreach (PointF pt in data)
                {
                    xList.Add(pt.X);
                }
                var ptArr = data.ToArray();
                Array.Sort(xList.ToArray(), ptArr);
                data.Clear();
                data.AddRange(ptArr);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Translate(PointF trans)
        {
            try
            {
                var tlist = new List<PointF>();
                for (int i = 0; i < data.Count; i++)
                {
                    var x = data[i].X + trans.X;
                    var y = data[i].Y + trans.Y;
                    tlist.Add(new PointF(x, y));
                }
                data.Clear();
                data.AddRange(tlist);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DisplayData CenterToXMidpoint()
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
            var transData = new DisplayData(FileName);
            foreach (var pt in data)
            {

                transData.Add(new System.Drawing.PointF((float)(pt.X - midX), pt.Y));
            }
            return transData;
        }
        public IDisplayData TrimToWindow(RectangleF window)
        {
            try
            {

                var trimmedDisplay = new DisplayData(this.FileName);

                var xList = new List<double>();
                var ptList = new List<PointF>();

                foreach (PointF pt in data)
                {
                    if( window.Contains(pt))
                    {
                        ptList.Add(new PointF(pt.X, pt.Y));
                        xList.Add(pt.X);
                    }
                    //if (pt.X >= window.Left && pt.X <= window.Right)
                    //{
                    //    if (window.Bottom <= 0 && window.Top <= 0 && pt.Y < 0)
                    //    {
                    //        ptList.Add(new PointF(pt.X, pt.Y));
                    //        xList.Add(pt.X);
                    //    }
                    //    if (window.Bottom >= 0 && window.Top >= 0 && pt.Y > 0)
                    //    {
                    //        ptList.Add(new PointF(pt.X, pt.Y));
                    //        xList.Add(pt.X);
                    //    }
                    //}
                }
                var ptArr = ptList.ToArray();

                Array.Sort(xList.ToArray(), ptArr);

                trimmedDisplay.AddRange(ptArr.ToList());
                trimmedDisplay.Color = Color;
                return trimmedDisplay;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private RectangleF boundingRect;
        public RectangleF GetBoundingRect(float borderPercent, int decimalPlaces)
        {
            try
            {

                boundingRect = GetBoundingRect();

                double round = Math.Pow(10, decimalPlaces);
                float bordery = boundingRect.Height * borderPercent / 2;
                float minYRound = (float)(Math.Floor((boundingRect.Y - bordery) * round) / round);
                float maxYRound = (float)(Math.Ceiling((boundingRect.Y + boundingRect.Height + bordery) * round) / round);
                return new RectangleF(boundingRect.X, minYRound, boundingRect.Width, (float)(maxYRound - minYRound));
            }
            catch (Exception)
            {

                throw;
            }

        }
        public RectangleF GetBoundingRect()
        {
            try
            {
                float maxX = float.MinValue;
                float minX = float.MaxValue;
                float maxY = float.MinValue;
                float minY = float.MaxValue;
                foreach (PointF pt in data)
                {
                    maxX = Math.Max(pt.X, maxX);
                    minX = Math.Min(pt.X, minX);
                    maxY = Math.Max(pt.Y, maxY);
                    minY = Math.Min(pt.Y, minY);
                }
                return new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddRange(IEnumerable<PointF> pointFs)
        {
            data.AddRange(pointFs);
        }

        public int IndexOf(PointF item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, PointF item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(PointF item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(PointF item)
        {
            return data.Contains(item);
        }

        public void CopyTo(PointF[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(PointF item)
        {
            return data.Remove(item);
        }

        public IEnumerator<PointF> GetEnumerator()
        {
            return new DisplayDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DisplayDataEnumerator(this);
        }
    }

    
}
