using System;


/// Attempt to read system temperature on macOS using osx-cpu-temp command line tool. 
/// how ever osx could not be used with a mac with M2 chip so this is just a placeholder implementation. 


public class Temperature
{
    private decimal _temperatureValue;

    public Temperature(decimal value, string unit)
    {
        _temperatureValue = value;
    }

    public static Temperature GetSystemTemperature()
    {
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "osx-cpu-temp",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            
            if (decimal.TryParse(output.Replace("°C", "").Trim(), out decimal temp))
            {
                return new Temperature(temp, "Celsius");
            }
        }
        catch
        {
            
        }
        
        return new Temperature(0, "Celsius"); 
    }

    public decimal Celsius => _temperatureValue;
    public decimal Fahrenheit => (_temperatureValue * 9 / 5) + 32;
    

    public override string ToString()
    {
        return $"{_temperatureValue}°C";
    }
}

