using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Library.Extensions;

namespace BenchmarkTest.Services {
	/// <summary>
	/// 方法傳入string[] 從第 start 開始擷取 number 個回傳
	/// </summary>
	[MemoryDiagnoser]
	[SimpleJob(RuntimeMoniker.Net48)]
	[SimpleJob(RuntimeMoniker.Net60)]
	public class StringArraySlice : ServiceBase {
		//設定兩組 benchmark 要測試的資料
		public static IEnumerable<object[]> GetParameters() {
			yield return new object[] { new string[] { "Student1", "Student2", "Student3", "Student4", "Student5", "Student6" }, 2, 2 };
			yield return new object[] { new string[] { "People1", "People2", "People3", "People4", "People5", "People6" }, 1, 3 };
		}

		[Benchmark]
		[ArgumentsSource(nameof(GetParameters))]
		public string[] ArraySlice(string[] names, int start, int number) => names.Skip(start).Take(number).ToArray();

		[Benchmark]
		[ArgumentsSource(nameof(GetParameters))]
		public ArraySegment<string> ArraySegmentSlice(string[] names, int start, int number) => new(names, start, number);

		[Benchmark]
		[ArgumentsSource(nameof(GetParameters))]
		public Span<string> SpanSlice(string[] names, int start, int number) => names.AsSpan().Slice(start, number);

		public override void Run() {
			var data = GetParameters();
			foreach (var paramSet in data) {
				Console.WriteLine("輸入值 = ");
				paramSet.Dump();

				Print(nameof(ArraySlice), ArraySlice((string[])paramSet[0], (int)paramSet[1], (int)paramSet[2]));
				Print(nameof(SpanSlice), SpanSlice((string[])paramSet[0], (int)paramSet[1], (int)paramSet[2]));
				Print(nameof(ArraySegmentSlice), ArraySegmentSlice((string[])paramSet[0], (int)paramSet[1], (int)paramSet[2]));
				Console.WriteLine(new string('*', 100));
			}
		}

		public static void Print(string methodName, Span<string> items) {
			Console.WriteLine($"方法 {methodName,-30} =>");
			foreach (var i in items) {
				Console.WriteLine(i);
			}
			Console.WriteLine();
		}

	}
}
