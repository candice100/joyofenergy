using JOIEnergy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOIEnergy.Services
{
    public class PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService) : IPricePlanService
    {
        public interface IDebug { void Log(string s); };

        private decimal CalculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count;
        }

        private decimal CalculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }

        private decimal CalculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = CalculateAverageReading(electricityReadings);
            var timeElapsed = CalculateTimeElapsed(electricityReadings);
            var averagedCost = average / timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            var electricityReadings = meterReadingService.GetReadings(smartMeterId);

            if (electricityReadings.Count == 0)
                return [];

            return pricePlan.ToDictionary(plan => plan.PlanName, plan => CalculateCost(electricityReadings, plan));
        }
    }
}
