using System.Text.RegularExpressions;

namespace AdventOfCode.Day03;

public class Day03Tests
{
    private readonly ITestOutputHelper _output;
    public Day03Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day03Solver().ExecuteExample1("??");
        
    [Fact] public void Step2WithExample() => new Day03Solver().ExecuteExample2("??");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day03Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day03Solver().ExecutePuzzle2());
}

public class Day03Solver : SolverBase
{
    private String memory;

    protected override void Parse(List<string> data)
    {
        memory = new("");
        foreach (var line in data)
            memory += line;
    }

    protected override object Solve1()
    {

        int result = 0;
        
        string mul_regex_s = @"mul\(\d{1,3},\d{1,3}\)";
        Regex mul_regex = new(mul_regex_s);
        MatchCollection mul_matches = mul_regex.Matches(memory);
        foreach (Match mul in mul_matches) {
            string nums_regex_s = @"\d{1,3}";
            Regex nums_regex = new(nums_regex_s);
            MatchCollection nums_match = nums_regex.Matches(mul.Value);
            int num_1 = int.Parse(nums_match[0].Value);
            int num_2 = int.Parse(nums_match[1].Value);
            
            result += num_1 * num_2;
        }
        return result;
    }

    protected override object Solve2()
    {
        int result = 0;
        
        string regex_s = @"don't\(\)|do\(\)|mul\(\d{1,3},\d{1,3}\)";
        Regex regex = new(regex_s);
        
        bool enabled = true;
        
        MatchCollection matches = regex.Matches(memory);
        
        foreach (Match match in matches) {

            switch (match.Value) {
                case "do()":
                    enabled = true;
                    break;
                case "don't()":
                    enabled = false;
                    break;
                default:
                    if (enabled) {
                        string nums_regex_s = @"\d{1,3}";
                        Regex nums_regex = new(nums_regex_s);
                        MatchCollection nums_match = nums_regex.Matches(match.Value);
                        int num_1 = int.Parse(nums_match[0].Value);
                        int num_2 = int.Parse(nums_match[1].Value);
                        
                        result += num_1 * num_2;
                    }
                    break;
            }
        }
        return result;
    }
}