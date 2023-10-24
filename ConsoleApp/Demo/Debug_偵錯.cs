using System.Diagnostics;

namespace ConsoleApp.Demo {
  internal class Debug_偵錯 : DemoBase {
    public override void Run() {
      string message = "This is test.";
      Console.WriteLine(message);

      //會顯示在輸出的偵錯頁
      Debug.WriteLine(message);

      //condition false 時會停下來
      Debug.Assert(message == "This is test.");

#if DEBUG
      //Debug 模式才會編譯代碼
      Console.WriteLine("Debug mode");
#endif
    }
  }
}
