using Library.Extensions;
using System.Reflection;

namespace ConsoleApp.Demo {
	internal class TypeGetType : DemoBase {
		public override void Run() {
			//取得所有組件，有引用的都會列出來
			var allAssembly = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName);
			var myList = allAssembly.Select(x => x.FullName).ToList();
			myList.Dump();

			//查自己的查得到
			Type? type1 = Type.GetType("System.Collections.ArrayList");
			type1.Dump(nameof(type1));

			//查引用的查不到，因為 GetType(string) 只能抓當前執行的程序集下的 Type
			Type? type2 = Type.GetType("Library.Extensions.GenericExtensions");
			type2.Dump(nameof(type2));

			//要在同一個字串加上第二個參數指定組件名稱。
			Type? type3 = Type.GetType("Library.Extensions.GenericExtensions, Library");
			type3.Dump(nameof(type3));

			//下列寫法也可以
			Type? type4 = Type.GetType(Assembly.CreateQualifiedName("Library", "Library.Extensions.GenericExtensions"));
			type4.Dump(nameof(type4));
		}
	}
}
