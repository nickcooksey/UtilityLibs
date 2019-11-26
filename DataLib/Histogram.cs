using GeometryLib;
using System;
using System.Collections.Generic;
namespace DataLib
{
    /// <summary>
    /// gets various subsets of data 
    /// </summary>
    public class HistogramBucket
    {
        public double CenterValue;
        public double Count;
        public HistogramBucket(double value, double count)
        {
            CenterValue = value;
            Count = count;
        }
    }

    public class Histogram
    {
        private int _pointCount;


        internal double[] LimitBins;
        internal double[] ValueBins;
        private int _binCount;
        internal double min = double.MaxValue;
        internal double max = double.MinValue;
        internal Histogram()
        {

        }

        internal void BuildHistogram(int binCount, List<double> inputValues, bool normalize)
        {
            _binCount = binCount;
            //find min and max error

            foreach (var e in inputValues)
            {
                if (e < min)
                    min = e;
                if (e > max)
                    max = e;
            }

            double range = max - min;
            double binSize = range / _binCount;
            LimitBins = new double[_binCount];
            ValueBins = new double[_binCount];

            //get bins limits
            for (int i = 0; i < _binCount; i++)
            {
                LimitBins[i] = min + (i * binSize);
            }
            //fill bins
            foreach (var e in inputValues)
            {
                for (int i = 0; i < _binCount - 1; i++)
                {
                    if (e >= LimitBins[i] && e < LimitBins[i + 1])
                    {
                        ValueBins[i]++;
                        _pointCount++;
                        break;
                    }
                }
            }
            if (normalize && _pointCount > 0)
            {
                for (int i = 0; i < ValueBins.Length; i++)
                {
                    ValueBins[i] /= _pointCount;
                }
            }

        }

    }
}
