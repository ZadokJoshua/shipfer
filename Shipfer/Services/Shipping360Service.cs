using System.Text;
using Newtonsoft.Json;
using Shipfer.Helpers;
using Shipfer.Models;

namespace Shipfer.Services;

public class Shipping360Service : IShipping360Service
{
    private bool IsAccessTokenExpired = PreferenceHelper.IsAccessTokenExpired();

    public async Task GenerateAccessToken()
    {
        try
        {
            using HttpClient httpClient = new();

            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{AppConfig.SHIPPING_API_USERNAME}:{AppConfig.SHIPPING_API_PASSWORD}"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + base64Credentials);

            var response = await httpClient.PostAsync(AppConfig.SHIPPING_API_PRIMARY_URL + "auth/api/v1/token", null);

            string json = await response.Content.ReadAsStringAsync();

            var accessTokenResponse = JsonConvert.DeserializeObject<GenerateAccessTokenResponse>(json);

            if (accessTokenResponse != null)
            {
                PreferenceHelper.SaveAccessTokenResponse(accessTokenResponse);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<RatesResponse?> GetRates(RateRequest rateRequest)
    {
        if (IsAccessTokenExpired) await GenerateAccessToken();
        string? accessToken = PreferenceHelper.GetAccessToken();
        if (string.IsNullOrEmpty(accessToken)) return null;

        try
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("compactResponse", "true");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var rawBody = JsonConvert.SerializeObject(rateRequest);
            var postData = new StringContent(rawBody, Encoding.UTF8, "application/json");
            var request = await httpClient.PostAsync(AppConfig.SHIPPING_API_PRIMARY_URL + "shipping/api/v1/rates", postData);

            var response = await request.Content.ReadAsStringAsync();

            var rates = JsonConvert.DeserializeObject<RatesResponse>(response);

            return rates;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
