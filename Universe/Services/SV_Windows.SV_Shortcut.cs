namespace Universe.Services;

public partial class SV_Windows {
  /// <summary>
  /// 產生捷徑到桌面
  /// </summary>
  public static class SV_Shortcut {
    public static void Create_編輯環境變數() {
      var shortcutName = "編輯-環境變數";
      SV_IShellLink.IShellLink link = (SV_IShellLink.IShellLink)new SV_IShellLink.ShellLink();
      link.SetDescription(shortcutName);
      link.SetPath(@"C:\Windows\System32\rundll32.exe");
      link.SetArguments(@"sysdm.cpl,EditEnvironmentVariables");
      link.SetWorkingDirectory(@"C:\Windows\System32");

      // 設定圖示
      link.SetIconLocation(@"%SystemRoot%\System32\shell32.dll", 24);

      SV_IShellLink.CreateShortcut(shortcutName, link, is管理員權限: true);
    }

    public static void Create_編輯hosts() {
      var shortcutName = "編輯-hosts";
      SV_IShellLink.IShellLink link = (SV_IShellLink.IShellLink)new SV_IShellLink.ShellLink();
      link.SetDescription(shortcutName);
      link.SetPath(@"C:\Windows\System32\notepad.exe");
      link.SetArguments(@"C:\Windows\System32\drivers\etc\hosts");
      link.SetWorkingDirectory(@"C:\Windows\System32");

      SV_IShellLink.CreateShortcut(shortcutName, link, is管理員權限: true);
    }

  }
}
