
List<int> numbers = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToList();

List<Lanternfish> fish = numbers.Select(n => new Lanternfish(n)).ToList();

int targetGenerations = 80;

for (int numGenerations = 1; numGenerations <= targetGenerations; numGenerations++)
{
    List<Lanternfish> newFish = new();
    foreach (Lanternfish f in fish)
    {
        TimerTickResult result = f.InternalTimerTick();
        if (result.NewFishSpawned)
            newFish.Add(new Lanternfish(8));
    }
    fish.AddRange(newFish);
    
    // Console.WriteLine($"After day {numGenerations}: {string.Join(",", fish.Select(f => f.Timer))}");
}

Console.WriteLine($"Total fish: {fish.Count}");

public class Lanternfish
{
    public Lanternfish(int timer)
    {
        this.Timer = timer;
    }

    private int Timer { get; set; }

    public TimerTickResult InternalTimerTick()
    {
        this.Timer--;

        if (this.Timer != -1)
            return new TimerTickResult();
        
        this.Timer = 6;
        return new TimerTickResult
        {
            NewFishSpawned = true
        };
    }
}

public class TimerTickResult
{
    public bool NewFishSpawned { get; init; }
}