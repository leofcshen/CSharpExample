using Library.Services;

namespace BenchmarkTest {
	public class ServiceBase : DemoServices<EnumTest> {
		public ServiceBase() : base("ToTest") { }
		public override void Run() { }
	}
}
