using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public class MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings) : IMeterReadingService
    {
        public List<ElectricityReading> GetReadings(string smartMeterId)
        {
            return meterAssociatedReadings.TryGetValue(smartMeterId, out List<ElectricityReading> value) ? value : [];
        }

        public void StoreReadings(MeterReadings meterReadings)
        {
            if (meterAssociatedReadings.TryGetValue(meterReadings.SmartMeterId, out List<ElectricityReading> value))
                meterReadings.ElectricityReadings.ForEach(electricityReading => value.Add(electricityReading));
            else
                meterAssociatedReadings.Add(meterReadings.SmartMeterId, meterReadings.ElectricityReadings);
        }
    }
}
