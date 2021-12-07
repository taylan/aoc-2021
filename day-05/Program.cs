
using System.Text;

string filename = "test-input.txt";

List<string> lines = File.ReadLines(filename).ToList();

int mapSize = File.ReadAllText(filename).Replace(" -> ", ",").Replace("\n", ",").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

OceanFloorMap map = new OceanFloorMap(lines, mapSize);

Console.WriteLine(map.ToString());

public class OceanFloorMap
{
    private readonly OceanFloorCoordinate[,] _map;
    private readonly int _mapSize;
    
    public OceanFloorMap(List<string> lines, int mapSize)
    {
        this._mapSize = mapSize;
        this._map = new OceanFloorCoordinate[mapSize, mapSize];
        this.InitializeMap(lines);
    }

    private void InitializeMap(List<string> lines)
    {
        Console.WriteLine("Initializing map...");
        
        for (int i = 0; i < this._mapSize; i++)
        {
            for (int j = 0; j < this._mapSize; j++)
            {
                this._map[i, j] = new OceanFloorCoordinate {X = i, Y = j};
            }
        }

        foreach (string line in lines)
        {
            string[] startAndEnd = line.Split(" -> ");

            string[] startCoordinates = startAndEnd[0].Split(",");
            string[] endCoordinates = startAndEnd[1].Split(",");

            int x1 = int.Parse(startCoordinates[0]);
            int y1 = int.Parse(startCoordinates[1]);

            int x2 = int.Parse(endCoordinates[0]);
            int y2 = int.Parse(endCoordinates[1]);

            if (x1 != x2 && y1 != y2)
            {
                Console.WriteLine($"Ignoring non-straight {line}.");
            }
        }
        
        Console.WriteLine("Map initialized.");
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int i = 0; i < this._mapSize; i++)
        {
            for (int j = 0; j < this._mapSize; j++)
            {
                builder.Append(this._map[i, j]);
            }

            builder.Append("\n");
        }

        return builder.ToString();
    }
}

public class OceanFloorCoordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Number { get; set; }

    public override string ToString()
    {
        return this.Number == 0 ? "." : this.Number.ToString();
    }
}

