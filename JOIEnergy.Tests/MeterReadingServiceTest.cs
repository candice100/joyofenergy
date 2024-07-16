using JOIEnergy.Domain;
using JOIEnergy.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace JOIEnergy.Tests
{
    public class MeterReadingServiceTest
    {
        private static string SMART_METER_ID = "smart-meter-id";

        private readonly MeterReadingService _meterReadingService;

        public MeterReadingServiceTest()
        {
            _meterReadingService = new MeterReadingService(new Dictionary<string, List<ElectricityReading>>());

            _meterReadingService.StoreReadings(new MeterReadings()
            {
                SmartMeterId = SMART_METER_ID,
                ElectricityReadings = [new ElectricityReading() { Time = DateTime.Now.AddMinutes(-30), Reading = 35m },
                                        new ElectricityReading() { Time = DateTime.Now.AddMinutes(-15), Reading = 30m }]
            });
        }

        [Fact]
        public void GivenMeterIdThatDoesNotExistShouldReturnNull()
        {
            Assert.Empty(_meterReadingService.GetReadings("unknown-id"));
        }

        [Fact]
        public void GivenMeterReadingThatExistsShouldReturnMeterReadings()
        {
            _meterReadingService.StoreReadings(new MeterReadings()
            {
                SmartMeterId = SMART_METER_ID,
                ElectricityReadings = [new ElectricityReading() { Time = DateTime.Now, Reading = 25m }]
            });

            var electricityReadings = _meterReadingService.GetReadings(SMART_METER_ID);

            Assert.Equal(3, electricityReadings.Count);
        }
    }
}
