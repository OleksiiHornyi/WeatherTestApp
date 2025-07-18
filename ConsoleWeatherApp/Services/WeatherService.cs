using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string cachedCity = "";
        private string cachedResponse = "";
        private DateTime lastFetch = DateTime.MinValue;

        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                city = ""; // для авто
            }

            if (cachedCity == city && (DateTime.Now - lastFetch).TotalSeconds < 60)
            {
                return ParseWeather(cachedResponse);
            }

            string url = string.IsNullOrEmpty(city)
                ? "https://wttr.in/?format=j1"
                : $"https://wttr.in/{city}?format=j1";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Помилка HTTP: {response.StatusCode}");
                    return null;
                }

                string content = await response.Content.ReadAsStringAsync();
                cachedCity = city;
                cachedResponse = content;
                lastFetch = DateTime.Now;

                return ParseWeather(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return null;
            }
        }

        private WeatherInfo ParseWeather(string json)
        {
            try
            {
                JsonDocument document = JsonDocument.Parse(json);
                JsonElement current = document.RootElement.GetProperty("current_condition")[0];
                string city = document.RootElement.GetProperty("nearest_area")[0].GetProperty("areaName")[0].GetProperty("value").GetString();
                string weatherDesc = current.GetProperty("weatherDesc")[0].GetProperty("value").GetString();
                string temperature = current.GetProperty("temp_C").GetString();
                return new WeatherInfo
                {
                    City = city,
                    TemperatureC = temperature,
                    Description = weatherDesc,
                    LocalizedDescription = LocalizationService.Translate(weatherDesc)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка обробки JSON: {ex.Message}");
                return null;
            }
        }
    }
}

