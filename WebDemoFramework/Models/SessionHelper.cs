using SA = LibraryFramework.Services.SessionAccessor;

namespace WebDemoFramework.Models {
  public static class SessionHelper {
    /// <summary>
    /// 我的瀏覽次數
    /// </summary>
    public static int 我的瀏覽次數 { get { return SA.Get<int>(); } set { SA.Set(value); } }
  }
}