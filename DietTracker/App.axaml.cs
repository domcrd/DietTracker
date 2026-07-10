using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DietTracker.Services;
using DietTracker.ViewModels;
using DietTracker.Views;

namespace DietTracker
{
    public partial class App : Application
    {
        // Ogni piattaforma (Desktop, Browser...) imposta questo valore
        // PRIMA che l'app parta, così sappiamo quale storage usare
        public static IEntriesStorage? Storage { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var dataService = new DietDataService(Storage!);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(dataService)
                };
            }
            else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
            {
                singleViewFactoryApplicationLifetime.MainViewFactory = () => new MainView { DataContext = new MainViewModel(dataService) };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel(dataService)
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}