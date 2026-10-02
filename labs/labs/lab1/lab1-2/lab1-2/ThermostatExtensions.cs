using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_2
{
    public static class ThermostatExtensions
    {
        public static bool IsCritical(this Thermostat thermostat, float margin)
            => thermostat.CurrentTemperature >= thermostat.Threshold + margin;

        public static bool IsStable(this Thermostat thermostat, float tolerance)
            => Math.Abs(thermostat.CurrentTemperature - thermostat.Threshold) <= tolerance;
    }
}
