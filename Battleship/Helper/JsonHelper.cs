namespace Battleship.Helper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Model;

    public class JsonHelper
    {
        static public List<BoardShipConfig> LoadShips(string url)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
                };
                var boardShipConfigJson = File.ReadAllText(url);
                return JsonSerializer.Deserialize<List<BoardShipConfig>>(boardShipConfigJson, options);
            }
            catch (System.Exception)
            {
                //log error
                throw;
            }
        }
    }
}