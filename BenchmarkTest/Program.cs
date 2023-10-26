using Library.Services;

#if RELEASE
using BenchmarkDotNet.Running;
using System.Reflection;
#endif

namespace BenchmarkTest {
	public enum EnumTest {
		FastStringCreation,
		StringArraySlice,
		MyTest,
	}

	internal class Program {
		/// <summary> 切換要執行或測試的類別 </summary>
		private static readonly EnumTest Action = EnumTest.FastStringCreation;

		static void Main(string[] args) {
#if RELEASE
			//BenchmarkRunner.Run<ClassT>();
			string className = Action.ToString();
			string? nameSpace = MethodBase.GetCurrentMethod()?.DeclaringType?.Namespace;
			Type? t = Type.GetType($"{nameSpace}.ToTest.{className}");

			if (t is not null) BenchmarkRunner.Run(t);
#endif

#if DEBUG
			new ServiceBase().Services[Action]().Run();
			Console.ReadLine();
#endif
		}
	}

	public class ServiceBase : AbstractService<EnumTest> {
		public ServiceBase() : base("ToTest") { }
		public override void Run() { }
	}
}
