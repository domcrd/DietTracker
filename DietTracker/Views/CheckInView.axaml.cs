using Avalonia.Controls;
using Avalonia.Input;
using DietTracker.ViewModels;

namespace DietTracker.Views
{
    public partial class CheckInView : UserControl
    {
        public CheckInView()
        {
            InitializeComponent();
        }

        // Gestisce il click/tap su qualsiasi punto dello sfondo
        private void OnBackgroundPressed(object? sender, PointerPressedEventArgs e)
        {
            // Se ho cliccato sul pulsante "Conferma", non voglio anche
            // invertire il colore: esco subito senza fare nulla
            if (e.Source is Button)
                return;

            if (DataContext is CheckInViewModel vm)
            {
                vm.ToggleCommand.Execute(null);
            }
        }
    }
}