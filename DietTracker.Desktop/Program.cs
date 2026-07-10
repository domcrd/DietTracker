using Avalonia;
using DietTracker;
using DietTracker.Services;
using System;

namespace DietTracker.Desktop
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App.Storage = new FileEntriesStorage();

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
#if DEBUG
                .WithDeveloperTools()
#endif
                .WithInterFont()
                .LogToTrace();
    }
}