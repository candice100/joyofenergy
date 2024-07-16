using JOIEnergy.Domain;
using JOIEnergy.Enums;
using System;
using Xunit;

namespace JOIEnergy.Tests
{
    public class PricePlanTest
    {
        private readonly PricePlan _pricePlan;

        public PricePlanTest()
        {
            _pricePlan = new PricePlan
            {
                EnergySupplier = Supplier.TheGreenEco,
                UnitRate = 20m,
                PeakTimeMultiplier = [
                    new() { DayOfWeek = DayOfWeek.Saturday, Multiplier = 2m },
                    new() { DayOfWeek = DayOfWeek.Sunday, Multiplier = 10m }
                ]
            };
        }

        [Fact]
        public void TestGetEnergySupplier() {
            Assert.Equal(Supplier.TheGreenEco, _pricePlan.EnergySupplier);
        }

        [Fact]
        public void TestGetBasePrice() {
            Assert.Equal(20m, _pricePlan.GetPrice(new DateTime(2018, 1, 2)));
        }

        [Fact]
        public void TestGetPeakTimePrice()
        {
            Assert.Equal(40m, _pricePlan.GetPrice(new DateTime(2018, 1, 6)));
        }

    }
}
