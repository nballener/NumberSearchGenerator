using System;

namespace numberSearchGenerator
{
    public class Game
    {
        private GameGrid gameGrid;
        public GameGrid GameGrid { get => gameGrid; set => gameGrid = value; }

        private int width;
        private int height;

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.GameGrid = new GameGrid(width, height);
        }

  }
}