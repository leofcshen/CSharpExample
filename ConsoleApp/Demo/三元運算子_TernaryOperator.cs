using Library.Extensions;

namespace ConsoleApp.Demo {
	internal class 三元運算子_TernaryOperator : DemoBase {
		public override void Run() {
			//一般方法
			string GetComment(int pScore) {
				string comment;

				if (pScore > 90) {
					comment = "你超強";
				} else {
					comment = "你就爛";
				}

				return comment;
			}

			GetComment(95).Dump(nameof(GetComment) + " " + 95);
			GetComment(80).Dump(nameof(GetComment) + " " + 80);

			//三元Lambda
			string GetComment_三元_Lambda(int pScore) => pScore > 90 ? "你超強" : "你就爛";

			GetComment_三元_Lambda(95).Dump(nameof(GetComment_三元_Lambda) + " " + 95);
			GetComment_三元_Lambda(80).Dump(nameof(GetComment_三元_Lambda) + " " + 80);

			//三元原型
			string GetComment_三元_原型(int pScore) {
				string comment;
				comment = pScore > 90 ? "你超強" : "你就爛";
				return comment;
			}

			GetComment_三元_原型(95).Dump(nameof(GetComment_三元_原型) + " " + 95);
			GetComment_三元_原型(80).Dump(nameof(GetComment_三元_原型) + " " + 80);

			//三元簡化
			string GetComment_三元_簡化(int pScore) {
				return pScore > 90 ? "你超強" : "你就爛";
			}

			GetComment_三元_簡化(95).Dump(nameof(GetComment_三元_簡化) + " " + 95);
			GetComment_三元_簡化(80).Dump(nameof(GetComment_三元_簡化) + " " + 80);

			//匿名方法要先宣告
			var getComment = (int pScore) => pScore > 90 ? "你超強" : "你就爛";

			getComment(95).Dump(nameof(getComment) + " " + 95);
			getComment(80).Dump(nameof(getComment) + " " + 80);
		}
	}
}
