using Shipfer.Models;

namespace Shipfer.Services;

public interface IShipping360Service
{
    Task GenerateAccessToken();
    Task<RatesResponse> GetRates(RateRequest rateRequest);
}