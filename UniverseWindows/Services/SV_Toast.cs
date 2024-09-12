namespace UniverseWindows.Services;

public class SV_Toast {
  /// <summary>
  /// 顯示桌面通知
  /// </summary>
  /// <param name="text"></param>
  /// <param name="title"></param>
  /// <param name="uri"></param>
  public static void ShowToast(string? text = null, string? title = null, Uri? uri = null) {
    //title ??= Assembly.GetExecutingAssembly().GetName().Name; //這個會顯示類別庫名稱
    title ??= Process.GetCurrentProcess().ProcessName;

    var toastBuilder = new ToastContentBuilder()
      .AddText(title);

    if (text is not null) {
      toastBuilder.AddText(text);
    }

    if (uri is not null) {
      toastBuilder.AddInlineImage(uri);
    }

    toastBuilder.Show(x => x.ExpirationTime = DateTimeOffset.Now.AddSeconds(3));
  }

  //=> new ToastContentBuilder()
  //.AddText(title ?? nameof(MyApp))
  //.AddText(text)
  //.AddInlineImage(uri ?? null)
  //.Show(x => x.ExpirationTime = DateTimeOffset.Now.AddSeconds(3));
}
