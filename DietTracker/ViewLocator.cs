using Avalonia.Controls;
using Avalonia.Controls.Templates;
using DietTracker.ViewModels;
using DietTracker.Views;
using System;

namespace DietTracker
{
    // Mappa esplicita ViewModel -> View, invece di reflection.
    // Necessario per funzionare correttamente su WebAssembly, dove il
    // trimming del codice rimuoverebbe le View se cercate solo per nome.
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            return param switch
            {
                CheckInViewModel => new CheckInView(),
                CalendarViewModel => new CalendarView(),
                MainViewModel => new MainView(),
                _ => new TextBlock { Text = "Not Found: " + param?.GetType().Name }
            };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}