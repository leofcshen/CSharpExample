namespace 用字串建立類別實例_介面;

internal class Program {
  //private static readonly string _calssName = "MyTestA";
  private static readonly string _calssName = "MyTestB";
  private static readonly string _parameter = "建構子參數";

  static void Main(/*string[] args*/) {
    var @namespace = AppDomain.CurrentDomain.FriendlyName;
    string namespaceClass = $"{@namespace}.{_calssName}";
    Type type = Type.GetType(namespaceClass) ?? throw new Exception($"找不到類別:{namespaceClass}");

    if (typeof(IRun).IsAssignableFrom(type)) {
      //IRun instance = (IRun)Activator.CreateInstance(type, "建構子參數") ?? throw new Exception($"找不到類別的介面:{nameof(IRun)}");

      // 檢查 CreateInstance 是否為 null
      object? objInstance = Activator.CreateInstance(type, _parameter) ?? throw new Exception($"無法實例化類別: {namespaceClass}");

      // 確保 objInstance 可以被轉換為 IRun 介面
      IRun instance = (IRun)objInstance ?? throw new Exception($"找不到類別的介面: {nameof(IRun)}");

      instance.Run();
    } else {
      Console.WriteLine("Class not implement IRun");
    }
  }
}

public interface IRun {
  void Run();
}

public class MyTestA(string name) : IRun {
  public string Name { get; set; } = name;

  public void Run() => Console.WriteLine($"AAA, Name = {Name}");
}

public class MyTestB(string name) : IRun {
  public string Name { get; set; } = name;

  public void Run() => Console.WriteLine($"BBB, Name = {Name}");
}
