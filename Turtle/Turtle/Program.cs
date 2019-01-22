using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Turtle
{
    class Program
    {
        public static void Main(string[] args)
        {            
            GameEngine Engine = new GameEngine(new GameSettings(),new ActionList());
            Engine.RunGame();
            ExitGame();
        }

        private static void ExitGame()
        {
            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }            
        }
    }

    public class GameEngine
    {
        private GameSettings gameSettings;
        private ActionList actionList;

        public Location TurtleCurrentLocation;

        public GameEngine(GameSettings game, ActionList list)
        {
            gameSettings = game;
            actionList = list;
            TurtleCurrentLocation = game.Start;
        }

        public void RunGame()
        {
            String ActionResult = "n/a";
            Console.WriteLine("Game Info");
            Console.WriteLine(String.Format(@"Height: {0}, Width: {1}, Start: {2},{3},{4}, End: {5},{6}, Mines: {7}", gameSettings.Height, gameSettings.Width, gameSettings.Start.x.ToString(), gameSettings.Start.y.ToString(), gameSettings.Start.facing.ToString(), gameSettings.End.x.ToString(), gameSettings.End.y.ToString(), gameSettings.Mines.Count.ToString()));
            foreach (var action in actionList.Moves)
            {
                ActionResult = ProcessAction(action);
                Console.WriteLine(ActionResult + "(x=" + TurtleCurrentLocation.x.ToString() + ", y=" + TurtleCurrentLocation.y.ToString() + ", facing=" + TurtleCurrentLocation.facing.ToString() + ")");
                if (ActionResult != "Still in Danger!") break;
            }
            Console.WriteLine("Final Status: " + ActionResult);

        }

        private string ProcessAction(IAction action)
        {
            TurtleCurrentLocation = action.DoMove(TurtleCurrentLocation);
            if (TurtleCurrentLocation.x >= gameSettings.Width || TurtleCurrentLocation.x < 0) return "out of bounds!";
            if (TurtleCurrentLocation.y > gameSettings.Height || TurtleCurrentLocation.y < 0) return "out of bounds!";
            if (CompareLocations(TurtleCurrentLocation, gameSettings.End)) return "Success! :)";
            if (CheckForMineHit()) return "Mine Hit! :( ";
            else return "Still in Danger!";
        }
        private bool CheckForMineHit()
        {
            foreach(var Mine in gameSettings.Mines)
            {
                if (CompareLocations(Mine, TurtleCurrentLocation)) return true;
            }
            return false;
        }

        private bool CompareLocations(Location loc1, Location loc2)
        {
            return (loc1.x == loc2.x && loc1.y == loc2.y);
        }
    }

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
                var xml = XDocument.Load(@"C:\Game\GameConfig.xml");
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
            catch(Exception e)
            {

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

    public class ActionList
    {
        public List<IAction> Moves;

        public ActionList()
        {
            Moves = new List<IAction>();
            string[] moveStrings = System.IO.File.ReadAllText(@"C:\Game\MoveList.txt").Split(',');
            foreach(var moveString in moveStrings)
            {
                if (moveString.ToLower() == "m")
                {
                    Moves.Add(new Move());
                }
                if (moveString.ToLower() == "r")
                {
                    Moves.Add(new Rotate());
                }
            }
        }
    }

    public interface IAction
    {
        Location DoMove(Location currloc);
    }

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

    public class Rotate : IAction
    {
        public Location DoMove(Location currloc)
        {
            if (currloc.facing == Facing.North) currloc.facing = Facing.East;
            else if (currloc.facing == Facing.East) currloc.facing = Facing.South;
            else if(currloc.facing == Facing.South) currloc.facing = Facing.West;
            else if(currloc.facing == Facing.West) currloc.facing = Facing.North;
            return currloc;
        }
    }

    public class Location
    {
        public int x;
        public int y;
        public Facing facing;
    }

    public enum Facing
    {
        North,East,South,West
    }
}
