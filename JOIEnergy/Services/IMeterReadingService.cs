using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        void StoreReadings(MeterReadings meterReadings);
        bool IsMeterReadingsValid(MeterReadings meterReadings);
    }
}