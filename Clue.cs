namespace numberSearchGenerator
{
    public class Clue
    {
        private int length;
        private int[] characters;
        private Direction direction;

        public Clue(int [] characters, Direction direction)
        {
            this.characters = characters;
            this.length = characters.Length;
            this.direction = direction;
        }

        public override string ToString()
        {
            string value = "";

            foreach(int character in characters)
            {
                value += character;
            }

            return value;
        }

        public int Length { get => length; set => length = value; }
        public int[] Characters { get => characters; set => characters = value; }
        public Direction Direction { get => direction; set => direction = value; }
    }
}