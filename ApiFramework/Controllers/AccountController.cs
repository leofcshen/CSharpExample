using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiFramework.Controllers {
  public class AccountController : ApiController {
    public IHttpActionResult Get(string a) {
      if (HttpContext.Current.Request.ContentType != "application/json") {
        return BadRequest("錯誤 ContentType 需為 application/json");
      }
      return Ok($"Get {a}");
    }

    public IHttpActionResult Post(string a) {
      if (Request.Content.Headers.ContentType?.MediaType != "application/json") {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return Ok($"Post {a}");
    }
  }
}
