using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Library.Extensions;
using System.Text;

namespace BenchmarkTest.ToTest {
  [MemoryDiagnoser]
  //[SimpleJob(RuntimeMoniker.Net48)]
  [SimpleJob(RuntimeMoniker.Net60)]
  public class MyTest : TestBase {
    public static IEnumerable<List<string>> GetParameters() {
      yield return new List<string> { "Student1", "Student2", "Student3", "Student4", "Student5", "Student6", "Student7" };
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetParameters))]
    public string StringJoin(List<string> list) => $"'{string.Join("','", list)}'";

    [Benchmark]
    [ArgumentsSource(nameof(GetParameters))]
    public string StringBuild(List<string> list) {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < list.Count; i++) {
        sb.Append('\'').Append(list[i]).Append('\'');
        if (i < list.Count - 1)
          sb.Append(',');
      }

      return sb.ToString();
    }

    public override void Run() {
      var data = GetParameters();
      foreach (var paramSet in data) {
        Console.WriteLine("輸入值 = ");
        paramSet.Dump();

        Print(nameof(StringJoin), StringJoin(paramSet));
        Print(nameof(StringBuild), StringBuild(paramSet));
        Console.WriteLine(new string('*', 100));
      }
    }

    public static void Print(string methodName, string result) {
      Console.WriteLine($"方法 {methodName,-30} =>");
      Console.WriteLine(result);
      Console.WriteLine();
    }
  }
}
