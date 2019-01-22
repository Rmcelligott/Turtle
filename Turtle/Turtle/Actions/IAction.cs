using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle.Actions
{
    public interface IAction
    {
        Location DoMove(Location currloc);
    }
}
