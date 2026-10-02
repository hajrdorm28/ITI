using System;

namespace lab1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat(); // threshold = 30

            var display1 = new TemperatureDisplay("display1", thermostat);
            var display2 = new TemperatureDisplay("display2", thermostat);

            thermostat.UpdateTemperature(25);
            thermostat.UpdateTemperature(32);
        }
    }
}
 