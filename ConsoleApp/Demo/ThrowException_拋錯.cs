using System.Diagnostics;

namespace ConsoleApp.Demo {
	internal class ThrowException_拋錯 : DemoBase {
		public override void Run() {
			try {
				Aoo aoo = new();
				aoo.DoSomethingInAoo();
			} catch (Exception ex) {
				Console.WriteLine("----- stack info -----");
				Console.WriteLine(ex.StackTrace);
				Console.WriteLine("----------------------");
			}
		}
	}

	public class Aoo {
		public Boo boo = new();

		public void DoSomethingInAoo() {
			boo.DoSomethingInBoo();
		}
	}

	public class Boo {
		public Coo coo = new();

		public void DoSomethingInBoo() {
			try {
				coo.DoSomethingInCoo();
			} catch (Exception ex) {
				Debug.WriteLine(ex);

				//throw ex 會重置呼叫堆疊，造成確切發生例外的位置遺失。
				//throw ex;
				throw;
			}
		}
	}

	public class Coo {
		public Doo doo = new();

		public void DoSomethingInCoo() {
			doo.DoSomethingInDoo();
		}
	}

	public class Doo {
		public void DoSomethingInDoo() {
			int i = 0;
			//exception occured
			int _ = 1 / i;
		}
	}
}
