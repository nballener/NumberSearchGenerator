using System;

namespace numberSearchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a number search generator!");

            Game game = new Game(6, 6, 25);

            game.PrintAllClues();
            // game.PrintSelectedClues();

            Console.WriteLine();
            Console.WriteLine(game.GameGrid);
        }
  }
}
