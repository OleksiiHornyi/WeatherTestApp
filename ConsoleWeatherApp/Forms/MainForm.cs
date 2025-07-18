using ConsoleApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsoleApp.Forms
{
    public partial class Forms : Form
    {
        private readonly WeatherService weatherService = new WeatherService();
        private readonly Timer timer = new Timer();

        public Forms()
        {
            InitializeComponent();
            timer.Interval = 60000; // 60 секунд
            timer.Tick += async (s, e) => await LoadWeatherAsync();
            timer.Start();
        }

        private async void btnGetWeather_Click(object sender, EventArgs e)
        {
            await LoadWeatherAsync();
        }

        private async Task LoadWeatherAsync()
        {
            string city = txtCity.Text.Trim();
            var weather = await weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                lblStatus.Text = "Помилка отримання даних";
                return;
            }

            lblCity.Text = $"Місто: {weather.City}";
            lblTemp.Text = $"Температура: {weather.TemperatureC}°C";
            lblDesc.Text = $"Стан: {weather.LocalizedDescription}";
            lblIcon.Text = GetWeatherIcon(weather.LocalizedDescription);
            lblStatus.Text = $"Оновлено: {DateTime.Now:T}";
        }

        private string GetWeatherIcon(string condition)
        {
            if (condition.Equals("Невеликий дощ") || condition.Equals("Сильний дощ") || condition.Equals("Мряка")
                            || condition.Equals("Місцями мряка") || condition.Equals("Помірний дощ)"))
            {
                return " 🌧";
            }
            if (condition.Equals("Невелика злива"))
            {
                return " ⛈️";
            }
            if (condition.Equals("Мінлива хмарність"))
            {
                return " ⛅";
            }
            if (condition.Equals("Сонячно") || condition.Equals("Ясно"))
            {
                return " ☀️";
            }
            if (condition.Equals("Похмуро"))
            {
                return " ☁️";
            }
            if (condition.Equals("Невеликий туман"))
            {
                return " 😶‍🌫️";
            }
            return "";
        }
    }
}
