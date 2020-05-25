using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace darker.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SettingsThemeMode
    {
        Both = 1,
        OnlySystem,
        OnlyApps
    }
}
