using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("price-plans")]
    public class PricePlanComparatorController(IPricePlanService pricePlanService, IAccountService accountService) : Controller
    {
        public const string PRICE_PLAN_ID_KEY = "pricePlanId";
        public const string PRICE_PLAN_COMPARISONS_KEY = "pricePlanComparisons";

        [HttpGet("compare-all/{smartMeterId}")]
        public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
        {
            var pricePlanId = accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            if (string.IsNullOrEmpty(pricePlanId))
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));

            var costPerPricePlan = pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

            return new ObjectResult(new Dictionary<string, object>() {
                {PRICE_PLAN_ID_KEY, pricePlanId},
                {PRICE_PLAN_COMPARISONS_KEY, costPerPricePlan},
            });
        }

        [HttpGet("recommend/{smartMeterId}")]
        public ObjectResult RecommendCheapestPricePlans(string smartMeterId, int? limit = null)
        {
            var consumptionForPricePlans = pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

            if (consumptionForPricePlans.Count == 0)
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));

            var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

            if (limit.HasValue && limit.Value < recommendations.Count())
                return new ObjectResult(recommendations.Take(limit.Value));

            return new ObjectResult(recommendations);
        }
    }
}
