using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SportManager.Data;
namespace SportManager;

public partial class App : Application
{ 
	public App(SportManagerDBContext context)
	{
		context.Database.EnsureCreated();
		InitializeComponent();
	}
	
	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}