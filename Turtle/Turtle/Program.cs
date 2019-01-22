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
        //Rough and ready solution to turtle problem
        //Not a lot of error handling or validation going on as I didn't want to spend more than an hour and a half on it, hence the single file classes etc
        //
        //the settings file and movelist are loaded from the bin folder, Didn't get around to handling them as input arguments
        //
        //Would like to spend time refactoring but didn't spend too long planning it out, just coded it as it came into my head.

        public static void Main(string[] args)
        {            
            GameEngine Engine = new GameEngine(); //initialise the game engine by loading the game config (start, endm dimensions etc) and the move list.
            Engine.RunGame(); 
            ExitGame();
        }

        public static void ExitGame()
        {
            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Environment.Exit(0);
        }
    }
}
