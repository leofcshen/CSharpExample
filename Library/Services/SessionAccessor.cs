using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Services {
	public class SessionAccessor {
		private readonly IHttpContextAccessor _contextAccessor;

		public SessionAccessor(IHttpContextAccessor contextAccessor) {
			_contextAccessor = contextAccessor;
		}

		public void Set<T>(T value, [CallerMemberName] string name = "") {
			var bytes = GetBytes(value);
			_contextAccessor.HttpContext.Session.Set(name, bytes);
		}

		//public T Get<T>([CallerMemberName] string name = "") {
		//	var bytes = _contextAccessor.HttpContext.Session.Get(name);

		//}

		private byte[] GetBytes(object obj) {
			using var memoryStream = new MemoryStream();
			var binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, obj);

			return memoryStream.ToArray();
		}

		private object DeserializeBytes(byte[] bytes) {
			using var memoryStream = new MemoryStream(bytes);
			var binaryFormatter = new BinaryFormatter();

			return binaryFormatter.Deserialize(memoryStream);
		}
	}
}
