using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DietTracker.Services
{
    // Contesto di serializzazione JSON "source-generated": il compilatore
    // genera in anticipo il codice per leggere/scrivere questo tipo esatto,
    // senza bisogno di reflection a runtime. Necessario per funzionare
    // correttamente su WebAssembly (dove la reflection è disabilitata).
    [JsonSerializable(typeof(Dictionary<DateOnly, bool>))]
    public partial class DietDataJsonContext : JsonSerializerContext
    {
    }
}