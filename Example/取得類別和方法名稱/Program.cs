using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace 取得類別和方法名稱;

internal class Program {
  static void Main(/*string[] args*/) {
    MyTestA myTestA = new();
    //myTestA.Print();
    TestC.Print();
  }
}

public class MyTestA {
  // 命名空間
  private static readonly string _namespace_typeof = typeof(MyTestA).Namespace!;
  private static readonly string _namespace_GetType = AppDomain.CurrentDomain.FriendlyName;
  private static readonly string _namespace_Assembly = Assembly.GetExecutingAssembly().GetName().Name!;

  // 類別名稱
  private static readonly string _className_nameof = nameof(MyTestA);
  private static readonly string _className_typeof = typeof(MyTestA).Name;
  private static readonly string _className_reflection = MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
  private static readonly string _classFullName_reflection = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;

  public void Print() {
    #region -- 取得命名空間 --
    CW(nameof(_namespace_typeof), _namespace_typeof);
    CW(nameof(_namespace_GetType), _namespace_GetType);
    CW(nameof(_namespace_Assembly), _namespace_Assembly);

    var namespace_typeof = typeof(MyTestA).Namespace!;
    CW(nameof(namespace_typeof), namespace_typeof);
    var namespaceAppDomain = AppDomain.CurrentDomain.FriendlyName;
    CW(nameof(namespaceAppDomain), namespaceAppDomain);
    var namespace_Assembly = Assembly.GetExecutingAssembly().GetName().Name!;
    CW(nameof(namespace_Assembly), namespace_Assembly);
    // 靜態類別無法使用下列 this.GetType().Namespace
    var namespace_GetType = this.GetType().Namespace!;
    CW(nameof(namespace_GetType), namespace_GetType);
    #endregion

    #region -- 取得類別名稱 --
    CW(nameof(_className_nameof), _className_nameof);
    CW(nameof(_className_typeof), _className_typeof);
    CW(nameof(_className_reflection), _className_reflection);
    CW(nameof(_classFullName_reflection), _classFullName_reflection);

    var className_nameof = nameof(MyTestA);
    CW(nameof(className_nameof), className_nameof);
    var className_typeof = typeof(MyTestA).Name;
    CW(nameof(className_typeof), className_typeof);
    var className_reflection = MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
    CW(nameof(className_reflection), className_reflection);
    var classFullName_reflection = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
    CW(nameof(classFullName_reflection), classFullName_reflection);
    #endregion

    #region -- 取得方法名稱 --
    // 方法一：nameof，編譯時期解析，幾乎無性能消耗
    var methodName_nameof = nameof(Print);
    CW(nameof(methodName_nameof), methodName_nameof);

    // 方法二：CallerMemberName，編譯時期解析 (一般場景最推薦)
    CW(nameof(GetMethodName_CallerMemberName), GetMethodName_CallerMemberName());

    // 方法三：表達式樹，適合用在 LINQ 和複雜邏輯中
    Expression<Action> expression = () => Print();
    var methodName_expression = ((MethodCallExpression)expression.Body).Method.Name;
    CW(nameof(methodName_expression), methodName_expression);

    // 方法四：委派方法
    Action action = Print;
    var methodName_delegate = action.Method.Name;
    CW(nameof(methodName_delegate), methodName_delegate);

    // 方法五：反射
    var methodName_reflection = MethodBase.GetCurrentMethod()!.Name;
    CW(nameof(methodName_reflection), methodName_reflection);

    // 方法六：完整堆棧跟踪解析
    var methodName_StackTrace = new StackTrace().GetFrame(0)!.GetMethod()!.Name;
    CW(nameof(methodName_StackTrace), methodName_StackTrace);

    CW(nameof(GetMethodName_MethodBase), GetMethodName_MethodBase());
    CW(nameof(GetMethodName_StackTrace), GetMethodName_StackTrace());
    CW(nameof(GetCallerName), GetCallerName());
    #endregion
  }

  public static string GetMethodName_CallerMemberName([CallerMemberName] string methodName = "") =>
    methodName;

  public static string GetMethodName_MethodBase() =>
    MethodBase.GetCurrentMethod()!.Name;

  public static string GetMethodName_StackTrace() {
    // 使用 StackTrace 來取得呼叫者的方法名稱
    var stackTrace = new StackTrace();
    var method = stackTrace.GetFrame(0)?.GetMethod(); // 取得上一層的方法
    return method != null ? method.Name : "Unknown Method";
  }

  public static string GetCallerName() =>
    GetMethodName_StatckTraceBase(2);

  public static string GetMethodName_StatckTraceBase(int index) =>
    new StackTrace().GetFrame(index)!.GetMethod()!.Name;

  public static void CW(string name, string value) =>
    Console.WriteLine($"{name,-30} = {value}");
}

public class TestB {
  public static void Print() {
    // 方法一
    var methodName_nameof = nameof(Print);

    // 方法二
    var methodName_CallerMemberName = GetMethodName_CallerMemberName();

    // 方法三
    Action action = Print;
    var methodName_delegate = action.Method.Name;

    // 方法四
    Expression<Action> expression = () => Print();
    var methodName_expression = ((MethodCallExpression)expression.Body).Method.Name;

    // 方法五
    var methodName_reflection = MethodBase.GetCurrentMethod()!.Name;

    // 方法六
    var methodName_StackTrace = new StackTrace().GetFrame(0)!.GetMethod()!.Name;
  }

  public static string GetMethodName_CallerMemberName([CallerMemberName] string methodName = "") =>
    methodName;
}

public class TestC {
  public static void Print() {
    // 進行效能測試
    MeasurePerformance();
  }

  private static void MeasurePerformance() {
    var iterations = 1000000;

    // 方法一：nameof
    var stopwatch = Stopwatch.StartNew();
    for (int i = 0; i < iterations; i++) {
      var methodName = nameof(Print);
    }
    stopwatch.Stop();
    Console.WriteLine($"{"nsmeof".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");

    // 方法二：CallerMemberName
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++) {
      var methodName = GetMethodName_CallerMemberName();
    }
    stopwatch.Stop();
    Console.WriteLine($"{"CallerMemberName".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");

    // 方法三：委派方法
    Action action = Print;
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++) {
      var methodName = action.Method.Name;
    }
    stopwatch.Stop();
    Console.WriteLine($"{"委派".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");

    // 方法四：表達式樹
    Expression<Action> expression = () => Print();
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++) {
      var methodName = ((MethodCallExpression)expression.Body).Method.Name;
    }
    stopwatch.Stop();
    Console.WriteLine($"{"表達式樹".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");

    // 方法五：反射
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++) {
      var methodName = MethodBase.GetCurrentMethod()!.Name;
    }
    stopwatch.Stop();
    Console.WriteLine($"{"反射".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");

    // 方法六：堆疊追蹤
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++) {
      var methodName = new StackTrace().GetFrame(0)!.GetMethod()!.Name;
    }
    stopwatch.Stop();
    Console.WriteLine($"{"堆疊追蹤".PadLeft(50)}: {stopwatch.ElapsedMilliseconds} ms");
  }

  public static string GetMethodName_CallerMemberName([CallerMemberName] string methodName = "") =>
    methodName;
}
