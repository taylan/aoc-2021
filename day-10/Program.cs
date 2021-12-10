
List<string> lines = File.ReadAllLines("input.txt").ToList();

Dictionary<char, char> parenthesisMap = new()
{
    {'(', ')'},
    {'[', ']'},
    {'<', '>'},
    {'{', '}'}
};

Dictionary<char, int> illegalCharScores = new()
{
    {')', 3},
    {']', 57},
    {'}', 1197},
    {'>', 25137}
};

List<char> openingChars = parenthesisMap.Keys.ToList();
List<char> closingChars = parenthesisMap.Values.ToList();


int totalIllegalCharScore = 0;

foreach (string line in lines)
{
    // Console.WriteLine($"Doing line {line}");
    Stack<char> chars = new();
    for (int i = 0; i < line.Length; i++)
    {
        char c = line[i];
    
        if(openingChars.Contains(c))
            chars.Push(c);
        else if (closingChars.Contains(c))
        {
            char topChar = chars.Peek();

            bool gotRequiredClosingChar = parenthesisMap.TryGetValue(topChar, out char requiredClosingChar);

            if (!gotRequiredClosingChar || c != requiredClosingChar)
            {
                Console.WriteLine($"Corrupt line. Char # {i}. Current char: {c}, prev char: {topChar}");
                totalIllegalCharScore += illegalCharScores[c];
                break;
            }
            else
            {
                chars.Pop();
            }
        }
    }
    
    // Console.WriteLine("---");
}
Console.WriteLine($"Total score: {totalIllegalCharScore}");