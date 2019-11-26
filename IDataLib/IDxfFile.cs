using IGeometryLib;
using System;
using System.Collections.Generic;
namespace IDataLib
{
    public interface IDxfSection
    {
        List<string> AsString();
    }

    public interface IDxfFile
    {
        string Filename { get; }
        List<IDwgEntity> Entities { get; }
        IDisplayData AsDisplayData(double segmentLength);
        List<IVector3> AsPointList(double segmentLength);
        void Save(string fileName, IProgress<int> progress);
        void Save(string fileName);
        void BuildFromFile(string fileName);
    }
}
