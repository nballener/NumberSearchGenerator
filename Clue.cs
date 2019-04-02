using System;
using System.Collections.Generic;

namespace numberSearchGenerator
{
    public class Clue
    {
        private Guid id;
        private int length;
        private List<int> characters;
        private Direction direction;
        private Clue opposite;
        private List<Clue> subClues;
        private int x;
        private int y;

        public Clue(Guid id, List<int> characters, Direction direction, int x, int y)
        {
            this.id = id;
            this.characters = characters;
            this.length = characters.Count;
            this.direction = direction;
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            string value = CharacterString();

            return $"({x}, {y}): ".PadRight(10) + $"{direction.ToString().PadLeft(10)} [{value.PadLeft(30, '-')}] L: {length}";
        }

        public string CharacterString()
        {
            string value = "";
            characters.ForEach(c => value += c);
            return value;
        }

        public int Length { get => length; set => length = value; }
        public List<int> Characters { get => characters; set => characters = value; }
        public Direction Direction { get => direction; set => direction = value; }
        public Clue Opposite { get => opposite; set => opposite = value; }
        public Guid ID { get => id; set => id = value; }
        public List<Clue> SubClues { get => subClues; set => subClues = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}