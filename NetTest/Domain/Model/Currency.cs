using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class Currency
	{
		public decimal Buy { get; private set; }
		public decimal Sell { get; private set; }
        public string CurrencyCode { get; private set; }

        public Currency(decimal buy, decimal sell, string currencyCode)
		{
			Buy = buy;
			Sell = sell;
			CurrencyCode = currencyCode;
		}

		public Currency(Currency currency, decimal exchangeRate, string currencyCode)
		{
			Buy = currency.Buy * exchangeRate;
			Sell = currency.Sell * exchangeRate;
			CurrencyCode = currencyCode;
		}
	}
}
