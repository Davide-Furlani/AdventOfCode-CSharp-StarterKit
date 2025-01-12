namespace AdventOfCode.Day02;

public class Day02Tests
{
    private readonly ITestOutputHelper _output;
    public Day02Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day02Solver().ExecuteExample1("??");
        
    [Fact] public void Step2WithExample() => new Day02Solver().ExecuteExample2("??");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day02Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day02Solver().ExecutePuzzle2());
}

public class Day02Solver : SolverBase
{
    List<List<int>> reports;

    protected override void Parse(List<string> data)
    {
        this.reports = new();
        
        foreach (string report_s in data) {
            string[] report_l = report_s.Split(" ");
            List<int> report = new List<int>();
            
            foreach (var value_s in report_l){
                report.Add(int.Parse(value_s));
            }
            this.reports.Add(report);
        }
    }

    protected override object Solve1() {
        int safe_reports = 0;
        
        foreach (var report in reports) {
            if(report.Count < 2)
                continue;

            bool safe = is_safe_report(report);
            
            safe_reports += safe ? 1 : 0;
        }
        return safe_reports;
    }

    protected override object Solve2()
    {
        int safe_reports = 0;
        
        foreach (var report in reports) {
            if(report.Count < 2)
                continue;
            
            bool safe = is_safe_report(report);
            
            if (!safe) {
                for (int i = 0; i < report.Count; i++) {
                    List<int> new_report = new(report);
                    new_report.RemoveAt(i);
                    if (is_safe_report(new_report)) {
                        safe = true;
                        break;
                    }
                }
            }

            safe_reports += safe ? 1 : 0;
        }
        return safe_reports;
    }

    bool is_safe_report(List<int> report) {
        int difference = report[1] - report[0];
        bool decreasing = difference < 0;

        bool safe = true;
        for (int i = 0; i < report.Count - 1; i++) {
            difference = report[i + 1] - report[i];
            if (Math.Abs(difference) == 0 || Math.Abs(difference) > 3) {
                safe = false;
                break;
            }

            if ((difference < 0 && !decreasing) || (difference > 0 && decreasing)) {
                safe = false;
                break;
            }
        }
        return safe;
    }
}