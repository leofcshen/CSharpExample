namespace Universe.Extensions;

public static class Ext_String {
  public static bool Get_Has異常字元(this string input)
    => SV_Regex.Regex異常字元().IsMatch(input);

  public static (bool Is有異常字元, List<(int 異常位置, string 異常字元)> 異常字元清單) Get_異常字元資料(this string input) {
    bool isMatch = SV_Regex.Regex異常字元().IsMatch(input);
    var matches = SV_Regex.Regex異常字元().Matches(input);
    var list = new List<(int 異常位置, string 異常字元)>();

    foreach (Match match in matches) {
      list.Add((match.Index, match.Value));
    }

    return (isMatch, list);
  }

  public static int GetLength中文算二(this string str) {
    int length = 0;

    foreach (var c in str)
      length += char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter ? 2 : 1;

    return length;
  }

  public static int GetLength中文算二_1(this string str) {
    int length = 0;

    foreach (var c in str) {
      length += char.GetUnicodeCategory(c) switch {
        System.Globalization.UnicodeCategory.OtherLetter => 2,
        _ => 1
      };
    }

    return length;
  }

  public static int GetLength中文算二_2(this string str)
    => str.Sum(c => char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter ? 2 : 1);
}
