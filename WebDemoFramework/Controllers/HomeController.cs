using System.Web.Mvc;
using SH = WebDemoFramework.Models.SessionHelper;

namespace WebDemoFramework.Controllers {
  public class HomeController : Controller {
    public static int Number = 0;
    public ActionResult Index() {
      SH.Count ??= 0;
      SH.Count++;
      return View();
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