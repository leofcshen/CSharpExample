using Universe.Services;

namespace ConsoleApp_Net;

internal class Program {
	static void Main(/*string[] args*/) {
		try {
			SV_Windows.Run_產生編輯hosts捷徑到桌面();

		} catch (Exception ex) {
			Console.WriteLine(ex.Message);
		}
		Console.WriteLine("Hello, World!");
	}
}
