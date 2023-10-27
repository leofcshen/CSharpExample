using Library.Extensions;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace WebDemo.Models {
	public class SessionAccessor {
		private readonly IHttpContextAccessor _contextAccessor;

		public SessionAccessor(IHttpContextAccessor contextAccessor) {
			_contextAccessor = contextAccessor;
		}

		public void Set<T>(T value, [CallerMemberName] string name = "")
			=> _contextAccessor.HttpContext?.Session.Set(name, JsonSerializer.SerializeToUtf8Bytes(value));

		public T Get<T>([CallerMemberName] string name = "")
			=> (_contextAccessor.HttpContext?.Session.Get(name)!).DeserializeBytes<T>();

		//public static void SetObject(this ISession session, string key, object value) {
		//	session.Set(key, GetBytes(value));
		//}

		//public static T GetObject<T>(this ISession session, string key) {
		//	var value = session.GetString(key);
		//	return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		//}
	}
}
