using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DietTracker.Services;

namespace DietTracker.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly DietDataService _dataService;

        // Nullable: finché è null, la UI non mostra ancora nulla
        // (siamo ancora in fase di caricamento dati)
        [ObservableProperty]
        private ViewModelBase? _currentViewModel;

        public MainViewModel(DietDataService dataService)
        {
            _dataService = dataService;

            // Il costruttore non può essere "async", quindi lanciamo
            // l'inizializzazione e la lasciamo girare in background.
            // Il carattere "_" indica che scartiamo volutamente il Task
            // restituito (pattern comune per "fire and forget" controllato).
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await _dataService.InitializeAsync();

            if (_dataService.IsTodayCompiled())
                CurrentViewModel = new CalendarViewModel(_dataService, ShowCheckIn);
            else
                CurrentViewModel = new CheckInViewModel(_dataService, ShowCalendar);
        }

        private void ShowCalendar()
        {
            CurrentViewModel = new CalendarViewModel(_dataService, ShowCheckIn);
        }

        private void ShowCheckIn()
        {
            CurrentViewModel = new CheckInViewModel(_dataService, ShowCalendar);
        }
    }
}