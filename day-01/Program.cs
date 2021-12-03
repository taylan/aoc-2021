
List<int> measurements = File.ReadAllLines("input.txt").Select(l => int.Parse(l)).ToList();

int numIncreasingMeasurements = 0;

for (int current = 1; current < measurements.Count; current++)
{
    int previous = current - 1;

    if (measurements[current] > measurements[previous])
    {
        numIncreasingMeasurements++;
    }
}

Console.WriteLine(numIncreasingMeasurements);
