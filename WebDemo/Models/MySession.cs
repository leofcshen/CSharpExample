namespace WebDemo.Models {
  public class MySession(IHttpContextAccessor contextAccessor) {
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string Name {
      get => new SessionAccessor(_contextAccessor).Get<string>();
      set => new SessionAccessor(_contextAccessor).Set(value);
    }
  }

  //public static class SessionAccessorExtension {
  //	public static string Name { set => SetName(value); }
  //	public static void SetName(this ISession session, string value) {
  //		session.SetObject(nameof(Name), value);
  //	}

  //	public static string GetName(this ISession session) {
  //		//return session.GetObject<string>(nameof(Name));
  //	}
  //	//public static string Name { set => SetObject(nameof(Name), value); }

  //	public static void SetObject(this ISession session, string key, object value) {
  //		session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
  //	}

  //	public static T DeserializeBytes<T>(this byte[] bytes) {
  //		using var memoryStream = new MemoryStream(bytes);
  //		var options = new JsonSerializerOptions {
  //			IncludeFields = true // 如果需要包括字段，请设置此选项为 true
  //		};

  //		return JsonSerializer.Deserialize<T>(memoryStream, options)!;
  //	}
  //}
}
