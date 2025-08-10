using System.Text.Json.Serialization;

namespace Hotmail_Get_Oauth2
{
    public class Setting
    {
        public string PathApp { get; } = AppDomain.CurrentDomain.BaseDirectory;
    }
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Setting))]
    internal partial class SourceGenerationContextSetting : JsonSerializerContext
    {
    }
}
