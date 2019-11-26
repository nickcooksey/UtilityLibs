using System;
using System.Collections.Generic;

namespace DataLib
{
    public class FourierPt
    {
        public double Frequency { get; set; }
        public double Amplitude { get; set; }

    }
  
    public class DerivAnalysis
    {
        public static List<HistogramBucket> GetDerivHistogram(List<double> data, int bucketCount)
        {
            try
            {
                var derivs = new List<double>();
                double max = double.MinValue;
                double min = double.MaxValue;
                for (int i = 0; i < data.Count - 1; i++)
                {
                    var p1 = data[i + 1];
                    var p0 = data[i];
                    var d = Math.Abs(p1 - p0);
                    if (d < min)
                        min = d;
                    if (d > max)
                        max = d;
                    derivs.Add(d);
                }

                double deltaD = (max - min) / bucketCount;
                var h = new MathNet.Numerics.Statistics.Histogram(derivs, bucketCount, min, max);
                var bucketV = min + (deltaD / 2.0);
                var histogram = new List<HistogramBucket>();

                while (bucketV <= max)
                {
                    var b = h.GetBucketOf(bucketV);
                    histogram.Add(new HistogramBucket(bucketV, b.Count));
                    bucketV += deltaD;
                }
                return histogram;
                //return String.Concat("%min, %max Derivatives : ", bmin.ToString("f4"), " , ", bmax.ToString("f4"));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GetDerivHistogram(CylData cylData)
        {
            try
            {
                var derivs = new List<double>();
                double max = double.MinValue;
                double min = double.MaxValue;
                for (int i = 0; i < cylData.Count - 1; i++)
                {
                    var p1 = cylData[i + 1];
                    var p0 = cylData[i];
                    var d = Math.Abs((p1.R - p0.R) / (p1.ThetaRad - p0.ThetaRad));
                    if (d < min)
                        min = d;
                    if (d > max)
                        max = d;
                    derivs.Add(d);
                }

                //double deltaD = (max - min) / 20;
                var h = new MathNet.Numerics.Statistics.Histogram(derivs, 10, min, max);
                var bmax = 100 * h.GetBucketOf(max).Count / h.DataCount;
                var bmin = 100 * h.GetBucketOf(min).Count / h.DataCount;

                return String.Concat("%min, %max Derivatives : ", bmin.ToString("f4"), " , ", bmax.ToString("f4"));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
    public class FreqAnalysis
    {

        public static List<string> GetFFT(CylData input)
        {
            try
            {
                int len = 0;
                var freqs = new List<string>();
                var fourierOptions = MathNet.Numerics.IntegralTransforms.FourierOptions.Default;
                if (input.Count % 2 == 0)
                {
                    len = input.Count + 2;
                }
                else
                {
                    len = input.Count + 1;
                }
                var data = new double[len];
                for (int j = 0; j < input.Count; j++)
                {
                    data[j] = input[j].R;
                }
                double sampleRate = input.Count;
                var fft = GetFFT(data, input.Count, sampleRate, fourierOptions);

                freqs.Add("F,   Amp");
                foreach (var f in fft)
                {
                    string s = string.Concat(f.Frequency.ToString("f5"), ",    ", f.Amplitude.ToString("f5"));
                    freqs.Add(s);
                }
                return freqs;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static FourierPt[] GetFFT(double[] input, double sampleRate)
        {
            try
            {
                int len = 0;
                var fourierOptions = MathNet.Numerics.IntegralTransforms.FourierOptions.NoScaling;
                if (input.Length % 2 == 0)
                {
                    len = input.Length + 2;
                }
                else
                {
                    len = input.Length + 1;
                }
                var data = new double[len];
                for (int j = 0; j < input.Length; j++)
                {
                    data[j] = input[j];
                }
                return GetFFT(data, input.Length, sampleRate, fourierOptions);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static FourierPt[] GetFFT(double[] data, int inputLength, double sampleRate, MathNet.Numerics.IntegralTransforms.FourierOptions fourierOptions)
        {
            MathNet.Numerics.IntegralTransforms.Fourier.ForwardReal(data, inputLength, fourierOptions);
            var freqs = MathNet.Numerics.IntegralTransforms.Fourier.FrequencyScale(inputLength, sampleRate);
            var fftOut = new List<FourierPt>();

            for (int i = 0; i < inputLength - 1; i += 2)
            {
                if (freqs[i] >= 0)
                {
                    var mag = Math.Sqrt(Math.Pow(data[i], 2) + Math.Pow(data[i + 1], 2.0));
                    fftOut.Add(new FourierPt { Frequency = i / 2.0, Amplitude = mag });
                }

            }
            return fftOut.ToArray();
        }

    }
}
