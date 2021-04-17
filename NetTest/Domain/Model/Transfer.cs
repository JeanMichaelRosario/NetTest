using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class Transfer
	{
		public int ID { get; set; }
		public int UserId { get; set; }
		public decimal Exchange { get; set; }
		public string CurrencyCode { get; set; }
		public DateTime Date { get; set; }
	}
}
