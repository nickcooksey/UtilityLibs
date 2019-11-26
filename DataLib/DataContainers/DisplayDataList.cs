using IDataLib;
using System;
using System.Collections;
using System.Collections.Generic;
using DataLib.DataContainers;

namespace DataLib
{
    public class DisplayDataList : IDisplayDataList
    {
       
        public IDisplayData this[int index] { get { return data[index]; } set { data[index] = value; } }

        public int Count { get { return data.Count; } }

        public bool IsReadOnly { get { return false; } }

        private List<IDisplayData> data;
        public DisplayDataList()
        {
            data = new List<IDisplayData>();
        }
        public void Add(IDisplayData item)
        {
            data.Add(item);
        }
        public void AddRange(IEnumerable<IDisplayData> dataSets)
        {
            data.AddRange(dataSets);
        }
        public void Clear()
        {
            data.Clear();
        }
        public int FindNearestFileIndex(System.Drawing.PointF mousePt, ref System.Drawing.PointF minPt)
        {
            try
            {
                double minDist2 = double.MaxValue;
                string filename = "";
                int nearestIndex = 0;

                for (int index = 0; index < Count; index++)
                {
                    foreach (var p in this[index])
                    {
                        var dist2 = Math.Pow(p.X - mousePt.X, 2) + Math.Pow(p.Y - mousePt.Y, 2);
                        if (dist2 < minDist2)
                        {
                            minDist2 = dist2;
                            minPt = new System.Drawing.PointF(p.X, p.Y);
                            filename = this[index].ShortFileName;
                            nearestIndex = index;
                        }
                    }

                }
                return nearestIndex;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Contains(IDisplayData item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IDisplayData[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }
        public int IndexOf(IDisplayData item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IDisplayData item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IDisplayData item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DisplayDataListEnumerator(this);
        }
        public IEnumerator<IDisplayData> GetEnumerator()
        {
            return new DisplayDataListEnumerator(this);
        }
    }
  
}
