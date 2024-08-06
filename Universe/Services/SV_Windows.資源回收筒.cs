namespace Universe.Services;

public static partial class SV_Windows {
  //LINQPad 不能用
  //[LibraryImport("shell32.dll", EntryPoint = "SHEmptyRecycleBinW", StringMarshalling = StringMarshalling.Utf16)]
  //private static partial int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

  [DllImport("shell32.dll", CharSet = CharSet.Unicode, EntryPoint = "SHEmptyRecycleBinW")]
  private static extern int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

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
}

public class My執行結果 {
  public bool IsSuccess { get; set; }
  public string? Message { get; set; }
}
