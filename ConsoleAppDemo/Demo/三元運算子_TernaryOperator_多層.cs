using Library.Extensions;

namespace ConsoleApp.Demo {
  internal class 三元運算子_TernaryOperator_多層 : DemoBase {
    public override void Run() {
      int scorePro = 90;
      int scoreGood = 70;
      int scoreNormal = 60;

      int score100 = 100;
      int score95 = 95;
      int score80 = 80;
      int score65 = 65;
      int score50 = 50;

      string pro = "你超強";
      string good = "你還行";
      string normal = "你普普";
      string poor = "你就爛";



      //一般方法
      string GetComment(int pScore) {
        string comment;

        if (pScore >= scorePro) {
          comment = pro;
        } else if (pScore >= scoreGood) {
          comment = good;
        } else if (pScore >= scoreNormal) {
          comment = normal;
        } else {
          comment = poor;
        }

        return comment;
      }

      GetComment(score100).Dump(nameof(GetComment) + " " + score100);
      GetComment(score95).Dump(nameof(GetComment) + " " + score95);
      GetComment(score80).Dump(nameof(GetComment) + " " + score80);
      GetComment(score65).Dump(nameof(GetComment) + " " + score65);
      GetComment(score50).Dump(nameof(GetComment) + " " + score50);

      //三元Lambda
      string GetComment_三元_Lambda(int pScore) => pScore >= scorePro ? pro : pScore >= scoreGood ? good : pScore >= scoreNormal ? normal : poor;

      GetComment_三元_Lambda(score100).Dump(nameof(GetComment_三元_Lambda) + " " + score100);
      GetComment_三元_Lambda(score95).Dump(nameof(GetComment_三元_Lambda) + " " + score95);
      GetComment_三元_Lambda(score80).Dump(nameof(GetComment_三元_Lambda) + " " + score80);
      GetComment_三元_Lambda(score65).Dump(nameof(GetComment_三元_Lambda) + " " + score65);
      GetComment_三元_Lambda(score50).Dump(nameof(GetComment_三元_Lambda) + " " + score50);

      string GetComment_三元_Lambda_排版(int pScore) =>
        pScore >= scorePro ? pro :
        pScore >= scoreGood ? good :
        pScore >= scoreNormal ? normal :
        poor;

      GetComment_三元_Lambda_排版(score100).Dump(nameof(GetComment_三元_Lambda_排版) + " " + score100);
      GetComment_三元_Lambda_排版(score95).Dump(nameof(GetComment_三元_Lambda_排版) + " " + score95);
      GetComment_三元_Lambda_排版(score80).Dump(nameof(GetComment_三元_Lambda_排版) + " " + score80);
      GetComment_三元_Lambda_排版(score65).Dump(nameof(GetComment_三元_Lambda_排版) + " " + score65);
      GetComment_三元_Lambda_排版(score50).Dump(nameof(GetComment_三元_Lambda_排版) + " " + score50);

      string GetComment_三元_Lambda_排版2(int pScore) =>
        pScore >= scorePro ?
          pScore == 100 ? "你是神" :
          pro :
        pScore >= scoreGood ? good :
        pScore >= scoreNormal ? normal :
        poor;

      GetComment_三元_Lambda_排版2(score100).Dump(nameof(GetComment_三元_Lambda_排版2) + " " + score100);
      GetComment_三元_Lambda_排版2(score95).Dump(nameof(GetComment_三元_Lambda_排版2) + " " + score95);
      GetComment_三元_Lambda_排版2(score80).Dump(nameof(GetComment_三元_Lambda_排版2) + " " + score80);
      GetComment_三元_Lambda_排版2(score65).Dump(nameof(GetComment_三元_Lambda_排版2) + " " + score65);
      GetComment_三元_Lambda_排版2(score50).Dump(nameof(GetComment_三元_Lambda_排版2) + " " + score50);
    }
  }
}
