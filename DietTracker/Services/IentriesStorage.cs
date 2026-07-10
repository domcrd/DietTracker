using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietTracker.Services
{
    // Astrae il "dove" salviamo/leggiamo i dati, in modo che
    // ogni piattaforma (Desktop, Browser, futuro Android/iOS)
    // possa fornire la propria implementazione concreta.
    public interface IEntriesStorage
    {
        Task<Dictionary<DateOnly, bool>?> LoadAsync();
        Task SaveAsync(Dictionary<DateOnly, bool> entries);
    }
}