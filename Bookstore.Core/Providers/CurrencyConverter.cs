using System.Net;
using static Bookstore.Shared.Enums.Enums;
using Newtonsoft.Json.Linq;
using Bookstore.Core.Exceptions;
using static Bookstore.Shared.Constants.Constants.AppSettings;
using Microsoft.Extensions.Options;
using Bookstore.Shared.Models.Settings;

namespace Bookstore.Core.Providers
{
    public class CurrencyConverter
    {
        private readonly ExchangeRateSettings _settings;

        public CurrencyConverter(IOptions<ExchangeRateSettings> settings) => 
            _settings = settings.Value;

        public double Convert(double amount, CurrencyType from, CurrencyType? to)
        {
            if (to is null)
            {
                throw new ServerException(HttpStatusCode.InternalServerError, 
                    ExceptionMessage.CONVERT_FAILED);
            }

            var url = string.Format(_settings.Url, from, to);

            using var client = new WebClient();
            var json = client.DownloadString(url);

            JToken token = JObject.Parse(json);

            var exchangeRate = (double)token.SelectToken(_settings.Path);

            return amount * exchangeRate;
        }
    }
}
