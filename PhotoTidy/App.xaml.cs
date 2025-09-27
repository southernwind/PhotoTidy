using System.Reflection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace PhotoTidy;

public partial class App {
	private static Window? _mainWindow;

	public static Window MainWindow {
		get {
			return _mainWindow ??= Ioc.Default.GetRequiredService<MainWindow>();
		}
	}

	public App() {
		this.InitializeComponent();
	}

	protected override void OnLaunched(LaunchActivatedEventArgs args) {
		Build();

		MainWindow.Activate();
	}

	private static void Build() {
		var serviceCollection = new ServiceCollection();
		var targetTypes = Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(x =>
				x.GetCustomAttributes<AddTransientAttribute>(inherit: true).Any());

		foreach (var targetType in targetTypes) {
			var attribute = targetType.GetCustomAttribute<AddTransientAttribute>();
			serviceCollection.AddTransient(attribute?.ServiceType ?? targetType, targetType);
		}

		var singletonTargetTypes = Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(x =>
				x.GetCustomAttributes<AddSingletonAttribute>(inherit: true).Any());

		foreach (var singletonTargetType in singletonTargetTypes) {
			serviceCollection.AddSingleton(singletonTargetType);
		}

		Ioc.Default.ConfigureServices(
			serviceCollection.BuildServiceProvider()
		);
	}
}