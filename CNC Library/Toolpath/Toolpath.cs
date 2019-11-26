using System;
using System.Collections;
using System.Collections.Generic;
using ICNCLib;

namespace CNCLib
{
    public class ToolPath : IToolpath
    {
        public int[] MiscIntegerArr { get; set; }
        public double[] MiscRealArr { get; set; }
        public int ProgNumber { get; set; }
        public int SeqIncrement { get; set; }
        public int StartNumber { get; set; }
        public int ToolNumber { get; set; }
        public int ToolDiamNumber { get; set; }
        public int ToolLengthNumber { get; set; }
        public double NomFeedrate { get; set; }
        public IMachinePosition Home { get; set; }         
        public double OffsetDist { get; set; }
        public double ToolDiameter { get; set; }
        public int OpCode { get; set; }
        public int CutPathCount { get; set; }
        public string FilePath { get; set; }
        public string OutputFileName { get; set; }
        public string InputFileName { get; set; }
        public string Title { get; set; }
        private List<IPathEntity> data;

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly { get { return false; } }

        public IPathEntity this[int index] { get { return data[index]; } set { data[index] = value; } }

        public ToolPath()
        {
            MiscIntegerArr = new int[10];
            MiscRealArr = new double[10];
            FilePath = "";
            OutputFileName = "";
            InputFileName = "";
            Title = "";
            data = new List<IPathEntity>();
        }
        public void FixRollovers()
        {

            foreach (var pe in this)
            {
                double deltaB = pe.Position.Bdeg - pe.PrevPosition.Bdeg;
                double deltaC = pe.Position.Cdeg - pe.PrevPosition.Cdeg;
                if (Math.Abs(deltaC) > 90)
                {

                }
            }
        }
        public void FixWrapArounds(double minCaxis, double maxCaxis)
        {

        }
        public List<string> InputPath()
        {

            var _input = new List<string>();
            foreach (var pe in this)
            {
                _input.Add(pe.InputString);
            }
            return _input;
        }

        public int IndexOf(IPathEntity item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, IPathEntity item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(IPathEntity item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(IPathEntity item)
        {
            return data.Contains(item);
        }

        public void CopyTo(IPathEntity[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(IPathEntity item)
        {
            return data.Remove(item);
        }

        public IEnumerator<IPathEntity> GetEnumerator()
        {
            return new ToolpathEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ToolpathEnumerator(this);
        }

       

    }

}
