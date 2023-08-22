using Newtonsoft.Json;

namespace MyLibrary.Extensions {
  public static class ObjectExtensions {
    /// <summary>
    /// 印出物件資料
    /// </summary>
    /// <param name="pValue"></param>
    public static void Dump(this object pValue, String pDescription = "") {
      var json = JsonConvert.SerializeObject(pValue, Formatting.Indented);

      if (!string.IsNullOrEmpty(pDescription)) {
        Console.WriteLine($"[{pDescription}]");
      }

      Console.WriteLine(json);
      Console.WriteLine();
    }
  }
}