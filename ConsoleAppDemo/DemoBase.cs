namespace ConsoleAppDemo {
  public interface IRun { void Run(); }

  public abstract class DemoBase : IRun {
    public abstract void Run();
  }

  public static class DemoFactory {
    /// <summary> Demo 服務 </summary>
    public static readonly Dictionary<EnumDemo, Func<IRun>> DemoServices = new Dictionary<EnumDemo, Func<IRun>>();

    /// <summary>
    /// 用反射依 Enum 產生對應的實例
    /// </summary>
    static DemoFactory() {
      foreach (EnumDemo demo in Enum.GetValues(typeof(EnumDemo))) {
        Type type = Type.GetType($"{typeof(DemoFactory).Namespace}.Demo.{demo}");
        if (type != null && typeof(IRun).IsAssignableFrom(type)) {
          DemoServices[demo] = () => (IRun)Activator.CreateInstance(type);
        }
      }
    }

    public static void Run<T>() where T : IRun, new() => new T().Run();
  }
}