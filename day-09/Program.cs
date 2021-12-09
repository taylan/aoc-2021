
List<string> lines = File.ReadAllLines("input.txt").ToList();

int mapWidth = lines[0].Length;
int mapHeight = lines.Count;

int[,] map = new int[mapWidth, mapHeight];

for (int j = 0; j < mapHeight; j++)
{
    for (int i = 0; i < mapWidth; i++)
    {
        map[i, j] = int.Parse(lines[j][i].ToString());
    }
}

int riskLevel = 0;

for (int i = 0; i < mapWidth; i++)
{
    for (int j = 0; j < mapHeight; j++)
    {
        int cellValue = map[i, j];
        List<int> adjacentPoints = GetAdjacentPoints(i, j, map);
        bool cellIsLowest = adjacentPoints.All(p => cellValue < p);
        if (cellIsLowest)
        {
            riskLevel += cellValue + 1;
            Console.WriteLine($"Cell {i}, {j} lowest. New risk level: {riskLevel}");
        }
    }
}

List<int> GetAdjacentPoints(int i, int j, int[,] map)
{
    List<int?> adjacentPoints = new()
    {
        map.TryGetCellValue(i - 1, j), // left
        map.TryGetCellValue(i + 1, j), // right
        map.TryGetCellValue(i, j - 1), // up
        map.TryGetCellValue(i, j + 1) // down
    };

    return adjacentPoints.Where(p => p.HasValue).Select(p => p.Value).ToList();
}

public static class MatrixExtensions
{
    public static int? TryGetCellValue(this int[,] matrix, int i, int j)
    {
        try
        {
            return matrix[i, j];
        }
        catch (IndexOutOfRangeException e)
        {
            return null;
        }
    }
}