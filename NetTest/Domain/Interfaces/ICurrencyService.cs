using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ICurrencyService
	{
		Task<Currency> GetDollarValueFromUrl(string url);
	}
}
