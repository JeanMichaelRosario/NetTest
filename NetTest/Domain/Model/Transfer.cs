using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class Transfer
	{
		public int UserId { get; set; }
		public decimal ExchangeAmount { get; set; }
		public string CurrencyCode { get; set; }
		public DateTime Date { get; set; }
	}

	public class TransferHistory : Transfer
	{
		public int ID { get; set; }
	}

	public class TransferLimit : Transfer
	{
		public decimal Limit { get; set; }
	}
}
