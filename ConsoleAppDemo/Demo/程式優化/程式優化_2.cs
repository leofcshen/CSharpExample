using Library.Extensions;

namespace ConsoleAppDemo.Demo {
  internal class 程式優化_2 : DemoBase {
    public override void Run() {

      #region -- 原型 --
      string actionStr = "AAA";

      if (actionStr == "AAA") {
        "AAA".Dump();
      } else if (actionStr == "BBB") {
        "BBB".Dump();
      } else if (actionStr == "CCC") {
        "CCC".Dump();
      }
      #endregion

      var action = EnumTest.BBB;

      #region -- 使用列舉 --
      if (action == EnumTest.AAA) {
        EnumTest.AAA.ToString().Dump();
      } else if (action == EnumTest.BBB) {
        EnumTest.BBB.ToString().Dump();
      } else if (action == EnumTest.CCC) {
        EnumTest.CCC.ToString().Dump();
      }
      #endregion

      #region -- 使用列舉 switch --
      switch (action) {
        case EnumTest.AAA:
          EnumTest.AAA.ToString().Dump();
          break;
        case EnumTest.BBB:
          EnumTest.BBB.ToString().Dump();
          break;
        case EnumTest.CCC:
          EnumTest.CCC.ToString().Dump();
          break;
        default:
          break;
      }
      #endregion

      #region -- 封裝成類別 --
      TestBase.TestServices1[action].Run();
      TestFactory.TestServices2[action]().Run();
      TestFactory.TestServices3[action]().Run();
      //也可直接使用具體類型來運行
      TestFactory.Run<BBB>();
      #endregion
    }
  }

  public enum EnumTest {
    AAA,
    BBB,
    CCC,
  }

  public interface IShow { void Run(); }

  public abstract class TestBase : IShow {
    public abstract void Run();

    public static readonly Dictionary<EnumTest, IShow> TestServices1 = new Dictionary<EnumTest, IShow> {
      {EnumTest.AAA, new AAA()},
      {EnumTest.BBB, new BBB()},
      {EnumTest.CCC, new CCC()},
    };
  }

  public class AAA : TestBase {
    public override void Run() => EnumTest.AAA.ToString().Dump();
  }

  public class BBB : TestBase {
    public override void Run() => EnumTest.BBB.ToString().Dump();
  }

  public class CCC : TestBase {
    public override void Run() => EnumTest.CCC.ToString().Dump();
  }

  public class TestFactory {
    public static readonly Dictionary<EnumTest, Func<IShow>> TestServices2 = new Dictionary<EnumTest, Func<IShow>> {
      {EnumTest.AAA, () => new AAA()},
      {EnumTest.BBB, () => new BBB()},
      {EnumTest.CCC, () => new CCC()},
    };

    #region --
    public static readonly Dictionary<EnumTest, Func<IShow>> TestServices3 = new Dictionary<EnumTest, Func<IShow>>();

    /// <summary>
    /// 用反射依 Enum 產生對應的實例
    /// </summary>
    static TestFactory() {
      foreach (EnumTest demo in Enum.GetValues(typeof(EnumTest))) {
        Type type = Type.GetType($"{typeof(TestFactory).Namespace}.{demo}");
        if (type != null && typeof(IShow).IsAssignableFrom(type)) {
          TestServices3[demo] = () => (IShow)Activator.CreateInstance(type);
        }
      }
    }
    #endregion
    public static void Run<T>() where T : IShow, new() => new T().Run();
  }
}