using System;

namespace numberSearchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a number search generator!");

            Game game = new Game(5, 5);
            GameGrid gameGrid = game.GameGrid;
            gameGrid.GenerateGrid();

            Console.WriteLine(gameGrid);
        }
  }
}
