using System.Net.Mime;

namespace AdventOfCode.Day04;

public class Day04Tests
{
    private readonly ITestOutputHelper _output;
    public Day04Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day04Solver().ExecuteExample1("??");
        
    [Fact] public void Step2WithExample() => new Day04Solver().ExecuteExample2("??");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day04Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day04Solver().ExecutePuzzle2());
}

public class Day04Solver : SolverBase
{
    List<string> grid;
    
    protected override void Parse(List<string> data)
    {
        grid = new(data);
    }

    protected override object Solve1()
    {
        int word_count = 0;
        
        List<Tuple<int, int>> directions = new List<Tuple<int, int>>();
        directions.Add(new Tuple<int, int>(0, 1));
        directions.Add(new Tuple<int, int>(0, -1));
        directions.Add(new Tuple<int, int>(1, 0));
        directions.Add(new Tuple<int, int>(-1, 0));
        directions.Add(new Tuple<int, int>(1, 1));
        directions.Add(new Tuple<int, int>(-1, -1));
        directions.Add(new Tuple<int, int>(1, -1));
        directions.Add(new Tuple<int, int>(-1, 1));

        for (int i = 0; i<grid.Count; i++) {
            for (int j = 0; j < grid[i].Length; j++) {
                if (grid[i][j] == 'X') {
                    foreach (Tuple<int, int> dir in directions) {
                        string word = "";
                        word += grid[i][j];
                        try {
                            word += grid[i+dir.Item1][j+dir.Item2];
                        }catch{}
                        try{
                            word += grid[i+2*dir.Item1][j+2*dir.Item2];
                        }catch{}
                        try{
                            word += grid[i+3*dir.Item1][j+3*dir.Item2];
                        }catch{}

                        if (word == "XMAS")
                            word_count++;
                    }
                }
            }
        }
        return word_count;
    }

    protected override object Solve2() {
        int word_count = 0;

        for (int i = 0; i < grid.Count; i++) {
            for (int j = 0; j < grid[i].Length; j++) {
                if (grid[i][j] == 'A') {
                    try {
                        if((grid[i-1][j-1] == 'M' && grid[i+1][j+1] == 'S') && (grid[i+1][j-1] == 'M' && grid[i-1][j+1] == 'S'))
                            word_count++;
                        if((grid[i-1][j-1] == 'M' && grid[i+1][j+1] == 'S') && (grid[i+1][j-1] == 'S' && grid[i-1][j+1] == 'M'))
                            word_count++;
                        if((grid[i-1][j-1] == 'S' && grid[i+1][j+1] == 'M') && (grid[i+1][j-1] == 'M' && grid[i-1][j+1] == 'S'))
                            word_count++;
                        if((grid[i-1][j-1] == 'S' && grid[i+1][j+1] == 'M') && (grid[i+1][j-1] == 'S' && grid[i-1][j+1] == 'M'))
                            word_count++;
                    }
                    catch {}
                    
                }
            }
        }
        return word_count;
    }
}