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

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}