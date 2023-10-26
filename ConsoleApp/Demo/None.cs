namespace ConsoleApp.Demo {
	internal class None : ServiceBase {
		public override void Run() {
			var allAssembly = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName).ToList();
		}
	}
}
