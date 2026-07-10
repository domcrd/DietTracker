using System;

namespace DietTracker.Models
{
    // Rappresenta una singola cella nella griglia del calendario
    public class CalendarDayCell
    {
        public DateOnly Date { get; }

        // true/false = stato del giorno, null = non ancora compilato
        public bool? Status { get; }

        // false per i giorni "di riempimento" prima del 1° del mese
        // (servono solo ad allineare la griglia sotto i nomi dei giorni della settimana)
        public bool IsInCurrentMonth { get; }

        public CalendarDayCell(DateOnly date, bool? status, bool isInCurrentMonth)
        {
            Date = date;
            Status = status;
            IsInCurrentMonth = isInCurrentMonth;
        }
    }
}