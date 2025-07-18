using System.Collections.Generic;

namespace ConsoleApp.Services
{
    public static class LocalizationService
    {
        private static readonly Dictionary<string, string> Translations = new Dictionary<string, string>()
        {
            { "Partly cloudy", "Мінлива хмарність" },
            { "Sunny", "Сонячно" },
            { "Clear", "Ясно" },
            { "Light rain", "Невеликий дощ" },
            { "Heavy rain", "Сильний дощ" },
            { "Overcast", "Похмуро" },
            { "Light drizzle", "Мряка"},
            { "Moderate rain", "Помірний дощ"},
            { "Light rain shower", "Невелика злива"},
            { "Patchy light drizzle", "Місцями мряка"},
            { "Mist", "Невеликий туман"}
        };

        public static string Translate(string original)
        {
            return Translations.TryGetValue(original, out var result) ? result : original;
        }
    }
}