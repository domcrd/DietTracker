using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DietTracker.Converters
{
    // Calcola la dimensione di una cella quadrata a partire dalla
    // larghezza disponibile, dividendo per il numero di colonne (7)
    // e sottraendo i margini della griglia e delle celle stesse
    public class CellSizeConverter : IValueConverter
    {
        public static readonly CellSizeConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                // 40 = margini della Grid del calendario (20 a sinistra + 20 a destra)
                // 14 = margini delle 7 celle (2px ciascuna: 1 a sinistra + 1 a destra)
                var usableWidth = width - 40 - 14;
                var cellSize = usableWidth / 7;

                //non superare mai 70px per lato, anche su schermi molto larghi
                const double maxCellSize = 150;
                if (cellSize > maxCellSize)
                    cellSize = maxCellSize;

                return cellSize > 0 ? cellSize : 0;
            }

            return 0.0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}