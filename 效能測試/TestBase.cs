using MyLibrary.Services;

namespace 效能測試 {
  public abstract class TestBase : IRun {
    public abstract void Run();
  }

  public class TestFactory : TestServicess<EnumTest> {
    public TestFactory() : base("BenchmarkTest") { }
  }
}
