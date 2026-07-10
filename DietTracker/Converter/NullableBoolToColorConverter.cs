using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace DietTracker.Converters
{
    // Come BoolToColorConverter, ma gestisce anche il caso null (grigio)
    public class NullableBoolToColorConverter : IValueConverter
    {
        public static readonly NullableBoolToColorConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                true => new SolidColorBrush(Colors.MediumSeaGreen),
                false => new SolidColorBrush(Colors.IndianRed),
                _ => new SolidColorBrush(Colors.WhiteSmoke)
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}