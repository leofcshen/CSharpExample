#if NET6_0_OR_GREATER
using System.Text.Json;

namespace Library.Extensions {
	public static class ByteArrayExtensions {
		public static T DeserializeBytes<T>(this byte[] bytes) {
			using var memoryStream = new MemoryStream(bytes);
			var options = new JsonSerializerOptions {
				IncludeFields = true // 如果需要包括字段，请设置此选项为 true
			};

			return JsonSerializer.Deserialize<T>(memoryStream, options)!;
		}
	}
}
#endif
