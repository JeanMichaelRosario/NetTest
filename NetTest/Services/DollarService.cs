using Domain.Interfaces;
using Domain.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
	public class DollarService : ICurrencyService
	{
		public async Task<Currency> GetValueFromUrl(string url)
		{
			using (var client = new HttpClient())
			{
				var result = await client.GetAsync(url);
				return await GetValuesFromContentResult(result);
			}
		}

		private async Task<Currency> GetValuesFromContentResult(HttpResponseMessage result)
		{
			string body = await result.Content.ReadAsStringAsync();
			var array = JsonConvert.DeserializeObject<string[]>(body);
			var currency = new Currency(ConvertToDecimal(array[0]), ConvertToDecimal(array[1]));
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
