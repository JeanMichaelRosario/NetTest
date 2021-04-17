using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Currencies
{
	public abstract class Currency
	{
		protected Model.Currency _currency { get; set; }

		public Currency(decimal buy, decimal sell)
		{
			_currency = new Model.Currency(buy, sell);
		}

		public Currency(Currency currency, decimal exchangeRate)
		{
			var buy = currency._currency.Buy * exchangeRate;
			var sell = currency._currency.Sell * exchangeRate;
			_currency = new Model.Currency(buy, sell);
		}

		public Model.Currency GetCurrency()
		{
			return _currency;
		}

		public static Currency GetDollar(decimal buy, decimal sell) => new Dollar(buy, sell);
		public static Currency GetReal(Dollar dollar, decimal exchangeRate) => new Real(dollar, exchangeRate);
	}
}
