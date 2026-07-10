using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietTracker.Services
{
    // Gestisce i dati dell'app: stato dei giorni + salvataggio,
    // delegando il "dove" salvare a IEntriesStorage (file, browser, ecc.)
    public class DietDataService
    {
        private readonly IEntriesStorage _storage;
        private Dictionary<DateOnly, bool> _entries = new();

        public DietDataService(IEntriesStorage storage)
        {
            _storage = storage;
        }

        // Va chiamato una volta sola, all'avvio, per caricare i dati salvati
        public async Task InitializeAsync()
        {
            var loaded = await _storage.LoadAsync();
            if (loaded != null)
                _entries = loaded;
        }

        public bool IsTodayCompiled()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return _entries.ContainsKey(today);
        }

        public async Task SetDayAsync(DateOnly date, bool compliant)
        {
            _entries[date] = compliant;
            await _storage.SaveAsync(_entries);
        }

        public bool? GetDay(DateOnly date)
        {
            if (_entries.TryGetValue(date, out var value))
                return value;
            return null;
        }

        public IReadOnlyDictionary<DateOnly, bool> GetAllEntries()
        {
            return _entries;
        }

        public async Task RemoveDayAsync(DateOnly date)
        {
            _entries.Remove(date);
            await _storage.SaveAsync(_entries);
        }
    }
}
