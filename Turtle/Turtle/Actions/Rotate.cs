using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle.Actions
{
    public class Rotate : IAction
    {
        public Location DoMove(Location currloc)
        {
            if (currloc.facing == Facing.North) currloc.facing = Facing.East;
            else if (currloc.facing == Facing.East) currloc.facing = Facing.South;
            else if (currloc.facing == Facing.South) currloc.facing = Facing.West;
            else if (currloc.facing == Facing.West) currloc.facing = Facing.North;
            return currloc;
        }
    }
}
