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

            foreach(int [] row in this.gameGrid.Grid)
            {
                clues.Add(new Clue(row, Direction.East));
            }

            return clues;
        }

        private List<Clue> ExtractWestClues(List<Clue> eastClues)
        {
            List<Clue> clues = new List<Clue>();

            foreach(Clue eastClue in eastClues)
            {
                int [] westClueCharacters = (int []) eastClue.Characters.Clone();
                Array.Reverse(westClueCharacters);
                Clue westClue = new Clue(westClueCharacters, Direction.West);
                clues.Add(westClue);
            }

            return clues;
        }

        public void PrintAllClues()
        {
            GameGrid gameGrid = this.GameGrid;

            foreach (Clue clue in this.AllClues)
            {
                Console.WriteLine(clue);
            }
        }

        public GameGrid GameGrid { get => gameGrid; set => gameGrid = value; }
        public List<Clue> AllClues { get => allClues; set => allClues = value; }
    }
}