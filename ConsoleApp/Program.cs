using Library.Services;

namespace ConsoleApp {
	internal class Program {
		private static readonly EnumServices Action = EnumServices.None;

		static void Main() {
			try {
				new ServiceBase().Services[Action]().Run();
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}

			Console.WriteLine();
			Console.WriteLine($"[{Action}] 執行結束，輸入任意鍵離開...");
			Console.ReadKey();
		}
	}

	public class ServiceBase : AbstractService<EnumServices> {
		public ServiceBase() : base("Demo") { }
		public override void Run() { }
	}

	public enum EnumServices {
		None,
		三元運算子_TernaryOperator,
		三元運算子_TernaryOperator_多層,
		Method_方法,
		程式優化_2,
		Reflection_反射,
		程式優化_1,
		TypeGetType,
		ThrowException_拋錯,
		Debug_偵錯,
	}
}
