using Domain.Model;
using Xunit;

namespace DomainTest
{
    public class CurrencyTest
    {
        [Theory]
        [InlineData(15, 18, 0.3, 4.5, 5.4, "brl")]
        [InlineData(20, 24, 0.4, 8, 9.6, "brl")]
        public void ExchangeTest(decimal buy, decimal sell, decimal exchangeRate, decimal expectedBuy, decimal expectedSell, string currencyCode)
        {
            var dollar = new Currency(buy, sell, "usd");
            var real = new Currency(dollar, exchangeRate, currencyCode);

            Assert.Equal(expectedBuy, real.Buy);
            Assert.Equal(expectedSell, real.Sell);
            Assert.Equal(currencyCode, real.CurrencyCode);
        }

        [Theory]
        [InlineData(15, 18, "usd")]
        [InlineData(20, 24, "usd")]
        public void CreateCurrency(decimal buy, decimal sell, string currencyCode)
        {
            var dollar = new Currency(buy, sell, currencyCode);

            Assert.Equal(buy, dollar.Buy);
            Assert.Equal(sell, dollar.Sell);
            Assert.Equal(currencyCode, dollar.CurrencyCode);
        }
    }
}
