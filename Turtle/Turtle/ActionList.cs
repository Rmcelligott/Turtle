using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turtle.Actions;

namespace Turtle
{
    public class ActionList
    {
        public List<IAction> Moves;

        public ActionList()
        {
            try
            {
                Moves = new List<IAction>();
                //string[] moveStrings = System.IO.File.ReadAllText(@"C:\Game\MoveList.txt").Split(',');
                string[] moveStrings = System.IO.File.ReadAllText(@"MoveList.txt").Split(',');
                foreach (var moveString in moveStrings)
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
            catch (Exception e)
            {
                Console.WriteLine("Error Parsing Move List File");
                Console.WriteLine(e.Message);
                Program.ExitGame();
            }
        }
    }
}
