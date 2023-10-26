using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace BenchmarkTest.ToTest {
	/// <summary>
	/// 方法傳入一串文字回傳屏蔽第四碼後的文字 Pas*********
	/// </summary>
	[SimpleJob(RuntimeMoniker.Net48)]
	[SimpleJob(RuntimeMoniker.Net60)]
	[MemoryDiagnoser]
	public class FastStringCreation : ServiceBase {
		//Benchmark 測試用的參數必須為常數
		private const string TestValue = "Password123!";

		/// <summary>
		/// 幼稚園方法
		/// <para>記憶體耗費 400 B</para>
		/// </summary>
		[Benchmark]
		[Arguments(TestValue)]
		public string MaskNaive(string clearValue) {
			var firstChars = clearValue.Substring(0, 3);
			var length = clearValue.Length - 3;
			for (int i = 0; i < length; i++) {
				// string 是 immutable，每次都 allocate 新的 string
				firstChars += '*';
			}
			return firstChars;
		}

		/// <summary>
		/// 使用 StringBuilder
		/// <para>記憶體耗費 184 B</para>
		/// </summary>
		[Benchmark]
		[Arguments(TestValue)]
		public string MaskStringBuilder(string clearValue) {
			var firstChars = clearValue.Substring(0, 3);
			var length = clearValue.Length - 3;
			var stringBuilder = new StringBuilder(firstChars);
			for (int i = 0; i < length; i++) {
				stringBuilder.Append('*');
			}
			//ToString() 時才分配
			return stringBuilder.ToString();
		}

		/// <summary>
		/// 使用 New String
		/// <para>記憶體耗費 120 B</para>
		/// </summary>
		[Benchmark]
		[Arguments(TestValue)]
		public string MaskNewString(string clearValue) {
			var firstChars = clearValue.Substring(0, 3);
			var length = clearValue.Length - 3;
			var asterisks = new string('*', length);
			return firstChars + asterisks;
		}

		/// <summary>
		/// 使用 span
		/// <para>記憶體耗費 48 B</para>
		/// </summary>
		[Benchmark]
		[Arguments(TestValue)]
		public string MaskStringCreate(string clearValue) {
#if NET6_0_OR_GREATER
			return string.Create(clearValue.Length, clearValue, (span, value) => {
				value.AsSpan().CopyTo(span);
				span[3..].Fill('*');
			});
#else
      return string.Empty;
#endif
		}

		/// <summary>
		/// 使用 Padding
		/// <para>記憶體耗費 80 B</para>
		/// </summary>
		[Benchmark]
		[Arguments(TestValue)]
		public string MaskPadding(string clearValue) {
			var firstChars = clearValue.Substring(0, 3);
			var length = clearValue.Length;
			//Padding 底層也是使用 span
			return firstChars.PadRight(length, '*');
		}

		public override void Run() {
			var methods = new Dictionary<string, Func<string, string>> {
				{ nameof(MaskNaive), MaskNaive },
				{ nameof(MaskStringBuilder), MaskStringBuilder },
				{ nameof(MaskNewString), MaskNewString },
				{ nameof(MaskStringCreate), MaskStringCreate },
				{ nameof(MaskPadding), MaskPadding }
			};

			Console.WriteLine("輸入值 = " + TestValue);
			foreach (var method in methods) {
				Console.WriteLine($"方法 {method.Key.PadRight(30)} => {method.Value(TestValue)}");
			}
		}
	}
}
