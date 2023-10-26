namespace ConsoleApp.Demo {
	internal class None : DemoBase {
		public override void Run() {
			var allAssembly = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName).ToList();
		}
	}
}
