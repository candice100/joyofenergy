using JOIEnergy.Domain;
using System;
using System.Collections.Generic;

namespace JOIEnergy.Generator
{
    public class ElectricityReadingGenerator
    {
        public ElectricityReadingGenerator() { }

        public List<ElectricityReading> Generate(int count)
        {
            var readings = new List<ElectricityReading>();
            var random = new Random();
            for (int i = count; i > 0; i--)
            {
                var reading = (decimal)random.NextDouble();
                var electricityReading = new ElectricityReading
                {
                    Reading = reading,
                    Time = DateTime.Now.AddSeconds(-i * 10)
                };
                readings.Add(electricityReading);
            }
            
            return readings;
        }
    }
}
