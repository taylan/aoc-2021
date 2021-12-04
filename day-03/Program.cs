
List<string> lines = File.ReadAllLines("input.txt").ToList();

List<Dictionary<string, int>> bitCounts = new();
for (int i = 0; i < lines.First().Length; i++)
{
    bitCounts.Add(new Dictionary<string, int>
    {
        {"0", 0},
        {"1", 0}
    });
}

foreach (string line in lines)
{
    for (int i = 0; i < line.Length; i++)
    {
        bitCounts[i][line[i].ToString()]++;
    }
}

string gammaRateString = "";
string epsilonRateString = "";

foreach (Dictionary<string,int> bitCount in bitCounts)
{
    int numZeros = bitCount["0"];
    int numOnes = bitCount["1"];

    if (numZeros >= numOnes)
    {
        gammaRateString += "0";
        epsilonRateString += "1";
    }
    else
    {
        gammaRateString += "1";
        epsilonRateString += "0";
    }
}

Console.WriteLine($"g: {gammaRateString}, e: {epsilonRateString}");

int gammaValue = Convert.ToInt32(gammaRateString, 2);
int epsilonValue = Convert.ToInt32(epsilonRateString, 2);

Console.WriteLine($"g: {gammaValue}, e: {epsilonValue}, consumption: {gammaValue * epsilonValue}");
