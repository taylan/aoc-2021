
using System.Text;

string filename = "input.txt";

List<string> lines = File.ReadLines(filename).ToList();

int mapSize = File.ReadAllText(filename).Replace(" -> ", ",").Replace("\n", ",").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max() + 1;

OceanFloorMap map = new OceanFloorMap(lines, mapSize);

int numDangerousPoints = map.GetNumDangerousPoints();
Console.WriteLine($"Dangerous points: {numDangerousPoints}");

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
                continue;
            }

            if (x1 == x2)
            {
                int startIndex = new[] {y1, y2}.Min();
                int numPoints = Math.Abs(y1 - y2) + 1;
                if (numPoints == 2)
                    numPoints++;

                for (int i = startIndex; i < startIndex + numPoints; i++)
                {
                    this._map[i, x1].Number++;
                }
            }

            if (y1 == y2)
            {
                int startIndex = new[] {x1, x2}.Min();
                int numPoints = Math.Abs(x1 - x2) + 1;

                for (int i = startIndex; i < startIndex + numPoints; i++)
                {
                    this._map[y1, i].Number++;
                }
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

    public int GetNumDangerousPoints()
    {
        int numDangerousPoints = 0;
        for (int i = 0; i < this._mapSize; i++)
        {
            for (int j = 0; j < this._mapSize; j++)
            {
                if (this._map[i, j].Number >= 2)
                    numDangerousPoints++;
            }
        }

        return numDangerousPoints;
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

