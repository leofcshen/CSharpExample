using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WebDemo.Models;

namespace WebDemo.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AppSettings _appSettings;

		public HomeController(
			ILogger<HomeController> logger,
			IHttpContextAccessor httpContextAccessor,
			IOptions<AppSettings> options) {
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
			_appSettings = options.Value;
		}

		public IActionResult Index() {
			#region -- 測試 Session --
			string name = "Tom";

			var se1 = new MySession(_httpContextAccessor);
			se1.Name = "Tom";
			var sessionName1 = se1.Name;
			Debug.Assert(sessionName1 == name);

			var se2 = new MySession(_httpContextAccessor);
			var sessionName2 = se2.Name;
			Debug.Assert(sessionName2 == name);
			#endregion

			#region -- 測試 config --
			var a = _appSettings.Url;
			_appSettings.Url = "a";
			var cc = _appSettings.Url;
			var b = _appSettings.Username;
			var c = _appSettings.Password;
			var d = _appSettings.Age;
			//var e = AppSettingsNew.Age;
			#endregion

			return View();
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
