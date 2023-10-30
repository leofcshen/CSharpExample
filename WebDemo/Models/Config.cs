namespace WebDemo.Models {
	public static class Config {
		private static ConfigItem? _body;
		public static ConfigItem Body {
			get => _body ??= Program.ConfigMapping<ConfigItem>();
		}
	}

	public class ConfigItem {
		public AppSettings AppSettings { get; set; } = new AppSettings();
		public AppSettingsNew AppSettingsNew { get; set; } = new AppSettingsNew();
	}

	public class AppSettings {
		public string Url { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public int? Age { get; set; }
	}

	public class AppSettingsNew {
		public string Url { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public int? Age { get; set; }
	}
}
