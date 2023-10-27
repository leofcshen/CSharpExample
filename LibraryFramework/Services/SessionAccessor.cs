using System.Runtime.CompilerServices;
using System.Web;

namespace LibraryFramework.Services {
	/// <summary>
	/// Session 存取器
	/// </summary>
	public static class SessionAccessor {
		#region -- 方法區域 --
		/// <summary>
		/// 存值
		/// </summary>
		public static void Set<T>(T value, [CallerMemberName] string sessionName = "")
			=> HttpContext.Current.Session[sessionName] = value;

		/// <summary>
		/// 取值
		/// </summary>
		public static T Get<T>([CallerMemberName] string sessionName = "")
			=> (T)HttpContext.Current.Session[sessionName];
		#endregion
	}
}
