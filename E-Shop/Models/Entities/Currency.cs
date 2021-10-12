using Newtonsoft.Json;

namespace E_Shop.Models
{
  public class Currency
  {
    [JsonProperty("currencyName")]
    public string CurrencyName { get; set; }

    [JsonProperty("currencySymbol")]
    public string CurrencySymbol { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
  }
}