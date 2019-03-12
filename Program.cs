using System;

namespace numberSearchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a number search generator!");

            GameGrid gameGrid = new GameGrid(5, 5);
            gameGrid.GenerateGrid();

            Console.WriteLine(gameGrid);
        }
  }
}
