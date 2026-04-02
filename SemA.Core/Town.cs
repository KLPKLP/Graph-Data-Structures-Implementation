namespace SemA.Core
{
    public class Town : IHasPosition
    {
        // zkontrolovat, jestli opravdu má být všude set (jestli se to edituje v GUI)
        
        public string Name { get; }
      
        public Coordinates Position { get; set; }
        



        public Town(string name, Coordinates position)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Jméno nesmí být prázdné.", nameof(name));
            }

            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            Name = name;
            Position = position;
        }

        public override string ToString() => $"{Name} (Position={Position})";

    }
}
