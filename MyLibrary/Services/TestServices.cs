namespace MyLibrary.Services {
  public interface IRun {
    /// <summary>
    /// 實際呼叫結果
    /// </summary>
    void Run();
  }

  public class TestServicess<TEnum> {
    public readonly Dictionary<TEnum, Func<IRun>> Services = new Dictionary<TEnum, Func<IRun>>();

    public TestServicess(string namespaceSegment) {
      foreach (TEnum demo in Enum.GetValues(typeof(TEnum))) {
        Type? type = Type.GetType($"{GetType().Namespace}.{namespaceSegment}.{demo}, {GetType().Namespace}");
        if (type != null && typeof(IRun).IsAssignableFrom(type)) {
          Services[demo] = () => (IRun)Activator.CreateInstance(type);
        }
      }
    }
  }
}
