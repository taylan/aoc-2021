
List<int> crabs = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToList();

int numTargetPositions = crabs.Count;

int minFuelCost = int.MaxValue;

for (int potentialTargetPosition = 0; potentialTargetPosition < numTargetPositions; potentialTargetPosition++)
{
    int fuelCost = 0;
    foreach (int crab in crabs)
    {
        fuelCost += Math.Abs(crab - potentialTargetPosition);
    }

    if (fuelCost < minFuelCost)
    {
        minFuelCost = fuelCost;
    }
}

Console.WriteLine(minFuelCost);