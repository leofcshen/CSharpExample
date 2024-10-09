namespace 用字串建立類別實例_繼承;

internal class Program {
  private static readonly string _calssName = "MyTestA";
  //private static readonly string _calssName = "MyTestB";
  //private static readonly string _calssName = "MyTestC";
  private static readonly string _parameter = "建構子參數";

  static void Main(/*string[] args*/) {
    object?[] _parameter = ["Tom"];
    var @namespace = AppDomain.CurrentDomain.FriendlyName;
    string namespaceClass = $"{@namespace}.{_calssName}";
    Type? type = Type.GetType(namespaceClass) ?? throw new Exception($"找不到類別:{namespaceClass}");

    Base instance = Activator.CreateInstance(type, _parameter) as Base ?? throw new Exception($"無法創建類別: {_calssName}");
    instance.Run();
  }
}

public abstract class Base(string name) {
  public string Name { get; set; } = name;

  public abstract void Run();
}

public class MyTestA(string name) : Base(name) {
  public override void Run() => Console.WriteLine($"AAA, Name = {Name}");
}

public class MyTestB(string name) : Base(name) {
  public override void Run() => Console.WriteLine($"BBB, Name = {Name}");
}
