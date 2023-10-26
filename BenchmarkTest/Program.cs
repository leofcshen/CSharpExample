using Library.Services;
#if RELEASE
using BenchmarkDotNet.Running;
using System.Reflection;
#endif

namespace BenchmarkTest {
  internal class Program {
    /// <summary> 切換要執行或測試的類別 </summary>
    private static readonly EnumTest Action = EnumTest.MyTest;

    static void Main(string[] args) {
#if RELEASE
      //BenchmarkRunner.Run<ClassT>();
      string className = Action.ToString();
      string? nameSpace = MethodBase.GetCurrentMethod()?.DeclaringType?.Namespace;
      Type? t = Type.GetType($"{nameSpace}.ToTest.{className}");

      if (t is not null) BenchmarkRunner.Run(t);
#endif

#if DEBUG
      new TestBase().Services[Action]().Run();
      Console.ReadLine();
#endif
    }
  }

  public class TestBase : DemoServicess<EnumTest> {
    public TestBase() : base("ToTest") { }
    public override void Run() { }
  }
}