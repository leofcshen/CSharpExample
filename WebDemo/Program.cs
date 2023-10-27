using WebDemo.Models;

namespace WebDemo {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// 新增 Session 服務
			builder.Services.AddSession();
			builder.Services.AddHttpContextAccessor();
			//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// 取 AppSettings 基本方法
			builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

			// 取 AppSettings 使用靜態物件
			//var provider = builder.Services.BuildServiceProvider();
			//var configuration = provider.GetService<IConfiguration>();
			//AppSettingsNew.Age = configuration.GetValue<int>($"{nameof(AppSettingsNew)}:{nameof(AppSettingsNew.Age)}");

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

			// 啟用 Session
			app.UseSession();

			app.Run();
		}
	}
}
