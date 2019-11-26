using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataLib;
using System.Collections;

namespace DataLib.DataContainers
{
    public class DisplayDataEnumerator : IEnumerator<System.Drawing.PointF>
    {
        private IDisplayData _collection;
        private int curIndex;
        private System.Drawing.PointF currentItem;


        public DisplayDataEnumerator(IDisplayData collection)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {

        }

        public System.Drawing.PointF Current
        {
            get { return currentItem; }
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }

    }

}
