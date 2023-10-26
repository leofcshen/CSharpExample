using Library.Services;

namespace ConsoleApp {
	public class ServiceBase : DemoServices<EnumDemo> {
		public ServiceBase() : base("Demo") { }
		public override void Run() { }
	}
}
