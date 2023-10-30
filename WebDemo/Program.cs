using WebDemo.Models;

namespace WebDemo {
	public class Program {
		private static WebApplicationBuilder? _appBuilder;
		private static WebApplicationBuilder AppBuilder { get => _appBuilder ?? throw new NullReferenceException($"{nameof(Program)}.{nameof(_appBuilder)}������null"); }

		public static void Main(string[] args) {
			var builder = _appBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// �s�W Session �A��
			builder.Services.AddSession();
			builder.Services.AddHttpContextAccessor();
			//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// �� AppSettings �򥻤�k
			builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

			// �� AppSettings �ϥ��R�A����
			//var provider = builder.Services.BuildServiceProvider();
			//var configuration = provider.GetService<IConfiguration>();
			///AppSettingsNew.Age = configuration.GetValue<int>($"{nameof(AppSettingsNew)}:{nameof(AppSettingsNew.Age)}");

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

			// �ҥ� Session
			app.UseSession();

			app.Run();
		}

		internal static T ConfigMapping<T>()
			=> AppBuilder.Configuration.Get<T>();
	}
}
