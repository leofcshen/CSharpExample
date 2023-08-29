using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Text;

namespace 效能測試 {
  internal class Program {
    static void Main(string[] args) {
      #region -- Benchmark 測試 (反註解區塊並切到 Release 執行)--
      BenchmarkRunner.Run<Benchmarks>();
      return;
      #endregion

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

    public IEnumerable<string[]> GetNames() {
      yield return new string[] {
        "Student1",
        "Student2",
        "Student3",
        "Student4",
        "Student5",
        "Student6"
      };
    }

    static string[] names = new string[] {
      "Student1",
      "Student2",
      "Student3",
      "Student4",
      "Student5",
      "Student6"
    };

    [Benchmark]
    //[Arguments(names)]
    [ArgumentsSource(nameof(GetNames))]
    public string[] ArraySlice(string[] names) => names.Skip(2).Take(2).ToArray();

    [Benchmark]
    [ArgumentsSource(nameof(GetNames))]
    public ArraySegment<string> ArraySegmentSlice(string[] names) => new(names, 2, 2);

    [Benchmark]
    [ArgumentsSource(nameof(GetNames))]
    public Span<string> SpanSlice(string[] names) => names.AsSpan().Slice(2, 2);
  }


  [SimpleJob(RuntimeMoniker.Net48)]
  [SimpleJob(RuntimeMoniker.Net60)]
  [MemoryDiagnoser]
  public class MyClass {
    //傳入一串文字回傳屏蔽四碼後的文字 Pas*********

    /// <summary>
    /// 幼稚園方法
    /// <para>記憶體耗費 400 B</para>
    /// </summary>
    [Benchmark]
    [Arguments("Password123!")]
    public string MaskNaive(string clearValue) {
      var firstChars = clearValue.Substring(0, 3);
      var length = clearValue.Length - 3;
      for (int i = 0; i < length; i++) {
        // string 是 immutable，每次都 allocate 新的 string
        firstChars += '*';
      }
      return firstChars;
    }

    /// <summary>
    /// 使用 StringBuilder
    /// <para>記憶體耗費 184 B</para>
    /// </summary>
    [Benchmark]
    [Arguments("Password123!")]
    public string MaskStringBuilder(string clearValue) {
      var firstChars = clearValue.Substring(0, 3);
      var length = clearValue.Length - 3;
      var stringBuilder = new StringBuilder(firstChars);
      for (int i = 0; i < length; i++) {
        stringBuilder.Append('*');
      }
      //ToString() 時才分配
      return stringBuilder.ToString();
    }

    /// <summary>
    /// 使用 New String
    /// <para>記憶體耗費 120 B</para>
    /// </summary>
    [Benchmark]
    [Arguments("Password123!")]
    public string MaskNewString(string clearValue) {
      var firstChars = clearValue.Substring(0, 3);
      var length = clearValue.Length - 3;
      var asterisks = new string('*', length);
      return firstChars + asterisks;
    }

    /// <summary>
    /// 使用 span
    /// <para>記憶體耗費 48 B</para>
    /// </summary>
    [Benchmark]
    [Arguments("Password123!")]
    public string MaskStringCreate(string clearValue) {
#if NET6_0_OR_GREATER
      return string.Create(clearValue.Length, clearValue, (span, value) => {
        value.AsSpan().CopyTo(span);
        span[3..].Fill('*');
      });
#else
      return string.Empty;
#endif
    }

    /// <summary>
    /// 使用 Padding
    /// <para>記憶體耗費 80 B</para>
    /// </summary>
    [Benchmark]
    [Arguments("Password123!")]
    public string MaskPadding(string clearValue) {
      var firstChars = clearValue.Substring(0, 3);
      var length = clearValue.Length;
      //Padding 底層也是使用 span
      return firstChars.PadRight(length, '*');
    }
  }
}