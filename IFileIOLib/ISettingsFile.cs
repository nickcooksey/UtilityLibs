using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFileIOLib
{
    public interface ISettingsFile<T>
    {
        T Open(string filename);
        void Save(T inspParams, string filename);
    }
}
