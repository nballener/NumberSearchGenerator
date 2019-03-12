using System;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class GameGrid
    {
        private int width;
        private int height;
        private List<int []> grid;
        private Random random;

        public GameGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.grid = new List<int []>();
            this.random = new Random();
        }

        public void GenerateGrid()
        {
            for (int y = 0; y < this.height; y++)
            {
                int [] row = new int[this.width];

                for (int x = 0; x < this.width; x++)
                {
                    row[x] = this.random.Next(10);
                }

                this.grid.Add(row);
            }
        }

        public override string ToString() {
            string value = "";

            foreach (int [] row in this.grid)
            {
                foreach (int cell in row)
                {
                    value += cell;
                }

                value += "\n";
            }

            return value;
        }
    }
}