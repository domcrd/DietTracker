using Avalonia.Controls;
using Avalonia.Input;
using DietTracker.ViewModels;

namespace DietTracker.Views
{
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        private void OnDetailBackgroundPressed(object? sender, PointerPressedEventArgs e)
        {
            if (DataContext is CalendarViewModel calendarVm &&
                calendarVm.SelectedDayDetail is DayDetailViewModel detailVm)
            {
                detailVm.ToggleCommand.Execute(null);
            }
        }
    }
}