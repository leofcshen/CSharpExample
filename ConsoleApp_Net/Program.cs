using System.Diagnostics;
using Universe.Extensions;

namespace ConsoleApp_Net;

internal class Program {
  static void Main(/*string[] args*/) {
    try {
      var iterations = 1000000000;

      // 方法一：nameof
      var stopwatch = Stopwatch.StartNew();
      for (int i = 0; i < iterations; i++) {
        var a = "abc哦".GetLength中文算二();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      // 方法一：nameof
      stopwatch.Restart();
      for (int i = 0; i < iterations; i++) {
        var b = "abc哦".GetLength中文算二_1();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      stopwatch.Restart();
      for (int i = 0; i < iterations; i++) {
        var b = "abc哦".GetLength中文算二_2();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      //SV_Windows.Run清空資源回收筒();
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
    }
  }


}
