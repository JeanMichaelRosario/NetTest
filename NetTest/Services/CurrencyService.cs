using Domain.Interfaces;
using Domain.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
	public class CurrencyService : ICurrencyService
	{
		private readonly string _dollarUrl;
		private const string DOLLAR_CODE = "usd";
		private const string REAL_CODE = "brl";
		private const decimal REAL_EXCHANGE_RATE = 0.25M;

        public CurrencyService(IOptionsMonitor<string> dollarUrl)
        {
			_dollarUrl = dollarUrl.CurrentValue;
        }
		public async Task<Currency> GetDollar()
		{
            using (var client = new HttpClient())
            {
				var result = await client.GetAsync(_dollarUrl);
				return await GetValuesFromContentResult(result);
			}
		}

		public async Task<Currency> GetCurrencyByCode(string currencyCode)
		{
			Currency result;
			var dollar = await GetDollar();

			switch (currencyCode.ToLower())
			{
				case "usd":
					result = dollar;
					break;
				case "brl":
					var real = new Currency(dollar, REAL_EXCHANGE_RATE, REAL_CODE);
					result = real;
					break;
				default:
					result = null;
					break;
			}

			if (result != null)
			{
				return result;
			}
			throw new Exception("Currency does not exist");
		}

		private async Task<Currency> GetValuesFromContentResult(HttpResponseMessage result)
		{
			string body = await result.Content.ReadAsStringAsync();
			var array = JsonConvert.DeserializeObject<string[]>(body);
			var currency = new Currency(ConvertToDecimal(array[0]), ConvertToDecimal(array[1]), DOLLAR_CODE);
			return currency;
		}

		private decimal ConvertToDecimal(string value)
		{
			value = value.Replace(',', '.');
			var isNumeric = decimal.TryParse(value, out var result);
			if (isNumeric && result > 0)
			{
				return ValidateValue(result);
			}
			throw new ArithmeticException("Value is not valid");
		}

		private decimal ValidateValue(decimal value)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException("Value should be greater than zero");
			}
			return decimal.Round(value, 2);
		}
    }
}
