using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_2
{
    public class Thermostat
    {
        private float _lastTemperature;
        
        public float CurrentTemperature { get; private set; }
        public float Threshold { get; }

        public Thermostat(float threshold = 30f)
        {
            Threshold = threshold;
            _lastTemperature = float.MinValue;
            CurrentTemperature = float.MinValue;
        }

        public event EventHandler<float> TemperatureThresholdReached;

        protected virtual void OnTemperatureThresholdReached(float temp)
        => TemperatureThresholdReached?.Invoke(this, temp);

        public void UpdateTemperature(float newTemp)
        {
            Console.WriteLine($"Current temperature: {newTemp}°C");

            bool crossedUp = _lastTemperature < Threshold && newTemp >= Threshold;

            _lastTemperature = newTemp;
            CurrentTemperature = newTemp;

            if (crossedUp) OnTemperatureThresholdReached(newTemp);
        }


        public delegate bool TemperatureRule(float temp, float threshold);
        public bool CheckRule(TemperatureRule rule) 
            => rule?.Invoke(CurrentTemperature, Threshold) ?? false;
    }
}
