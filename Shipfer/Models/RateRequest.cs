using Newtonsoft.Json;

namespace Shipfer.Models;
public class FromAddress
{
    [JsonProperty("addressLine1")]
    public string AddressLine1 { get; set; }

    [JsonProperty("addressLine2")]
    public string AddressLine2 { get; set; }

    [JsonProperty("addressLine3")]
    public string AddressLine3 { get; set; }

    [JsonProperty("cityTown")]
    public string CityTown { get; set; }

    [JsonProperty("company")]
    public string Company { get; set; }

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("postalCode")]
    public string PostalCode { get; set; }

    [JsonProperty("stateProvince")]
    public string StateProvince { get; set; }

    [JsonProperty("residential")]
    public bool Residential { get; set; }
}

public class Parcel
{
    [JsonProperty("height")]
    public double Height { get; set; }

    [JsonProperty("length")]
    public double Length { get; set; }

    [JsonProperty("width")]
    public double Width { get; set; }

    [JsonProperty("dimUnit")]
    public string DimUnit { get; set; }

    [JsonProperty("weightUnit")]
    public string WeightUnit { get; set; }

    [JsonProperty("weight")]
    public double Weight { get; set; }
}

public class RateRequest
{
    [JsonProperty("dateOfShipment")]
    public string DateOfShipment { get; set; }

    [JsonProperty("fromAddress")]
    public FromAddress FromAddress { get; set; }

    [JsonProperty("parcel")]
    public Parcel Parcel { get; set; }

    [JsonProperty("carrierAccounts")]
    public List<string> CarrierAccounts { get; set; }

    [JsonProperty("parcelType")]
    public string ParcelType { get; set; }

    [JsonProperty("toAddress")]
    public ToAddress ToAddress { get; set; }

    [JsonProperty("isHazmat")]
    public bool IsHazmat { get; set; }
}

public class ToAddress
{
    [JsonProperty("addressLine1")]
    public string AddressLine1 { get; set; }

    [JsonProperty("addressLine2")]
    public string AddressLine2 { get; set; }

    [JsonProperty("addressLine3")]
    public string AddressLine3 { get; set; }

    [JsonProperty("cityTown")]
    public string CityTown { get; set; }

    [JsonProperty("company")]
    public string Company { get; set; }

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("postalCode")]
    public string PostalCode { get; set; }

    [JsonProperty("residential")]
    public bool Residential { get; set; }

    [JsonProperty("stateProvince")]
    public string StateProvince { get; set; }
}