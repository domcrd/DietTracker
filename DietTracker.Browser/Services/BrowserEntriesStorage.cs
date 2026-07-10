using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Threading.Tasks;
using DietTracker.Services;

namespace DietTracker.Browser.Services
{
    // Implementazione per il browser: legge/scrive nel localStorage
    // tramite JS Interop (chiamando il file storage.js)
    [SupportedOSPlatform("browser")]
    public partial class BrowserEntriesStorage : IEntriesStorage
    {
        private const string StorageKey = "diet-tracker-entries";
        private static bool _moduleLoaded;

        // Queste due funzioni "collegano" i metodi C# alle funzioni
        // JavaScript definite in storage.js
        [JSImport("getItem", "storage")]
        private static partial string? GetItem(string key);

        [JSImport("setItem", "storage")]
        private static partial void SetItem(string key, string value);

        private static async Task EnsureModuleLoadedAsync()
        {
            if (_moduleLoaded)
                return;

            await JSHost.ImportAsync("storage", "./js/storage.js");
            _moduleLoaded = true;
        }

        public async Task<Dictionary<DateOnly, bool>?> LoadAsync()
        {
            await EnsureModuleLoadedAsync();

            var json = GetItem(StorageKey);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize(json, DietDataJsonContext.Default.DictionaryDateOnlyBoolean);
        }

        public async Task SaveAsync(Dictionary<DateOnly, bool> entries)
        {
            await EnsureModuleLoadedAsync();

            var json = JsonSerializer.Serialize(entries, DietDataJsonContext.Default.DictionaryDateOnlyBoolean);
            SetItem(StorageKey, json);
        }
    }
}