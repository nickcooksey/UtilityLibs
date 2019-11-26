using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using ICNCLib;
namespace CNCLib
{
    public  class ToolpathEnumerator:IEnumerator<IPathEntity>
    {
        private IToolpath collection;
        private int curIndex;
        private IPathEntity curItem;


        public ToolpathEnumerator(IToolpath collection)
        {
            this.collection = collection;
            curIndex = -1;
            curItem = default;
        }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= collection.Count)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                curItem = collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }
        public IPathEntity Current
        {
            get { return curItem; }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
