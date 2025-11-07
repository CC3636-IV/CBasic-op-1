using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class WeatherService
{

        // Weatherstack API 

    private readonly HttpClient _http = new HttpClient();

    public async Task<decimal?> GetTemperatureCelsiusAsync(string city, string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException("API key required", nameof(apiKey));
        
        
        var url = $"http://api.weatherstack.com/current?access_key={apiKey}&query={Uri.EscapeDataString(city)}";
        var resp = await _http.GetAsync(url);
        
        if (!resp.IsSuccessStatusCode) return null;

        using var stream = await resp.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        
        if (doc.RootElement.TryGetProperty("current", out var current) &&
            current.TryGetProperty("temperature", out var tempElement))
        {
            return tempElement.GetDecimal();
        }

        return null;
    }
}