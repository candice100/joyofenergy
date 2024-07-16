using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public class AccountService(Dictionary<string, string> smartMeterToPricePlanAccounts) : IAccountService
    {
        public string GetPricePlanIdForSmartMeterId(string smartMeterId)
            => smartMeterToPricePlanAccounts.TryGetValue(smartMeterId, out string value) ? value : null;
    }
}
