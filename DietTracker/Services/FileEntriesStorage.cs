using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DietTracker.Services
{
    // Implementazione per Desktop: legge/scrive un file JSON locale
    public class FileEntriesStorage : IEntriesStorage
    {
        private string GetFilePath()
        {
            var folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DietTracker");

            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "entries.json");
        }

        public Task<Dictionary<DateOnly, bool>?> LoadAsync()
        {
            var path = GetFilePath();
            if (!File.Exists(path))
                return Task.FromResult<Dictionary<DateOnly, bool>?>(null);

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize(json, DietDataJsonContext.Default.DictionaryDateOnlyBoolean);
            return Task.FromResult(loaded);
        }

        public Task SaveAsync(Dictionary<DateOnly, bool> entries)
        {
            var path = GetFilePath();
            var json = JsonSerializer.Serialize(entries, DietDataJsonContext.Default.DictionaryDateOnlyBoolean);
            File.WriteAllText(path, json);
            return Task.CompletedTask;
        }
    }
}