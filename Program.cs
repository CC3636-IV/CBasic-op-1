using System;
using System.Threading.Tasks;
using Spectre.Console;


namespace CBasic_op_1;

class Program
{
    static async Task Main(string[] args)
    {

        string apiKey = Environment.GetEnvironmentVariable("WEATHERSTACK_API_KEY");
        

        if (string.IsNullOrEmpty(apiKey))
        {
            AnsiConsole.MarkupLine("[bold red]Please set the WEATHERSTACK_API_KEY environment variable[/]");
            return;
        }

        string city = args.Length > 0 ? args[0] : "Bergen,no";
        var weather = new WeatherService();
        var tempC = await weather.GetTemperatureCelsiusAsync(city, apiKey);

        if (tempC.HasValue)
        {
            decimal c = tempC.Value;
            decimal f = Math.Round((c * 9m / 5m) + 32m, 1);

            // output panel with Spectre.Console
            var panel = new Panel(new Markup($"[bold yellow]{c}°C[/]  [grey]([/][cyan]{f}°F[/][grey])[/]"))
                .Header($"Current temperature in [green]{city}[/]", Justify.Center)
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand()
                // .Width(30) --- IGNORE --- (Google told me this works the error code told otherwise)
                ;

            AnsiConsole.Write(panel);
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Could not retrieve weather data.[/]");
        }


        
    }
}
