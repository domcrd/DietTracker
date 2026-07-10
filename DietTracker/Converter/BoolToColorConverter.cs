using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace DietTracker.Converters
{
    // Traduce true/false in un colore per la UI
    public class BoolToColorConverter : IValueConverter
    {
        // Un'unica istanza condivisa, così in XAML possiamo
        // riferirci a questa classe senza doverla dichiarare come risorsa
        public static readonly BoolToColorConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isCompliant)
            {
                return isCompliant
                    ? new SolidColorBrush(Colors.MediumSeaGreen)
                    : new SolidColorBrush(Colors.IndianRed);
            }

            return new SolidColorBrush(Colors.WhiteSmoke);
        }

        // Non ci serve la conversione inversa (da colore a bool), quindi lanciamo un'eccezione se mai venisse chiamata
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}