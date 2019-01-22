using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turtle.Actions;

namespace Turtle
{
    public class GameEngine
    {
        private GameSettings gameSettings;
        private ActionList actionList;

        public Location TurtleCurrentLocation;

        public GameEngine()
        {
            gameSettings = new GameSettings();
            actionList = new ActionList();
            TurtleCurrentLocation = gameSettings.Start;
        }

        public void RunGame()
        {
            String ActionResult = "n/a";
            Console.WriteLine("Game Info");
            Console.WriteLine(String.Format(@"Height: {0}, Width: {1}, Start: {2},{3},{4}, End: {5},{6}, Mines: {7}", gameSettings.Height, gameSettings.Width, gameSettings.Start.x.ToString(), gameSettings.Start.y.ToString(), gameSettings.Start.facing.ToString(), gameSettings.End.x.ToString(), gameSettings.End.y.ToString(), gameSettings.Mines.Count.ToString()));

            //loop through the move list and apply the actions to the TurtlesCurrentLocation, then check if it hit anything etc (Process Action)
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
            if (TurtleCurrentLocation.y >= gameSettings.Height || TurtleCurrentLocation.y < 0) return "out of bounds!";
            if (CompareLocations(TurtleCurrentLocation, gameSettings.End)) return "Success! :)";
            if (CheckForMineHit()) return "Mine Hit! :( ";
            else return "Still in Danger!";
        }
        private bool CheckForMineHit()
        {
            foreach (var Mine in gameSettings.Mines)
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
}
