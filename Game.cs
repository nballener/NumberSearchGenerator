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
            List<Clue> clues = new List<Clue>();

            List<Clue> eastClues = ExtractEastClues();
            clues.AddRange(eastClues);

            List<Clue> westClues = ExtractOppositeClues(eastClues);
            clues.AddRange(westClues);

            List<Clue> southClues = ExtractSouthClues();
            clues.AddRange(southClues);

            List<Clue> northClues = ExtractOppositeClues(southClues);
            clues.AddRange(northClues);

            List<Clue> southEastClues = ExtractSouthEastClues();
            clues.AddRange(southEastClues);

            List<Clue> northWestClues = ExtractOppositeClues(southEastClues);
            clues.AddRange(northWestClues);
            
            List<Clue> northEastClues = ExtractNorthEastClues();
            clues.AddRange(northEastClues);

            List<Clue> southWestClues = ExtractOppositeClues(northEastClues);
            clues.AddRange(southWestClues);
            
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
                case Direction.East:
                case Direction.SouthEast:
                    return x + 1;
                case Direction.West:
                    return x - 1;
            }

            return x;
        }

        private int IncrementYByDirection(int y, Direction direction)
        {
            switch(direction)
            {
                case Direction.South:
                case Direction.SouthEast:
                    return y + 1;
                case Direction.North:
                case Direction.NorthEast:
                    return y - 1;
            }
            
            return y;
        }

        private List<Clue> ExtractEastClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.East;

            int rowCount = 0;
            foreach (List<int> row in this.gameGrid.Grid)
            {
                clues.AddRange(ExtractCluesFromList(row, direction, 0, rowCount));
                rowCount++;
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

                clues.AddRange(ExtractCluesFromList(col, direction, j, 0));
            }

            return clues;
        }

        private List<Clue> ExtractSouthEastClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.SouthEast;

            int minLength = 3;
            int startX = 0;
            
            for (int startY = gameGrid.Grid.Count - minLength; startY >= 0; startY--)
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

            for (int currentStartX = 1; currentStartX <= gameGrid.Grid[0].Count - minLength; currentStartX++)
            {
                int x = currentStartX;
                int y = 0;
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

                clues.AddRange(ExtractCluesFromList(row, direction, currentStartX, 0));
            }

            return clues;
        }

        private List<Clue> ExtractNorthEastClues()
        {
            List<Clue> clues = new List<Clue>();
            Direction direction = Direction.NorthEast;

            int minLength = 3;
            int startX = 0;
            
            for (int startY = minLength - 1; startY < gameGrid.Grid.Count; startY++)
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
                    y--;

                    if (y < 0)
                        break;
                    if (x == gameGrid.Grid[0].Count)
                        break;
                }

                clues.AddRange(ExtractCluesFromList(row, direction, startX, startY));
            }

            for (int currentStartX = 1; currentStartX <= gameGrid.Grid[0].Count - minLength; currentStartX++)
            {
                int x = currentStartX;
                int y = gameGrid.Grid.Count - 1;
                int currentLength = 0;

                List<int> row = new List<int>();
                while (true)
                {
                    row.Add(gameGrid.Grid[y][x]);
                    currentLength++;
                    x++;
                    y--;

                    if (y < 0)
                        break;
                    if (x == gameGrid.Grid[0].Count)
                        break;
                }

                clues.AddRange(ExtractCluesFromList(row, direction, currentStartX, gameGrid.Grid.Count - 1));
            }

            return clues;
        }

        private List<Clue> ExtractOppositeClues(List<Clue> targetClues)
        {
            List<Clue> clues = new List<Clue>();

            targetClues.ForEach(targetClue => {
                List<int> clueCharacters = new List<int>();
                clueCharacters.AddRange(targetClue.Characters);
                clueCharacters.Reverse();

                Direction direction = GetOppositeDirection(targetClue.Direction);
                int x = GetOppositeX(targetClue);
                int y = GetOppositeY(targetClue);

                Clue clue = new Clue(Guid.NewGuid(), clueCharacters, direction, x, y);
                targetClue.Opposite = clue;
                clue.Opposite = targetClue;
                clues.Add(clue);
            });

            return clues;
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.North: return Direction.South;
                case Direction.South: return Direction.North;
                case Direction.East: return Direction.West;
                case Direction.West: return Direction.East;
                case Direction.NorthEast: return Direction.SouthWest;
                case Direction.NorthWest: return Direction.SouthEast;
                case Direction.SouthEast: return Direction.NorthWest;
                default: return Direction.NorthEast;
            }
        }

        private int GetOppositeX(Clue clue)
        {
            int length = clue.Length;
            int x = clue.X;
            int y = clue.Y;
            Direction direction = clue.Direction;

            switch(direction)
            {
                case Direction.East:
                case Direction.NorthEast:
                case Direction.SouthEast: return x + length - 1;
                default: return x;
            }
        }

        private int GetOppositeY(Clue clue)
        {
            int length = clue.Length;
            int x = clue.X;
            int y = clue.Y;
            Direction direction = clue.Direction;

            switch(direction)
            {
                case Direction.East: return y;
                case Direction.SouthEast: return y + length - 1;
                case Direction.NorthEast: return y - (length - 1);
                default: return y + length - 1;
            }
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