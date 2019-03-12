using System;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class GameGrid
    {
        private int width;
        private int height;
        private List<int []> rows;
        private Random random;

        public GameGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.rows = new List<int []>();
            this.random = new Random();
        }

        public void GenerateGrid()
        {
            for (int y = 0; y < this.height; y++)
            {
                Console.Write(this.random.Next());
            }
        }
    }
}