namespace Universe.Services;

public static partial class SV_Regex {
  public const string 異常字元 = @"[\p{C}]";
  public const string 手機號碼 = @"^09\d{8}$";

  [GeneratedRegex(異常字元, RegexOptions.IgnoreCase, "en-Us")]
  public static partial Regex Regex異常字元();

  [GeneratedRegex(手機號碼)]
  public static partial Regex Regex手機號碼();
}
