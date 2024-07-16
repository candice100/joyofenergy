using JOIEnergy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOIEnergy.Domain
{
    public class PricePlan
    {
        public string PlanName { get; set; }
        public decimal UnitRate { get; set; }
        public Supplier EnergySupplier { get; set; }
        public IList<PeakTimeMultiplier> PeakTimeMultiplier { get; set; }

        public decimal GetPrice(DateTime datetime)
        {
            var multiplier = PeakTimeMultiplier.FirstOrDefault(m => m.DayOfWeek == datetime.DayOfWeek)?.Multiplier ?? 1;

            return multiplier * UnitRate;
        }
    }

    public class PeakTimeMultiplier
    {
        public DayOfWeek DayOfWeek { get; set; }
        public decimal Multiplier { get; set; }
    }
}
