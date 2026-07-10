using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DietTracker.Services;

namespace DietTracker.ViewModels
{
    public partial class CheckInViewModel : ViewModelBase
    {
        private readonly DietDataService _dataService;

        // Chiamato quando l'utente conferma, per dire al MainViewModel
        // di passare alla schermata calendario
        private readonly Action _onConfirmed;

        // Stato attuale mostrato a schermo: true = verde, false = rosso.
        // Di default true, come richiesto ("schermata verde" al primo avvio)
        [ObservableProperty]
        private bool _isCompliant = true;

        public CheckInViewModel(DietDataService dataService, Action onConfirmed)
        {
            _dataService = dataService;
            _onConfirmed = onConfirmed;
        }

        // Comando collegato al tap sulla schermata: inverte il colore
        [RelayCommand]
        private void Toggle()
        {
            IsCompliant = !IsCompliant;
        }

        // Comando collegato al pulsante "Conferma"
        [RelayCommand]
        private async Task Confirm()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            await _dataService.SetDayAsync(today, IsCompliant);
            _onConfirmed();
        }
    }
}