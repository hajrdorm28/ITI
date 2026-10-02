using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_1
{
    [Flags]
    public enum PlaybackOptions
    {
        None = 0,
        Play = 1,
        Pause = 2,
        Stop = 4,
        Next = 8,
        Previous = 16
    };
}
