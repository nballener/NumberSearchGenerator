using System;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class Game
    {
        private GameGrid gameGrid;
        private int width;
        private int height;
        private List<Clue> allClues;

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameGrid = new GameGrid(width, height);
            this.gameGrid.GenerateGrid();
            this.allClues = ExtractClues();
        }

        private List<Clue> ExtractClues()
        {
            List<Clue> eastClues = ExtractEastClues();
            List<Clue> westClues = ExtractWestClues(eastClues);

            List<Clue> clues = new List<Clue>();
            clues.AddRange(eastClues);
            clues.AddRange(westClues);

            return clues;
        }

        private List<Clue> ExtractEastClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.East;

            foreach (List<int> row in this.gameGrid.Grid)
            {
                int length = row.Count;
                for (int i = 3; i < length; i++)
                {
                    Clue clue = new Clue(row.GetRange(0, i), direction);
                    clues.Add(clue);
                }

                for (int i = 0; i < length - 2; i++)
                {
                    Clue clue = new Clue(row.GetRange(i, length - i), direction);
                    clues.Add(clue);
                }

                clues.Add(new Clue(row, direction));
            }

            return clues;
        }

        private List<Clue> ExtractWestClues(List<Clue> eastClues)
        {
            List<Clue> clues = new List<Clue>();

            foreach (Clue eastClue in eastClues)
            {
                List<int> westClueCharacters = new List<int>();
                westClueCharacters.AddRange(eastClue.Characters);
                westClueCharacters.Reverse();
                Clue westClue = new Clue(westClueCharacters, Direction.West);
                clues.Add(westClue);
            }

            return clues;
        }

        public void PrintAllClues()
        {
            GameGrid gameGrid = this.GameGrid;
            this.AllClues.ForEach(clue => Console.WriteLine(clue));
        }

        public GameGrid GameGrid { get => gameGrid; set => gameGrid = value; }
        public List<Clue> AllClues { get => allClues; set => allClues = value; }
    }
}