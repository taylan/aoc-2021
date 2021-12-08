
List<string> lines = File.ReadAllLines("input.txt").ToList();

int sum = 0;

List<int> acceptableLengths = new() {2, 4, 3, 7};

foreach (string line in lines)
{
    int count = line.Split('|')[1].Trim().Split(' ').Count(s => acceptableLengths.Contains(s.Length));

    sum += count;
}

Console.WriteLine(sum);