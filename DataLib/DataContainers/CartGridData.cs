using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataLib
{
    public class CartGridData : ICartGridData
    {
       
        public string FileName { get; set; }
        public int Count
        {
            get { return data.Count; }
        }
        public bool IsReadOnly { get { return false; } }

        public ICartData this[int index] { get { return data[index]; } set { data[index] = value; } }

        private List<ICartData> data;
        public CartGridData()
        {
            data = new List<ICartData>();
        }
        public CartGridData(string filename)
        {
            this.FileName = filename;
            data = new List<ICartData>();
        }
        IBoundingBox boundingBox;
        public IBoundingBox  BoundingBox()
        {
            if(boundingBox==null)
            {
                boundingBox = GetBoundingBox();
            }
            return boundingBox;
        }
        private IBoundingBox GetBoundingBox()
        {
            try
            {
                var bblist = new List<IBoundingBox>();
                foreach(var cd in data)
                {
                    bblist.Add(cd.BoundingBox());
                }
                return GeometryLib.BoundingBoxBuilder.Union(bblist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddRange(IEnumerable<ICartData> vector3s)
        {
            data.AddRange(vector3s);
        }
        public int IndexOf(ICartData item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ICartData item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(ICartData item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(ICartData item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ICartData[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ICartData item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ICartData> GetEnumerator()
        {
            return new CartGridDataEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CartGridDataEnumerator(this);
        }
    }
  
}
