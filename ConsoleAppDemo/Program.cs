using MyLibrary.Extensions;

namespace ConsoleAppDemo {
  internal class Program {
    /// <summary> 要執行的動作 </summary>
    private static readonly EnumDemo Action = EnumDemo.None;

    static void Main(string[] args) {
      DemoFactory.DemoServices[Action]().Run();

      Console.WriteLine();
      Console.WriteLine($"{Action} 執行結束，輸入任意鍵繼續...");
      Console.ReadLine();
    }
  }
}