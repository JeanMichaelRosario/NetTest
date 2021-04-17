using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Currencies
{
	public class Dollar : Currency
	{
		internal Dollar(decimal buy, decimal sell) : base(buy, sell)
		{

		}
	}
}
