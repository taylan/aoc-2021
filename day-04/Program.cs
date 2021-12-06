using System.Text;

List<string> lines = File.ReadAllLines("input.txt").ToList();

BingoGame game = new(lines);


game.PlayGame();

if (game.HasWinner)
    Console.WriteLine(game.GetFinalScore());

// Console.WriteLine(game);

public class BingoGame
{
    private readonly List<int> _numbers;
    private readonly List<BingoBoard> _boards = new();
    
    private int _lastPlayedNumber = -1;
    private BingoBoard _winningBoard = null!;

    public bool HasWinner => this._winningBoard != null;

    public BingoGame(List<string> lines)
    {
        this._numbers = lines.First().Split(',').Select(int.Parse).ToList();
        
        lines.RemoveRange(0, 2);
        int numBoards = lines.Count(s => s == "") + 1;
        for (int i = 0; i < numBoards; i++)
        {
            BingoBoard board = new(i + 1);
            board.Parse(lines.Skip(i * 6).Take(5).ToList());
            this._boards.Add(board);
        }
    }

    public int GetFinalScore()
    {
        return this._winningBoard.GetSumOfAllUnmarkedNumbers() * this._lastPlayedNumber;
    }

    public void PlayGame()
    {
        foreach (int number in this._numbers)
        {
            foreach (BingoBoard board in this._boards)
            {
                board.ProcessNumber(number);

                this._lastPlayedNumber = number;

                if (board.HasBingo)
                {
                    this._winningBoard = board;
                    break;
                }
            }

            if (this._winningBoard != null)
                break;
        }
        
        if (this._winningBoard != null)
        {
            Console.WriteLine($"WINNING BOARD:");
            Console.WriteLine(this._winningBoard);
        }
        else
        {
            Console.WriteLine("NO BINGO FOR YOU");
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
    public bool Marked { get; private set; }

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

    private bool _hasRowBingo = false;
    private bool _hasColumnBingo = false;
    private int _bingoRowIndex = -1;
    private int _bingoColumnIndex = -1;

    public BingoBoard(int id)
    {
        this.ID = id;
    }

    public int ID { get; private set; }

    private void SetCell(int i, int j, int value)
    {
        this._board[i, j] = new BingoCell {Number = value};
        this.CellIndex[value] = new Tuple<int, int>(i, j);
    }

    public bool HasBingo => this._hasRowBingo || this._hasColumnBingo;

    public void ProcessNumber(int number)
    {
        if (!this.CellIndex.HasValue(number))
            return;
        
        (int i, int j) = this.CellIndex[number];
        this._board[i, j].Mark();
        
        this.UpdateHasBingo(Tuple.Create(i, j));
    }

    private void UpdateHasBingo(Tuple<int, int> lastUpdatedCell)
    {
        bool hasRowBingo = true;
        for (int j = 0; j < BingoBoardSize; j++)
        {
            hasRowBingo = hasRowBingo && this._board[lastUpdatedCell.Item1, j].Marked;
        }
        
        if (hasRowBingo)
        {
            this._hasRowBingo = true;
            this._bingoRowIndex = lastUpdatedCell.Item1;
            Console.WriteLine($"Board {this.ID} has bingo on row {this._bingoRowIndex}");
            return;
        }

        bool hasColumnBingo = true;
        for (int i = 0; i < BingoBoardSize; i++)
        {
            hasColumnBingo = hasColumnBingo && this._board[i, lastUpdatedCell.Item2].Marked;
        }

        if (hasColumnBingo)
        {
            this._hasColumnBingo = true;
            this._bingoColumnIndex = lastUpdatedCell.Item2;
            Console.WriteLine($"Board has bingo on column {this._bingoColumnIndex}");
        }
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

    public int GetSumOfAllUnmarkedNumbers()
    {
        int sum = 0;
        
        for (int i = 0; i < BingoBoardSize; i++)
        {
            for (int j = 0; j < BingoBoardSize; j++)
            {
                sum += !this._board[i, j].Marked ? this._board[i, j].Number : 0;
            }
        }

        return sum;
    }
}

public static class DictionaryExtensions
{
    public static bool HasValue<TK, TV>(this Dictionary<TK, TV> dict, TK key)
    {
        return dict.TryGetValue(key, out TV _);
    }
}