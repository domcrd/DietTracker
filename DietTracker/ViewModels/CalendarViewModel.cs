using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DietTracker.Models;
using DietTracker.Services;

namespace DietTracker.ViewModels
{
    public partial class CalendarViewModel : ViewModelBase
    {
        private readonly DietDataService _dataService;
        private readonly Action _onTodayReset;

        [ObservableProperty]
        private DateOnly _currentMonth;

        [ObservableProperty]
        private string _monthLabel = string.Empty;

        // NOTA: proprietà usata solo dal popup di conferma, attualmente disattivato.
        // Lasciata qui (invece che eliminata) per poterlo riattivare in futuro
        // senza dover ricostruire tutto da zero.
        [ObservableProperty]
        private DayDetailViewModel? _selectedDayDetail;

        public ObservableCollection<CalendarDayCell> Days { get; } = new();

        public CalendarViewModel(DietDataService dataService, Action onTodayReset)
        {
            _dataService = dataService;
            _onTodayReset = onTodayReset;

            var today = DateOnly.FromDateTime(DateTime.Now);
            CurrentMonth = new DateOnly(today.Year, today.Month, 1);

            BuildCalendar();
        }

        [RelayCommand]
        private void NextMonth()
        {
            CurrentMonth = CurrentMonth.AddMonths(1);
            BuildCalendar();
        }

        [RelayCommand]
        private void PreviousMonth()
        {
            CurrentMonth = CurrentMonth.AddMonths(-1);
            BuildCalendar();
        }

        // ===== COMPORTAMENTO ATTIVO: toggle diretto al click, senza popup =====
        [RelayCommand]
        private async Task SelectDay(CalendarDayCell cell)
        {
            if (!cell.IsInCurrentMonth)
                return;

            // Ciclo a 3 stati: null (grigio) -> true (verde) -> false (rosso) -> null (grigio) -> ...
            if (cell.Status == null)
            {
                await _dataService.SetDayAsync(cell.Date, true);
            }
            else if (cell.Status == true)
            {
                await _dataService.SetDayAsync(cell.Date, false);
            }
            else
            {
                await _dataService.RemoveDayAsync(cell.Date);
            }

            BuildCalendar();
        }

        // ===== COMPORTAMENTO DISATTIVATO: apertura popup di conferma =====
        // Per riattivarlo: commenta il metodo SelectDay sopra e scommenta questo,
        // poi riattiva anche il blocco overlay in CalendarView.axaml
        //
        // [RelayCommand]
        // private void SelectDay(CalendarDayCell cell)
        // {
        //     if (!cell.IsInCurrentMonth)
        //         return;
        //
        //     SelectedDayDetail = new DayDetailViewModel(
        //         cell.Date,
        //         cell.Status,
        //         onConfirmed: OnDayDetailConfirmedAsync,
        //         onCancelled: OnDayDetailCancelled);
        // }
        //
        // private async Task OnDayDetailConfirmedAsync(DateOnly date, bool newValue)
        // {
        //     await _dataService.SetDayAsync(date, newValue);
        //     SelectedDayDetail = null;
        //     BuildCalendar();
        // }
        //
        // private void OnDayDetailCancelled()
        // {
        //     SelectedDayDetail = null;
        // }

        [RelayCommand]
        private async Task ResetToday()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            await _dataService.RemoveDayAsync(today);
            _onTodayReset();
        }

        private void BuildCalendar()
        {
            Days.Clear();
            MonthLabel = CurrentMonth.ToString("MMMM yyyy").ToUpper();

            var firstDayOfMonth = CurrentMonth;
            var daysInMonth = DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);
            int firstDayOffset = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

            for (int i = 0; i < firstDayOffset; i++)
            {
                var fillerDate = firstDayOfMonth.AddDays(i - firstDayOffset);
                Days.Add(new CalendarDayCell(fillerDate, null, isInCurrentMonth: false));
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateOnly(CurrentMonth.Year, CurrentMonth.Month, day);
                var status = _dataService.GetDay(date);
                Days.Add(new CalendarDayCell(date, status, isInCurrentMonth: true));
            }
        }
    }
}