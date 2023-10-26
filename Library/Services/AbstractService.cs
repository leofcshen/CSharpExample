using System.Reflection;

namespace Library.Services {
	public interface IRun {
		/// <summary>
		/// 實際呼叫結果
		/// </summary>
		void Run();
	}

	public abstract class AbstractService<TEnum> : IRun where TEnum : notnull {
		public readonly Dictionary<TEnum, Func<IRun>> Services = new();

		public abstract void Run();

		/// <summary>
		/// 建構函式
		/// </summary>
		/// <param name="namespaceSegment">類別所在的子資料夾</param>
		public AbstractService(string namespaceSegment) { CreateServices(namespaceSegment); }

		/// <summary>
		/// 使用列舉產生  Services
		/// <para>{ TEnum.AAA, new AAA() },</para>
		/// <para>{ TEnum.BBB, new CCC() },</para>
		/// </summary>
		/// <param name="namespaceSegment">類別所在的子資料夾</param>
		private void CreateServices(string namespaceSegment) {
			string? projcetNamespace = GetType().Namespace;

			foreach (TEnum className in Enum.GetValues(typeof(TEnum))) {
				var path = $"{projcetNamespace}.{namespaceSegment}.{className}";
				Type? type = Type.GetType(Assembly.CreateQualifiedName(projcetNamespace, path));

				if (type != null && typeof(IRun).IsAssignableFrom(type)) {
					var instance = Activator.CreateInstance(type) as IRun;

					if (instance is not null) {
						Services[className] = () => instance;
					}
				}
			}
		}
	}
}
