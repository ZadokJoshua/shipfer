using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipfer.Models;
public class DeliveryCommitment
{
    [JsonProperty("additionalDetails")]
    public string AdditionalDetails { get; set; }

    [JsonProperty("estimatedDeliveryDateTime")]
    public string EstimatedDeliveryDateTime { get; set; }

    [JsonProperty("guarantee")]
    public string Guarantee { get; set; }
}

public class Error
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

public class Rate
{
    [JsonProperty("baseCharge")]
    public double BaseCharge { get; set; }

    [JsonProperty("carrier")]
    public string Carrier { get; set; }

    [JsonProperty("carrierAccount")]
    public string CarrierAccount { get; set; }

    [JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonProperty("deliveryCommitment")]
    public DeliveryCommitment DeliveryCommitment { get; set; }

    [JsonProperty("parcelType")]
    public string ParcelType { get; set; }

    [JsonProperty("rateTypeId")]
    public string RateTypeId { get; set; }

    [JsonProperty("serviceId")]
    public string ServiceId { get; set; }

    [JsonProperty("surcharges")]
    public List<Surcharge> Surcharges { get; set; }

    [JsonProperty("totalCarrierCharge")]
    public double TotalCarrierCharge { get; set; }

    [JsonProperty("isHazmat")]
    public bool IsHazmat { get; set; }
}

public class RatesResponse
{
    [JsonProperty("rates")]
    public List<Rate> Rates { get; set; }

    [JsonProperty("errors")]
    public List<Error> Errors { get; set; }
}

public class Surcharge
{
    [JsonProperty("fee")]
    public double Fee { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}


