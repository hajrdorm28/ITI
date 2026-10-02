using System;

namespace lab1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var thermostat = new Thermostat();

            thermostat.TemperatureThresholdReached += (sender, temp) 
                => Console.WriteLine($"ALERT: Temperature {temp}°C crossed threshold");

            thermostat.UpdateTemperature(25);
            thermostat.UpdateTemperature(35);

            Thermostat.TemperatureRule extremeHeat = (temp, threshold) => temp > threshold + 10;
            if (thermostat.CheckRule(extremeHeat))
                Console.WriteLine("RULE TRIGGERED: Extreme heat detected!");
            
            if (thermostat.IsCritical(5))
                Console.WriteLine("CRITICAL TEMPERATURE! (Margin: 5°C)");
            
            if (thermostat.IsStable(1))
                Console.WriteLine("System Stable (1°C)");
        }

    }
}