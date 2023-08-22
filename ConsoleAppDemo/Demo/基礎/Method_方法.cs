using MyLibrary.Extensions;

namespace ConsoleAppDemo.Demo {
  internal class Method_方法 : DemoBase {
    public override void Run() {
      //輸入西元年回傳民國年
      #region -- 一般方法 --
      int GetRocYear(int pADyear) => pADyear - 1911;

      int year2000 = 2000;

      int year2000ToROC = GetRocYear(year2000);
      year2000ToROC.Dump(nameof(year2000ToROC));
      #endregion



      #region -- 回傳多個值
      #region -- 使用 out --
      int GetRocYearByOut(int pADyear, out string pROCYearDescription) {
        int rocYear = pADyear - 1911;
        pROCYearDescription = $"西元 {pADyear} 年 => 民國 {rocYear} 年";
        return rocYear;
      }

      int year2001 = 2001;
      string year2001Desc = string.Empty;

      int year2001ToROC = GetRocYearByOut(year2001, out year2001Desc);
      year2001ToROC.Dump(nameof(year2001ToROC));
      year2001Desc.Dump(nameof(year2001Desc));

      #region -- C# 7 開始可在呼叫時才宣告 out 變數 --
      int year2002 = 2002;

      int year2002ToROC = GetRocYearByOut(year2002, out string year2002Desc);
      year2002ToROC.Dump(nameof(year2002ToROC));
      year2002Desc.Dump(nameof(year2002Desc));
      #endregion
      #endregion

      #region -- 使用 Tuple --
      Tuple<int, string> GetRocYearByTuple(int pADyear) =>
        Tuple.Create(pADyear - 1911, $"西元 {pADyear} 年 => 民國 {pADyear - 1911} 年");

      int year2003 = 2003;
      var data2003 = GetRocYearByTuple(year2003);
      data2003.Item1.Dump(nameof(data2003.Item1));
      data2003.Item2.Dump(nameof(data2003.Item2));

      #region -- c# 7 開始 --
      (int ROCyear, string ROCdescription) GetRocYearByTupleNew(int pADyear) =>
        (pADyear - 1911, $"西元 {pADyear} 年 => 民國 {pADyear - 1911} 年");

      int year2004 = 2004;

      var data2004 = GetRocYearByTupleNew(year2004);
      data2004.Item1.Dump(nameof(data2004.Item1));
      data2004.Item2.Dump(nameof(data2004.Item2));
      data2004.ROCyear.Dump(nameof(data2004.ROCyear));
      data2004.ROCdescription.Dump(nameof(data2004.ROCdescription));
      #endregion
      #endregion

      #region -- 使用自訂類別 --

      Roc GetRocYearByClass(int pADyear) {
        var myClass = new Roc();
        myClass.RocYear = pADyear - 1911;
        myClass.RocDescription = $"西元 {pADyear} 年 => 民國 {myClass.RocYear} 年";

        return myClass;
      }

      int year2005 = 2005;
      var data2005 = GetRocYearByClass(year2005);

      int year2005ToROC = data2005.RocYear;
      string year2005Desc = data2005.RocDescription;
      year2005ToROC.Dump(nameof(year2005ToROC));
      year2005Desc.Dump(nameof(year2005Desc));
      #endregion

      #region -- 使用動態類別 --
      dynamic GetRocYearByDynamic(int pADyear) {
        return new { RocYear = pADyear - 1911, RocDescription = $"西元 {pADyear} 年 => 民國 {pADyear - 1911} 年" };
      }

      int year2006 = 2006;
      var data2006 = GetRocYearByDynamic(year2006);

      int year2006ToRoc = data2006.RocYear;
      string year2006Desc = data2006.RocDescription;
      year2006ToRoc.Dump(nameof(year2006ToRoc));
      year2006Desc.Dump(nameof(year2005Desc));
      #endregion
      #endregion
    }

    /// <summary>
    /// 民國年物件
    /// </summary>
    public class Roc {
      /// <summary>
      /// 民國年
      /// </summary>
      public int RocYear { get; set; }
      /// <summary>
      /// 民國年描述
      /// </summary>
      public string RocDescription { get; set; }
    }
  }
}