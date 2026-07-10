using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DietTracker.ViewModels
{
    public partial class DayDetailViewModel : ViewModelBase
    {
        // Il giorno che stiamo modificando
        public DateOnly Date { get; }

        // Stato temporaneo modificabile nel popup (non ancora salvato)
        [ObservableProperty]
        private bool _isCompliant;

        // Callback chiamato con (data, nuovoValore) quando l'utente conferma
        private readonly Func<DateOnly, bool, Task> _onConfirmed;

        // Callback chiamato quando l'utente annulla, senza salvare nulla
        private readonly Action _onCancelled;

        public DayDetailViewModel(DateOnly date, bool? currentStatus, Func<DateOnly, bool, Task> onConfirmed, Action onCancelled)
        {
            Date = date;
            // Se il giorno non era ancora compilato, partiamo da "true" come default
            _isCompliant = currentStatus ?? true;
            _onConfirmed = onConfirmed;
            _onCancelled = onCancelled;
        }

        [RelayCommand]
        private void Toggle()
        {
            IsCompliant = !IsCompliant;
        }

        [RelayCommand]
        private async Task Confirm()
        {
            await _onConfirmed(Date, IsCompliant);
        }

        [RelayCommand]
        private void Cancel()
        {
            _onCancelled();
        }
    }
}