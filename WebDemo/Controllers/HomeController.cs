using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDemo.Models;

namespace WebDemo.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(
			ILogger<HomeController> logger,
			IHttpContextAccessor httpContextAccessor) {
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index() {
			var se = new MySession(_httpContextAccessor);
			se.Name = "Tom";
			var name = se.Name;
			Debug.Assert(name == "Tom");

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
