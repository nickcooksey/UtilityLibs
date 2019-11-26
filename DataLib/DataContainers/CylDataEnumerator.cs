using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace DataLib
{
    public class CylDataEnumerator : IEnumerator<IPointCyl>
    {
        private ICylData _collection;
        private int curIndex;
        private IPointCyl currentItem;


        public CylDataEnumerator(ICylData collection)
        {
            _collection = collection;
            curIndex = -1;
            currentItem = default;

        }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                currentItem = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }
        bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }
        public IPointCyl Current
        {
            get { return currentItem; }
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }

    }
}
