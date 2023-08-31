#if RELEASE
using BenchmarkDotNet.Running;
using System.Reflection;
#endif

namespace 效能測試 {
  public class FastStringCreation {
    // Your benchmarks here
  }
  internal class Program {
    /// <summary> 要執行的動作 </summary>
    private static readonly EnumTest Action = EnumTest.StringArraySlice;
    //private static readonly EnumTest Action = EnumTest.StringArraySlice;
    static void Main(string[] args) {
#if RELEASE
      //BenchmarkRunner.Run<ClassT>();
      string className = Action.ToString();
      string? nameSpace = MethodBase.GetCurrentMethod()?.DeclaringType?.Namespace;
      Type? t = Type.GetType($"{nameSpace}.BenchmarkTest.{className}");
      if (t is not null) {
        BenchmarkRunner.Run(t);
      }
#endif

#if DEBUG
      new TestFactory().Services[Action]().Run();
      Console.ReadLine();
#endif
    }
  }
}