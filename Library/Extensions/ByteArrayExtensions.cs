#if NET6_0_OR_GREATER
using System.Text.Json;

namespace Library.Extensions {
	public static class ByteArrayExtensions {
		public static T DeserializeBytes<T>(this byte[] bytes) {
			using var memoryStream = new MemoryStream(bytes);
			return JsonSerializer.Deserialize<T>(memoryStream)!;
		}
	}
}
#endif
