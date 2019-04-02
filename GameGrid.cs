using System;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class GameGrid
    {
        private int width;
        private int height;
        private List<List<int>> grid;
        private Random random;

        public GameGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.Grid = new List<List<int>>();
            this.random = new Random();
        }

        public void GenerateGrid()
        {
            for (int y = 0; y < this.height; y++)
            {
                List<int> row = new List<int>();

                for (int x = 0; x < this.width; x++)
                {
                    row.Add(this.random.Next(10));
                }

                this.Grid.Add(row);
            }
        }

        public override string ToString() {
            string value = "";

            this.Grid.ForEach(row => {
                row.ForEach(cell => value += $"{cell} ");
                value += "\n\n";
            });

            return value;
        }

        public List<List<int>> Grid { get => grid; set => grid = value; }
    }
}