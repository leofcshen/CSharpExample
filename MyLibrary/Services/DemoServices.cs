namespace MyLibrary.Services {
  public interface IRun {
    /// <summary>
    /// 實際呼叫結果
    /// </summary>
    void Run();
  }

  public abstract class DemoServicess<TEnum> : IRun {
    public readonly Dictionary<TEnum, Func<IRun>> Services = new();

    public abstract void Run();

    /// <summary>
    /// 建構函式
    /// </summary>
    /// <param name="namespaceSegment">類別所在的子資料夾</param>
    public DemoServicess(string namespaceSegment) {
      CreateServices(namespaceSegment);
    }

    /// <summary>
    /// 使用列舉產生  Services
    /// <para>{ TEnum.AAA, new AAA() },</para>
    /// <para>{ TEnum.BBB, new CCC() },</para>
    /// </summary>
    /// <param name="namespaceSegment">類別所在的子資料夾</param>
    private void CreateServices(string namespaceSegment) {
      string? projcetNamespace = GetType().Namespace;
      foreach (TEnum className in Enum.GetValues(typeof(TEnum))) {
        Type? type = Type.GetType($"{projcetNamespace}.{namespaceSegment}.{className}, {projcetNamespace}");
        if (type != null && typeof(IRun).IsAssignableFrom(type)) {
          Services[className] = () => Activator.CreateInstance(type) as IRun;
        }
      }
    }
  }
}