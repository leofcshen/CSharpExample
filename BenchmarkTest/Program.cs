using Library.Services;

#if RELEASE
using BenchmarkDotNet.Running;
using System.Reflection;
#endif

namespace BenchmarkTest {
	internal class Program {
		private static readonly EnumServices Action = EnumServices.FastStringCreation;

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

	public class ServiceBase : AbstractService<EnumServices> {
		public ServiceBase() : base("Services") { }
		public override void Run() { }
	}

	public enum EnumServices {
		FastStringCreation,
		StringArraySlice,
		MyTest,
	}
}
