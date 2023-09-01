using MyLibrary.Services;

namespace ConsoleAppDemo {
  internal class Program {
    /// <summary> 切換要執行的動作 </summary>
    private static readonly EnumDemo Action = EnumDemo.None;

    static void Main(string[] args) {
      new DemoBase().Services[Action]().Run();

      Console.WriteLine();
      Console.WriteLine($"{Action} 執行結束，輸入任意鍵繼續...");
      Console.ReadLine();
    }
  }

  public class DemoBase : DemoServicess<EnumDemo> {
    public DemoBase() : base("Demo") { }
    public override void Run() { }
  }
}