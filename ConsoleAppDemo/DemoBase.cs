using MyLibrary.Services;

namespace ConsoleAppDemo {


  public class DemoFactory : TestServicess<EnumDemo> {
    public DemoFactory() : base("Demo") { }
    public abstract void Run();
  }
}