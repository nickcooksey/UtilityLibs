using System.Collections.Generic;

namespace ICNCLib
{
    public interface ICncFileParser
    {
        IToolpath CreatePath(string fileName);
         
    }
}
