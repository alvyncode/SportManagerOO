using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SportManager.Data;
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
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
