using System.Text.Json;

namespace Strategy.Helpers;

internal static class SerializationHelper
{
    private static readonly JsonSerializerOptions s_options = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static string Serialize(this object source) => JsonSerializer.Serialize(source, s_options);
}