using System.Web.Http;

namespace ApiFramework.Controllers {
  public class AccountController : ApiController {
    //[HttpPost]
    //public string Get(string a) => $"Get {a}";


    //public string Post(string a) => $"Post {a}";

    [HttpPost]
    public string MyApi(string a) => "MyApi";

  }
}
