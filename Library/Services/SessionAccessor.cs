using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

#if NET48
using System.Runtime.Serialization.Json;
#elif NET8_0_OR_GREATER
using System.Text.Json;
#endif

namespace Library.Services;

public class SessionAccessor(IHttpContextAccessor contextAccessor) {
	private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

	public void Set<T>(T value, [CallerMemberName] string name = "") {
		var bytes = SessionAccessor.GetBytes(value!);
		_contextAccessor.HttpContext.Session.Set(name, bytes);
	}

	//public T Get<T>([CallerMemberName] string name = "") {
	//	var bytes = _contextAccessor.HttpContext.Session.Get(name);

	//}

	//todo 過時程式
	//private byte[] GetBytes(object obj) {
	//	using var memoryStream = new MemoryStream();
	//	var binaryFormatter = new BinaryFormatter();
	//	binaryFormatter.Serialize(memoryStream, obj);

	//	return memoryStream.ToArray();
	//}

	private static byte[] GetBytes(object obj) {
#if NET48
		if (obj == null) {
			throw new ArgumentNullException(nameof(obj));
		}
#elif NET8_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(obj);
#endif

#if NET48
		using var memoryStream = new MemoryStream();
		var serializer = new DataContractJsonSerializer(obj.GetType());
		serializer.WriteObject(memoryStream, obj);

		return memoryStream.ToArray();
#elif NET8_0_OR_GREATER
		return JsonSerializer.SerializeToUtf8Bytes(obj);
#endif
	}

	//todo 過時程式
	//private object DeserializeBytes(byte[] bytes) {
	//	using var memoryStream = new MemoryStream(bytes);
	//	var binaryFormatter = new BinaryFormatter();

	//	return binaryFormatter.Deserialize(memoryStream);
	//}

	private static T DeserializeBytes<T>(byte[] bytes) {
#if NET48
		if (bytes == null) {
			throw new ArgumentNullException(nameof(bytes));
		}
#elif NET8_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(bytes);
#endif

#if NET48
		using var memoryStream = new MemoryStream(bytes);
		var serializer = new DataContractJsonSerializer(typeof(T));

		return (T)serializer.ReadObject(memoryStream);
#elif NET8_0_OR_GREATER
		return JsonSerializer.Deserialize<T>(bytes)!;
#endif
	}

}
