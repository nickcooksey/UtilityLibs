using GeometryLib;
using IDataLib;
using IGeometryLib;
using System;
using System.Collections.Generic;
namespace DataLib
{

    public class FitData
    {
        private class SegmentFitData
        {
            public IPointCyl StartPoint { get; set; }
            public IPointCyl EndPoint { get; set; }
            public Func<double, double> FitFunction { get; set; }
        }

        private List<SegmentFitData> dataSegments;
        private bool _segmentFit;
        private int _polyOrder;
        public FitData(bool segmentFit, int polyOrder)
        {
            _segmentFit = segmentFit;
            _polyOrder = polyOrder;
            dataSegments = new List<SegmentFitData>();

        }
       
        public ICylData CorrectData(ICylData points)
        {
            try
            {
                var result = new CylData(points.FileName);
                points.SortByTheta();
                for (int i = 0; i < points.Count; i++)
                {
                    var inputPoint = points[i];
                    if (_segmentFit)
                    {
                        foreach (SegmentFitData segment in dataSegments)
                        {

                            if (inputPoint.ThetaRad >= segment.StartPoint.ThetaRad && inputPoint.ThetaRad < segment.EndPoint.ThetaRad)
                            {
                                double r = inputPoint.R - segment.FitFunction(inputPoint.ThetaRad);
                                var pt = new PointCyl(r, inputPoint.ThetaRad, inputPoint.Z);
                                result.Add(pt);
                                break;
                            }

                        }
                    }
                    else
                    {
                        double r = inputPoint.R - dataSegments[0].FitFunction(inputPoint.ThetaRad);
                        var pt = new PointCyl(r, inputPoint.ThetaRad, inputPoint.Z);
                        result.Add(pt);
                    }
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void CalcFitCoeffs(ICylData points)
        {
            try
            {

                if (points.Count <= _polyOrder)
                {
                    _polyOrder = points.Count - 1;
                }

                if (_segmentFit)
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        var segment = new CylData(points.FileName);
                        var p1 = points[i];
                        var p2 = points[i + 1];
                        segment.Add(p1);
                        segment.Add(p2);
                        var func = GetFunc(segment);
                        var segFit = new SegmentFitData()
                        {
                            StartPoint = p1,
                            EndPoint = p2,
                            FitFunction = func
                        };
                        dataSegments.Add(segFit);
                    }
                }
                else
                {
                    var func = GetFunc(points);
                    var segFit = new SegmentFitData()
                    {
                        StartPoint = points[0],
                        EndPoint = points[points.Count - 1],
                        FitFunction = func
                    };
                    dataSegments.Add(segFit);
                }

            }
            catch (Exception)
            {

                throw;
            }


        }
        private Func<double, double> GetFunc(ICylData points)
        {
            try
            {
                double[] x = new double[points.Count];
                double[] y = new double[points.Count];

                for (int i = 0; i < points.Count; i++)
                {
                    var pt = points[i];
                    x[i] = pt.ThetaRad;
                    y[i] = pt.R;
                }
                if (_polyOrder > 1)
                {
                    return MathNet.Numerics.Fit.PolynomialFunc(x, y, _polyOrder);

                }
                else
                {
                    return MathNet.Numerics.Fit.LineFunc(x, y);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
    
}
