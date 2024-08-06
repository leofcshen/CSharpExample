using Universe.Services;

namespace ConsoleApp_Net;

internal class Program {
  static void Main(/*string[] args*/) {
    try {
      SV_Windows.DeleteByDays(0);

    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
    }
    Console.WriteLine("Hello, World!");
  }
}
