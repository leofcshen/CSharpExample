using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using SH = WebDemoFramework.Models.SessionHelper;

namespace WebDemoFramework.Controllers {
  public class HomeController : Controller {
    public static int 總瀏覽次數 = 0;

    public ActionResult Index() {
      總瀏覽次數++;

      //取 session 值
      var sessionValue = Session["我的瀏覽次數"];
      if (sessionValue is null) {
        //存 session 值
        Session["我的瀏覽次數"] = 0;
      }

      SH.我的瀏覽次數++;

      return View(總瀏覽次數);
    }

    private static readonly Dictionary<string, string> SpecialDomain = new Dictionary<string, string>() {
      { "Tom 股份無限公司", "tom.myproduct.com" },
      { "John 股份有限公司", "john.myproduct.com" },
    };

    private static ReadOnlyDictionary<string, string> SpecialDomain1 = new ReadOnlyDictionary<string, string>(SpecialDomain);

    private static ReadOnlyDictionary<string, string> SpecialDomain2 = new ReadOnlyDictionary<string, string>(SpecialDomain);

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      var test = new Dictionary<string, string>() {
        { "Tom 股份無限公司", "tom.myproduct.com" },
        { "John 股份有限公司", "john.myproduct.com" },
      };

      
      var domain = Request.Url.Host;
      
      string companyName = SpecialDomain.FirstOrDefault(x => x.Value == domain).Key;
      companyName ??= "一般訪客";

      ViewBag.GreetMessage = companyName + " 你好。";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}