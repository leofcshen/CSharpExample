using Universe.Services;

namespace WebDemo.Models;

public class Session {
  public static string Name { get => SV_Session.Get<string>(); set => SV_Session.Set(value); }
}
