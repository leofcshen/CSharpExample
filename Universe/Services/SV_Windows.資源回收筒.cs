namespace Universe.Services;

public static partial class SV_Windows {
  //LINQPad 不能用
  //[LibraryImport("shell32.dll", EntryPoint = "SHEmptyRecycleBinW", StringMarshalling = StringMarshalling.Utf16)]
  //private static partial int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

  [DllImport("shell32.dll", CharSet = CharSet.Unicode, EntryPoint = "SHEmptyRecycleBinW")]
  private static extern int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  private static extern void SHUpdateRecycleBinIcon();

  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

  public static void Run刷新資源回收筒圖示() => SHUpdateRecycleBinIcon();

  const uint SHERB_NOCONFIRMATION = 0x00000001;
  const uint SHERB_NOPROGRESSUI = 0x00000002;
  const uint SHERB_NOSOUND = 0x00000004;
  const int E_資源回收筒是空的 = unchecked((int)0x8000FFFF);

  public static My執行結果 Run清空資源回收筒(bool isThrowError = false) {
    uint flags = SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND;
    int result = SHEmptyRecycleBin(IntPtr.Zero, null, flags);

    var resultMessage = result switch {
      0 => "資源回收筒已清空",
      E_資源回收筒是空的 => "資源回收筒是空的",
      _ => $"清空資源回收筒失敗，錯誤碼：{result}",
    };

    if (isThrowError && result != 0) {
      throw new Exception(resultMessage);
    }

    My執行結果 r = new() {
      IsSuccess = result == 0,
      Message = resultMessage
    };

    return r;
  }

  #region -- 刪除超過 n 天的檔案 --
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
  /// <summary>
  /// 不顯示任何對話方塊
  /// </summary>
  private const int FOF_NO_UI = 0x0400;

  public static void Run刪除資源回收筒超過n天(int days) {
    // 定義刪除時間
    DateTime limit = DateTime.Now.AddDays(-days);

    dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application")!)!;
    dynamic recycleBin = shell.NameSpace(10); // 0xa in hexadecimal is 10 in decimal

    foreach (dynamic item in recycleBin.Items()) {
      DateTime dateDeleted = GetDateDeleted(item);
      if (dateDeleted < limit) {
        try {
          DeleteFileDirectly(item.Path.ToString());
        } catch (Exception ex) {
          throw new Exception($"無法刪除檔案: {item.Path}，錯誤訊息: {ex.Message}");
        }
      }
    }

    Run刷新資源回收筒圖示();
  }

  static DateTime GetDateDeleted(dynamic item) {
    try {
      return item.ExtendedProperty("System.Recycle.DateDeleted");
    } catch {
      return DateTime.MaxValue;
    }
  }

  static void DeleteFileDirectly(string filePath) {
    SHFILEOPSTRUCT fileOp = new() {
      wFunc = FO_DELETE,
      pFrom = filePath + '\0' + '\0',
      fFlags = FOF_NO_UI
    };

    int result = SHFileOperation(ref fileOp);
    if (result != 0) {
      throw new Exception($"SHFileOperation failed with code {result}");
    }
  }
  #endregion
}
