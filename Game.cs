using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class Game
    {
        private GameGrid gameGrid;
        private int width;
        private int height;
        private List<Clue> allClues;
        private List<Clue> selectedClues;

        public Game(int width, int height, int maxClues)
        {
            this.width = width;
            this.height = height;
            this.gameGrid = new GameGrid(width, height);
            this.gameGrid.GenerateGrid();
            this.allClues = ExtractClues();
            this.selectedClues = SelectClues(max: maxClues);
        }

        private List<Clue> ExtractClues()
        {
            // List<Clue> eastClues = ExtractEastClues();
            // List<Clue> westClues = ExtractWestClues(eastClues);
            // List<Clue> southClues = ExtractSouthClues();
            // List<Clue> northClues = ExtractNorthClues(southClues);

            List<Clue> southEastClues = ExtractSouthEastClue();

            List<Clue> clues = new List<Clue>();

            // clues.AddRange(eastClues);
            // clues.AddRange(westClues);
            // clues.AddRange(southClues);
            // clues.AddRange(northClues);
            clues.AddRange(southEastClues);

            return clues;
        }

        private List<Clue> SelectClues(int max)
        {
            List<Clue> clues = new List<Clue>();

            int allCluesCount = this.allClues.Count;
            int currentClueCount = 0;
            int tries = 0;
            Random rand = new Random();

            while (currentClueCount < max && tries < 1000)
            {
                tries++;
                Clue clue = this.allClues[rand.Next(allCluesCount)];
                if (clues.Find(_clue => _clue == clue || _clue.Opposite == clue) == null)
                {
                    Clue matching = clues.Find(_clue => {
                        bool matching_check = Regex.IsMatch(_clue.CharacterString(), clue.CharacterString());
                        bool matching_check_reverse = Regex.IsMatch(clue.CharacterString(), _clue.CharacterString());
                        bool matching_direction = _clue.Direction == clue.Direction;
                        return (matching_check || matching_check_reverse) && matching_direction;
                    });

                    if (matching == null)
                    {
                        clues.Add(clue);
                        currentClueCount++;
                        tries = 0;
                    }
                }
            }

            return clues;
        }

        private List<Clue> ExtractCluesFromList(List<int> row, Direction direction, int x, int y)
        {
            List<Clue> clues = new List<Clue>();

            int _x = x;
            int _y = y;
            int length = row.Count;
            for (int i = 3; i < length; i++)
            {
                Clue clue = new Clue(Guid.NewGuid(), row.GetRange(0, i), direction, _x, _y);
                clues.Add(clue);

                _x = IncrementXByDirection(_x, direction);
                _y = IncrementYByDirection(_y, direction);
            }

            _x = x;
            _y = y;

            for (int i = 0; i < length - 2; i++)
            {
                Clue clue = new Clue(Guid.NewGuid(), row.GetRange(i, length - i), direction, _x, _y);
                clues.Add(clue);

                _x = IncrementXByDirection(_x, direction);
                _y = IncrementYByDirection(_y, direction);
            }


            return clues;
        }

        private int IncrementXByDirection(int x, Direction direction)
        {
            switch(direction)
                {
                    case Direction.SouthEast:
                        return x + 1;
                }

            return x;
        }

        private int IncrementYByDirection(int y, Direction direction)
        {
            switch(direction)
                {
                    case Direction.SouthEast:
                        return y + 1;
                }
            
            return y;
        }

        private List<Clue> ExtractEastClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.East;

            foreach (List<int> row in this.gameGrid.Grid)
            {
                clues.AddRange(ExtractCluesFromList(row, direction, 0, 0));
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
                Clue westClue = new Clue(Guid.NewGuid(), westClueCharacters, Direction.West, 0, 0);
                eastClue.Opposite = westClue;
                westClue.Opposite = eastClue;
                clues.Add(westClue);
            }

            return clues;
        }

        private List<Clue> ExtractSouthClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.South;

            int rowLength = gameGrid.Grid[0].Count;

            for (int j = 0; j < rowLength; j++)
            {
                List<int> col = new List<int>();
                gameGrid.Grid.ForEach(row => col.Add(row[j]));

                clues.AddRange(ExtractCluesFromList(col, direction, 0, 0));
            }

            return clues;
        }

        private List<Clue> ExtractNorthClues(List<Clue> southClues)
        {
            List<Clue> clues = new List<Clue>();

            foreach (Clue southClue in southClues)
            {
                List<int> northClueCharacters = new List<int>();
                northClueCharacters.AddRange(southClue.Characters);
                northClueCharacters.Reverse();
                Clue northClue = new Clue(Guid.NewGuid(), northClueCharacters, Direction.North, 0, 0);
                southClue.Opposite = northClue;
                northClue.Opposite = southClue;
                clues.Add(northClue);
            }

            return clues;
        }

        public List<Clue> ExtractSouthEastClue()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.SouthEast;

            int minLength = 3;
            int startX = 0;
            
            for (int startY = gameGrid.Grid[0].Count - minLength; startY >= 0; startY--)
            {
                int x = startX;
                int y = startY;
                int currentLength = 0;

                List<int> row = new List<int>();
                while (true)
                {
                    row.Add(gameGrid.Grid[y][x]);
                    currentLength++;
                    x++;
                    y++;

                    if (y == gameGrid.Grid.Count)
                        break;
                    if (x == gameGrid.Grid[0].Count)
                        break;
                }

                clues.AddRange(ExtractCluesFromList(row, direction, startX, startY));
            }

            return clues;
        }

        public void PrintAllClues()
        {
            GameGrid gameGrid = this.GameGrid;
            this.AllClues.ForEach(clue => Console.WriteLine(clue));
        }

        public void PrintSelectedClues()
        {
            GameGrid gameGrid = this.GameGrid;

            this.selectedClues.Sort((a, b) => {
                if (a.Characters.Count < b.Characters.Count) return -1;
                else if (a.Characters.Count > b.Characters.Count) return 1;
                else return 0;
            });

            this.selectedClues.ForEach(clue => Console.WriteLine(clue));

            Console.WriteLine($"Total Clues: {this.selectedClues.Count}");
        }

        public GameGrid GameGrid { get => gameGrid; set => gameGrid = value; }
        public List<Clue> AllClues { get => allClues; set => allClues = value; }
    }
}