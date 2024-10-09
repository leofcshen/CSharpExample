using System.Diagnostics;
using System.Reflection;

namespace 動態載入類別;

internal class Program {
  private static readonly bool _is不執行_展示用;

  static void Main(/*string[] args*/) {
    CallClassMethod("SV_A", "Print", ["Tom"]);
    CallClassMethod("SV_A", "Print", ["Tom", "John"]);
    CallClassMethod("SV_A", "Print", ["Tom", 20]);

    CallClassMethod("SV_B", "Print", ["Tom"]);
    CallClassMethod("SV_B", "Print", ["Tom", "John"]);
    CallClassMethod("SV_B", "Print", ["Tom", 20]);

    CallClassMethod("SV_C", "Print", ["Tom"]);
    CallClassMethod("SV_C", "Print", ["Tom", "John"]);
    CallClassMethod("SV_C", "Print", ["Tom", 20]);
  }

  static void CallClassMethod(string className, string methodName, object?[] parameters) {
    try {
      var @namespace = AppDomain.CurrentDomain.FriendlyName;
      var fullClassName = $"{@namespace}.{className}";
      // 動態載入類別
      var type = Type.GetType(fullClassName) ?? throw new Exception($"找不到類別:{fullClassName}");
      var is靜態類別 = type.IsAbstract && type.IsSealed;
      // 根據參數類型取得對應的方法
      var types = parameters.Select(x => x?.GetType() ?? typeof(object)).ToArray();
      var method = type.GetMethod(methodName, types);

      if (_is不執行_展示用) {
        // 沒有方法重載的話不需要第二個參數
        method = type.GetMethod(methodName);
      }

      // 靜態類別不需要實例化
      var instance = is靜態類別 ? null : Activator.CreateInstance(type);
      // 呼叫方法
      method?.Invoke(instance, parameters);

      if (_is不執行_展示用) {
        // 靜態類別不需要實例化
        method?.Invoke(null, parameters);
        // 方法不需要參數的話給 null
        method?.Invoke(instance, null);
        // 靜態類別且方法無參數
        method?.Invoke(null, null);
      }
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
      Console.WriteLine(ex.StackTrace);
    }
  }
}

public static class SV_A {
  private static string _className = MethodBase.GetCurrentMethod().DeclaringType.Name;
  private static string _fullClassName = MethodBase.GetCurrentMethod().DeclaringType.FullName;
  private static string GetCurrentMethodName() {
    return MethodBase.GetCurrentMethod().Name;
  }

  // 公用方法來獲取當前呼叫的名稱
  private static string GetCallingMethodName() {
    var stackTrace = new StackTrace(); // 獲取當前堆疊跟蹤
    var callingMethod = stackTrace.GetFrame(2)?.GetMethod(); // 獲取上層方法（呼叫者）
    return callingMethod?.Name; // 返回呼叫者方法名稱
  }
  public static void Print(string name) => Console.WriteLine($"{_fullClassName} {GetCurrentMethodName()}:Hi {name}");

  public static void Print(string nameA, string nameB) =>
    Console.WriteLine($"{_fullClassName} {GetCurrentMethodName()}:Hi {nameA}, I'm {nameB}");
  public static void Print(string name, int age) =>
    Console.WriteLine($"{_fullClassName} {GetCurrentMethodName()}: I'm {name}, {age} years old");
}

public class SV_B {
  private static string _className = MethodBase.GetCurrentMethod().DeclaringType.Name;
  private static string _fullClassName = MethodBase.GetCurrentMethod().DeclaringType.FullName;
  public void Print(string name) => Console.WriteLine($"{_fullClassName} Print:Hi {name}");

  public void Print(string nameA, string nameB) =>
    Console.WriteLine($"{_fullClassName} Print:Hi {nameA}, I'm {nameB}");

  public void Print(string name, int age) =>
    Console.WriteLine($"{_fullClassName} Print: I'm {name}, {age} years old");
}
