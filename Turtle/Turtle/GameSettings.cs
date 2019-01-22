using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Turtle
{
    public class GameSettings
    {
        public Location Start;
        public Location End;

        public List<Location> Mines;
        public int Height;
        public int Width;

        public GameSettings()
        {
            try
            {
                //var xml = XDocument.Load(@"C:\Game\GameConfig.xml");
                var xml = XDocument.Load(@"GameConfig.xml");
                Height = int.Parse(xml.Root.Attribute("height").Value);
                Width = int.Parse(xml.Root.Attribute("width").Value);
                Start = BuildLocationFromElement(xml.Root.Element("start"));
                End = BuildLocationFromElement(xml.Root.Element("end"));
                Mines = new List<Location>();
                foreach (var mine in xml.Root.Elements("mine"))
                {
                    Mines.Add(BuildLocationFromElement(mine));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Parsing Game Settings File");
                Console.WriteLine(e.Message);
                Program.ExitGame();
            }

        }

        private Location BuildLocationFromElement(XElement element)
        {
            Location loc = new Location();
            loc.x = int.Parse(element.Attribute("x").Value);
            loc.y = int.Parse(element.Attribute("y").Value);
            var facingValue = element.Attribute("facing");
            if (facingValue != null)
            {
                loc.facing = (Facing)Enum.Parse(typeof(Facing), element.Attribute("facing").Value);
            }
            return loc;
        }
    }
}
