using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCLib
{
    public class DelayPathEntity:PathEntity
    {
        
        public double Delay { get; set; }
        public DelayPathEntity()
        {
            this.Type = ICNCLib.BlockType.DELAY;
        }
    }
}
