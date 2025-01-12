using System.Net.Mime;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day01;

public class Day01Tests
{
    private readonly ITestOutputHelper _output;
    public Day01Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day01Solver().ExecuteExample1("??");
        
    [Fact] public void Step2WithExample() => new Day01Solver().ExecuteExample2("??");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day01Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day01Solver().ExecutePuzzle2());
}

public class Day01Solver : SolverBase
{
    List<int> data_l;
    List<int> data_r;

    protected override void Parse(List<string> data)
    {
        data_l = new List<int>();
        data_r = new List<int>();
        foreach (string pair_s in data){
            string pair_clean = Regex.Replace(pair_s, @"\s+", " ");
            string[] pair = pair_clean.Trim().Split(" ");
            data_l.Add(int.Parse(pair[0]));
            data_r.Add(int.Parse(pair[1]));
        }
        data_l.Sort();
        data_r.Sort();
    }

    protected override object Solve1()
    {
        
        
        
        int distance = 0;

        for(int i=0; i<data_l.Count; i++){
            distance += Math.Abs(data_l[i] - data_r[i]);
        }

        return distance;
    }

    protected override object Solve2()
    {
        int similarity = 0;

        foreach (int id_l in data_l){
            foreach (int id_r in data_r){
                if(id_l == id_r){
                    similarity += id_r;
                }
            }
        }
        
        return similarity;
    }
}