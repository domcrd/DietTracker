using Avalonia;
using Avalonia.Browser;
using DietTracker;
using DietTracker.Browser.Services;
using System.Runtime.Versioning;
using System.Threading.Tasks;

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        App.Storage = new BrowserEntriesStorage();

        return BuildAvaloniaApp()
            .WithInterFont()
            .WithInterFont()
#if DEBUG
            .WithDeveloperTools()
#endif
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}