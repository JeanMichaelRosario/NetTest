using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Currencies
{
	public class Real : Currency
	{
		internal Real(Dollar dollar, decimal exchangeRate) : base(dollar, exchangeRate)
		{

		}
	}
}
