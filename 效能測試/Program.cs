using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CoreRun;

namespace 效能測試 {
  internal class Program {
    static void Main(string[] args) {
      BenchmarkRunner.Run<Benchmarks>();
      return;

      string[] names = {
        "Student1",
        "Student2",
        "Student3",
        "Student4",
        "Student5",
        "Student6",
      };

      Benchmarks benchmarks = new Benchmarks();

      var a = benchmarks.ArraySlice(names);
      Print(a);

      Span<string> b = benchmarks.SpanSlice(names);
      Print(b);

      ArraySegment<string> c = benchmarks.ArraySegmentSlice(names);
      Print(c);

      Console.ReadLine();
    }

    public static void Print(Span<string> items) {
      foreach (var i in items) {
        Console.WriteLine(i);
      }
      Console.WriteLine();
    }
  }

  [MemoryDiagnoser]
  public class Benchmarks {
    public static IEnumerable<object[]> TestData() {
      yield return new object[] { new string[] {
        "Student1",
        "Student2",
        "Student3",
        "Student4",
        "Student5",
        "Student6",
      } };    
    }


    

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public string[] ArraySlice(string[] names)
      => names.Skip(2).Take(2).ToArray();

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public ArraySegment<string> ArraySegmentSlice(string[] names)
      => new ArraySegment<string>(names, 2, 2);

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public Span<string> SpanSlice(string[] names) => names.AsSpan().Slice(2, 2);
  }
}