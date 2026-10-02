using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    public class Thermostat
    {
        private float _lastTemperature;
        private float _threshold;

        public Thermostat(float threshold = 30f)
        {
            _threshold = threshold;
            _lastTemperature = float.MinValue;
        }

        public event EventHandler<float> TemperatureThresholdReached;
        protected virtual void OnTemperatureThresholdReached(float currentTemp) 
            => TemperatureThresholdReached?.Invoke(this, currentTemp);

        public void UpdateTemperature(float NewTemperature)
        {
            Console.WriteLine($"Current temperature: {NewTemperature}");

            bool crossedUp = _lastTemperature < _threshold && NewTemperature >= _threshold; // true

            _lastTemperature = NewTemperature;

            if(crossedUp) OnTemperatureThresholdReached(NewTemperature);
        }
}
}
