using Domain.Model;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ICurrencyService
	{
		Task<Currency> GetCurrencyByCode(string currencyCode);
	}
}
