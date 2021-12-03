Submarine submarine = new Submarine();

List<string> instructionLines = File.ReadAllLines("input.txt").ToList();

foreach (string instructionLine in instructionLines)
{
    submarine.Go(Instruction.Parse(instructionLine));
}

Console.WriteLine(submarine.Position.Horizontal * submarine.Position.Depth);

public class Position
{
    public int Horizontal { get; set; }
    public int Depth { get; set; }
}

public class Instruction
{
    public Direction Direction { get; set; }
    public int Distance { get; set; }

    public static Instruction Parse(string s)
    {
        string[] parts = s.Split(' ');
        return new Instruction
        {
            Direction = Instruction.ParseDirection(parts[0]),
            Distance = int.Parse(parts[1])
        };
    }

    private static Direction ParseDirection(string s)
    {
        switch (s)
        {
            case "forward":
                return Direction.Forward;
            case "down":
                return Direction.Down;
            case "up":
                return Direction.Up;
            default:
                throw new Exception($"Unknown direction: {s}");
        }
    }

}

public enum Direction
{
    Up,
    Down,
    Forward
}

public class Submarine
{
    public Position Position { get; private set; }
    public Submarine()
    {
        this.Position = new Position();
    }

    public void Go(Instruction instruction)
    {
        switch (instruction.Direction)
        {
            case Direction.Forward:
                this.Position.Horizontal += instruction.Distance;
                break;
            case Direction.Down:
                this.Position.Depth += instruction.Distance;
                break;
            case Direction.Up:
                this.Position.Depth -= instruction.Distance;
                break;
        }
    }

}
