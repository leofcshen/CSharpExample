using MyLibrary.Extensions;
using System.Reflection;

namespace ConsoleAppDemo.Demo {
  internal class Reflection_反射 : DemoBase {
    /// <summary> NameSpace1 </summary>
    private static readonly string NameSpace1 = typeof(Reflection_反射).Namespace;
    /// <summary> NameSpace2 </summary>
    private static string NameSpace2 { get; set; }

    /// <summary> Class 名稱 </summary>
    private static readonly string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;

    public Reflection_反射() {
      NameSpace2 = this.GetType().Namespace;
    }

    public override void Run() {
      NameSpace1.Dump(nameof(NameSpace1));
      NameSpace2.Dump(nameof(NameSpace2));

      ClassName.Dump(nameof(ClassName));

      string methodName = MethodBase.GetCurrentMethod().Name;
      methodName.Dump(nameof(methodName));
    }
  }
}
