using System.Runtime.CompilerServices;
using System.Text.Json;

namespace WebDemo.Models;

public class SessionAccessor(IHttpContextAccessor contextAccessor) {
  private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

  public void Set<T>(T value, [CallerMemberName] string name = "")
      => _contextAccessor.HttpContext?.Session.Set(name, JsonSerializer.SerializeToUtf8Bytes(value));

  public T Get<T>([CallerMemberName] string name = "") {
    var data = _contextAccessor.HttpContext?.Session.Get(name);
    return data == null ? default! : JsonSerializer.Deserialize<T>(data)!;
  }

  //public static void SetObject(this ISession session, string key, object value) {
  //	session.Set(key, GetBytes(value));
  //}

  //public static T GetObject<T>(this ISession session, string key) {
  //	var value = session.GetString(key);
  //	return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
  //}
}
