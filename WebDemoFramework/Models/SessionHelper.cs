using LibraryFramework.Services;
using SA = LibraryFramework.Services.SessionAccessor;

namespace WebDemoFramework.Models {
  public static class SessionHelper {
    public static int? Count { get { return SA.Get<int>(); } set { SA.Set(value); } }
  }
}