using System;

namespace numberSearchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a number search generator!");
            int maxGames = 1;

            for (int i = 0; i < maxGames; i++)
            {
                Game game = new Game(30, 30, 30);

                // game.PrintAllClues();
                game.PrintSelectedClues();

                Console.WriteLine();
                Console.WriteLine(game.GameGrid);
            }
        }
  }
}
