using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SportManager.Data;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace SportManager;

public static class MauiProgram
{

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		var connectionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
		builder.Services.AddDbContext<SportManagerDBContext>(options =>
			options.UseMySql(
				connectionString, 
				ServerVersion.AutoDetect(connectionString)
			)
		);
		
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("IstokWeb-Bold.ttf", "IstokWeb-Bold");
				fonts.AddFont("IstokWeb-Regular.ttf", "IstokWeb-Regular");
			});
		builder
    		.UseMauiApp<App>()
    		.UseSkiaSharp();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
