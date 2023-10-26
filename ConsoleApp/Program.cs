using Library.Services;

namespace ConsoleApp {
	internal class Program {
		/// <summary> 切換要執行的動作 </summary>
		private static readonly EnumDemo Action = EnumDemo.None;

		static void Main() {
			try {
				new DemoBase().Services[Action]().Run();
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}

			Console.WriteLine();
			Console.WriteLine($"[{Action}] 執行結束，輸入任意鍵離開...");
			Console.ReadKey();
		}
	}

	public class DemoBase : DemoServicess<EnumDemo> {
		public DemoBase() : base("Demo") { }
		public override void Run() { }
	}
}
