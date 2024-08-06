namespace Universe.Services;

public static partial class SV_Windows {
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  private struct SHFILEOPSTRUCT {
    public IntPtr hwnd;
    [MarshalAs(UnmanagedType.U4)]
    public int wFunc;
    public string pFrom;
    public string pTo;
    public short fFlags;
    [MarshalAs(UnmanagedType.Bool)]
    public bool fAnyOperationsAborted;
    public IntPtr hNameMappings;
    public string lpszProgressTitle;
  }

  private const int FO_DELETE = 3;
  private const int FOF_NO_UI = 0x0400; // Do not display a progress dialog box
  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

  public static void DeleteByDays(int days) {
    // 定義刪除時間
    DateTime limit = DateTime.Now.AddDays(-days);

    dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
    dynamic recycleBin = shell.NameSpace(10); // 0xa in hexadecimal is 10 in decimal

    foreach (dynamic item in recycleBin.Items()) {
      DateTime dateDeleted = GetDateDeleted(item);
      if (dateDeleted < limit) {
        try {
          DeleteFileDirectly(item.Path.ToString());
        } catch (Exception ex) {
          Console.WriteLine($"無法刪除檔案: {item.Path}，錯誤訊息: {ex.Message}");
        }
      }
    }

    Run刷新資源回收筒();
  }

  static DateTime GetDateDeleted(dynamic item) {
    try {
      return item.ExtendedProperty("System.Recycle.DateDeleted");
    } catch {
      return DateTime.MaxValue;
    }
  }

  static void DeleteFileDirectly(string filePath) {
    SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT {
      wFunc = FO_DELETE,
      pFrom = filePath + '\0' + '\0',
      fFlags = FOF_NO_UI
    };

    int result = SHFileOperation(ref fileOp);
    if (result != 0) {
      throw new Exception($"SHFileOperation failed with code {result}");
    }
  }
}
