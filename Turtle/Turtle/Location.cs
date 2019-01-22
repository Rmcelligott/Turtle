using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle
{
    public class Location
    {
        public int x;
        public int y;
        public Facing facing;
    }

    public enum Facing
    {
        North, East, South, West
    }
}
