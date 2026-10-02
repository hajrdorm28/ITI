using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    public class TemperatureDisplay
    {
        private string _name;

        public TemperatureDisplay(string name, Thermostat thermostat)
        {
            _name = name;
            thermostat.TemperatureThresholdReached += OnTemperatureThresholdReached;
        }

        private void OnTemperatureThresholdReached(object? sender, float temp)
        {
            if(_name == "display1") 
                Console.WriteLine($"ALERT: Temperature {temp}°C crossed threshold (30°C)");
            else 
                Console.WriteLine($"{_name}: Threshold exceeded! Current: {temp}°C");
        }
    }
}
