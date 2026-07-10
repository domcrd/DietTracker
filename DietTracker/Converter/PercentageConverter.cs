using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DietTracker.Converters
{
    // Calcola una percentuale di un valore numerico (es. larghezza finestra).
    // Il parametro passato in XAML è la percentuale desiderata (es. 0.7 = 70%)
    public class PercentageConverter : IValueConverter
    {
        public static readonly PercentageConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double size && parameter is string percentText &&
                double.TryParse(percentText, NumberStyles.Any, CultureInfo.InvariantCulture, out var percent))
            {
                return size * percent;
            }

            return 0.0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}