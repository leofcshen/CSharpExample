using Newtonsoft.Json;

namespace MyLibrary.Extensions {
  public static class GenericExtensions {
    public static void Dump<T>(this T pValue, string pDescription = "") {
      var json = JsonConvert.SerializeObject(pValue, Formatting.Indented);

      if (!string.IsNullOrEmpty(pDescription)) {
        Console.WriteLine($"[{pDescription}]");
      }

      Console.WriteLine(json);
      Console.WriteLine();
    }
  }
}
