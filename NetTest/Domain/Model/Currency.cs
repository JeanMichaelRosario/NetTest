﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class Currency
	{
		public decimal Buy { get; set; }
		public decimal Sell { get; set; }

		public Currency(decimal buy, decimal sell)
		{
			Buy = buy;
			Sell = sell;
		}
	}
}