using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using E_Shop.Models;
using Newtonsoft.Json.Linq;

namespace E_Shop.Helpers
{
  public static class RequestHelper
  {
    public const string FreeBaseUrl = "https://free.currencyconverterapi.com/api/v6/";
    public const string PremiumBaseUrl = "https://api.currencyconverterapi.com/api/v6/";

    public static List<Currency> GetAllCurrencies(string apiKey = "https://localhost:44321")
    {
      string url;
      if (string.IsNullOrEmpty(apiKey))
        url = FreeBaseUrl + "currencies";
      else
        url = PremiumBaseUrl + "currencies" + "?apiKey=" + apiKey;

      var jsonString = GetResponse(url);

      var data = JObject.Parse(jsonString)["results"].ToArray();
      return data.Select(item => item.First.ToObject<Currency>()).ToList();
    }

    private static string GetResponse(string url)
    {
      string jsonString;

      var request = (HttpWebRequest)WebRequest.Create(url);
      request.AutomaticDecompression = DecompressionMethods.GZip;

      using (var response = (HttpWebResponse)request.GetResponse())
      using (var stream = response.GetResponseStream())
      using (var reader = new StreamReader(stream))
      {
        jsonString = reader.ReadToEnd();
      }

      return jsonString;
    }
  }
}