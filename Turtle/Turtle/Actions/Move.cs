using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle.Actions
{
    public class Move : IAction
    {
        public Location DoMove(Location currloc)
        {
            if (currloc.facing == Facing.North) currloc.y--;
            if (currloc.facing == Facing.East) currloc.x++;
            if (currloc.facing == Facing.South) currloc.y++;
            if (currloc.facing == Facing.West) currloc.x--;
            return currloc;
        }
    }
}
