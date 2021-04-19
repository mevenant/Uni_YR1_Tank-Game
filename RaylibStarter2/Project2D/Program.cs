using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Program
    {
        static void Main(string[] args)
        {
            string window_title = "the simplest level editor and tank game";

            InitWindow((int)Global.WINDOW_SIZE.x, (int)Global.WINDOW_SIZE.y, window_title);
            
            Game game = new Game(Game.Modes.Menu);

            game.init();

            while (!WindowShouldClose())
            {
                game.update();
                game.draw();
            }

            game.shut_down();

            CloseWindow();
        }
    }
}
