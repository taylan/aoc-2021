using System.Text;

List<string> lines = File.ReadAllLines("test-input.txt").ToList();

BingoGame game = new BingoGame(lines);

Console.WriteLine(game.ToString());

game.PlayGame();

Console.WriteLine(game.ToString());


public class BingoGame
{
    private List<int> _numbers;
    private List<BingoBoard> _boards = new();

    public BingoGame(List<string> lines)
    {
        this._numbers = lines.First().Split(',').Select(int.Parse).ToList();
        
        lines.RemoveRange(0, 2);
        int numBoards = lines.Count(s => s == "") + 1;
        for (int i = 0; i < numBoards; i++)
        {
            BingoBoard board = new();
            board.Parse(lines.Skip(i * 6).Take(5).ToList());
            this._boards.Add(board);
        }
    }

    public void PlayGame()
    {
        foreach (int number in this._numbers.Take(5))
        {
            foreach (BingoBoard board in this._boards)
            {
                board.ProcessNumber(number);
            }
        }
    }
    
    public override string ToString()
    {
        string theWhole = "";
        foreach (BingoBoard board in this._boards)
        {
            theWhole += $"{board}\n\n";
        }

        return theWhole;
    }
}

public class BingoCell
{
    public int Number { get; internal init; }
    private bool Marked { get; set; }

    public void Mark()
    {
        this.Marked = true;
    }

    public override string ToString()
    {
        return this.Marked ? $"*{this.Number}*" : this.Number.ToString();
    }
}

public class BingoBoard
{
    private const int BingoBoardSize = 5;
    
    private readonly BingoCell[,] _board = new BingoCell[BingoBoardSize, BingoBoardSize];
    private Dictionary<int, Tuple<int, int>> CellIndex { get; } = new();

    private void SetCell(int i, int j, int value)
    {
        this._board[i, j] = new BingoCell {Number = value};
        this.CellIndex[value] = new Tuple<int, int>(i, j);
    }

    public bool HasBingo => false;

    public void ProcessNumber(int number)
    {
        if (!this.CellIndex.HasValue(number))
            return;
        
        (int i, int j) = this.CellIndex[number];
        this._board[i, j].Mark();
    }

    public void Parse(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            List<int> numbersInLine = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            for (int j = 0; j < numbersInLine.Count; j++)
            {
                this.SetCell(i, j, numbersInLine[j]);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < BingoBoardSize; i++)
        {
            for (int j = 0; j < BingoBoardSize; j++)
            {
                builder.Append(this._board[i, j]).Append('\t');
            }

            builder.Append(Environment.NewLine);
        }

        return builder.ToString();
    }
}

public static class DictionaryExtensions
{
    public static bool HasValue<TK, TV>(this Dictionary<TK, TV> dict, TK key)
    {
        return dict.TryGetValue(key, out TV _);
    }
}