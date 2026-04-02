using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemA.Core
{
    public interface IHasPosition
    {
        Coordinates Position { get; }
    }
}
