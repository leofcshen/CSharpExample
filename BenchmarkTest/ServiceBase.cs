using Library.Services;

namespace BenchmarkTest {
	public class ServiceBase : DemoServices<EnumTest> {
		public ServiceBase(string input) : base(input) { }
		public override void Run() { }
	}
}
