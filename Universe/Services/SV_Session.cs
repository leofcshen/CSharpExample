namespace Universe.Services;

/// <summary>
/// SV_Session
/// </summary>
public class SV_Session {
  private static IHttpContextAccessor HttpContextAccessor
    => new HttpContextAccessor();

  /// <summary>存值</summary>
  public static void Set<T>(T value, [CallerMemberName] string sessionName = "")
    => HttpContextAccessor.HttpContext?.Session?.SetString(sessionName, JsonSerializer.Serialize(value));

  /// <summary>取值</summary>
  public static T Get<T>([CallerMemberName] string sessionName = "") {
    var session = HttpContextAccessor.HttpContext?.Session;

    if (session == null) {
      return default!;
    }

    var value = session.GetString(sessionName);

    if (value == null) {
      return default!;
    }

    return JsonSerializer.Deserialize<T>(value)!;
  }
}

public class SV_Session1 {
  private static readonly IHttpContextAccessor _httpContextAccessor
    = new HttpContextAccessor();

  private static ISession? Session
    => _httpContextAccessor.HttpContext?.Session;

  /// <summary>存值</summary>
  public static void Set<T>(T value, [CallerMemberName] string sessionName = "")
    => Session?.SetString(sessionName, JsonSerializer.Serialize(value));

  /// <summary>取值</summary>
  public static T Get<T>([CallerMemberName] string sessionName = "") {
    var value = Session?.GetString(sessionName);

    if (string.IsNullOrEmpty(value)) {
      return default!;
    }

    try {
      return JsonSerializer.Deserialize<T>(value)!;
    } catch (JsonException) {
      return default!;
    }
  }
}
