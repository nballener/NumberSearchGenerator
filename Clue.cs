using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class Clue
    {
        private int length;
        private List<int> characters;
        private Direction direction;

        public Clue(List<int> characters, Direction direction)
        {
            this.characters = characters;
            this.length = characters.Count;
            this.direction = direction;
        }

        public override string ToString()
        {
            string value = "";

            foreach (int character in characters)
            {
                value += character;
            }

            return direction + ": " + value;
        }

        public int Length { get => length; set => length = value; }
        public List<int> Characters { get => characters; set => characters = value; }
        public Direction Direction { get => direction; set => direction = value; }
    }
}