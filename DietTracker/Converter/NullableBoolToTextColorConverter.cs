using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace DietTracker.Converters
{
    // Sceglie il colore del testo in base allo stato del giorno,
    // per restare leggibile sia su sfondo colorato che su grigio chiaro
    public class NullableBoolToTextColorConverter : IValueConverter
    {
        public static readonly NullableBoolToTextColorConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                true => new SolidColorBrush(Colors.White),
                false => new SolidColorBrush(Colors.White),
                _ => new SolidColorBrush(Colors.DimGray) // testo scuro sul grigio chiaro
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}