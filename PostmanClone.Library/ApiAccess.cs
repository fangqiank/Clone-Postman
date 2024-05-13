using System.Text.Json;

namespace PostmanClone.Library
{
    public class ApiAccess : IApiAccess
    {
        private readonly HttpClient _httpClient = new();
        public async Task<string> CallApi(
            string url,
            bool formatOutput = true,
            HttpAction action = HttpAction.GET
            )
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                if (formatOutput)
                {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
                    json = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    });
                }


                return json;
            }

            return $"Error: {response.StatusCode}";
        }

        public bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            bool output = Uri.TryCreate(url, uriKind: UriKind.Absolute, out Uri uriOutput) &&
                (uriOutput.Scheme == Uri.UriSchemeHttps);

            return output;
        }
    }
}
