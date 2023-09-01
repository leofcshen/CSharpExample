using System.Web.Http;

namespace ApiFramework {
  public static class WebApiConfig {
    public static void Register(HttpConfiguration config) {
      // Web API 設定和服務

      // Web API 路由
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
        name: "AccountApi",
        routeTemplate: "api/{controller}/{action}/{id}",
        defaults: new { id = RouteParameter.Optional },
        constraints: new { controller = "Account" }
      );
    }
  }
}
