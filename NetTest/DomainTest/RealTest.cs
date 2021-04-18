using Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DomainTest
{
    public class RealTest
    {
        [Theory]
        [InlineData(15, 18, 0.3, 4.5, 5.4)]
        [InlineData(20, 24, 0.4, 8, 9.6)]
        [InlineData(25, 30, 0.5, 12.5, 15)]
        public void ExchangeTest(decimal buy, decimal sell, decimal exchangeRate, decimal expectedBuy, decimal expectedSell)
        {
            var dollar = Currency.GetDollar(buy, sell);
            var real = Currency.GetReal(dollar, exchangeRate).GetCurrency();

            Assert.Equal(expectedBuy, real.Buy);
            Assert.Equal(expectedSell, real.Sell);
        }
    }
}
