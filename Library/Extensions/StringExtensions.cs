namespace Library.Extensions {
	public static class StringExtensions {
		public static string MyMethod(string pString)
			=> $"{nameof(MyMethod)} {pString}";
		public static string MyMethodExtended(this string pString)
			=> $"{nameof(MyMethodExtended)} {pString}";
		public static string ToString(this string pString)
			=> $"MyToString {pString}";
		public static string MyMethodSameName(this string input)
			=> $"Method [{nameof(MyMethodSameName)}] from Class [{nameof(StringExtensions)}]";
	}

	public static class StringExtensions_New {
		public static string MyMethodSameName(this string input)
			=> $"Method [{nameof(MyMethodSameName)}] from Class [{nameof(StringExtensions_New)}]";
	}
}
