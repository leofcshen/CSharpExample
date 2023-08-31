using MyLibrary.Extensions;

namespace ConsoleAppDemo {
  internal class Program {
    /// <summary> 要執行的動作 </summary>
    private static readonly EnumDemo Action = EnumDemo.三元運算子_TernaryOperator;

    static void Main(string[] args) {
      new DemoFactory().Services[Action]().Run();

      Console.WriteLine();
      Console.WriteLine($"{Action} 執行結束，輸入任意鍵繼續...");
      Console.ReadLine();
    }
  }
}