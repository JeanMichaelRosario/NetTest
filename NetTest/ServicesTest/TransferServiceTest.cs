using Data.Interfaces;
using Domain.Interfaces;
using Domain.Model;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTest
{
    public class TransferServiceTest
    {
        #region Setup
        public List<TransferHistory> GetListOfHistory()
        {
            var list = new List<TransferHistory>();

            list.Add(new TransferHistory()
            {
                ID = 1,
                UserId = 12,
                ExchangeAmount = 180,
                CurrencyCode = "usd",
                Date = DateTime.Now
            });

            list.Add(new TransferHistory()
            {
                ID = 1,
                UserId = 12,
                ExchangeAmount = 270,
                CurrencyCode = "brl",
                Date = DateTime.Now
            });

            return list;
        }

        public List<TransferLimit> GetListOfLimits()
        {
            var list = new List<TransferLimit>();

            list.Add(new TransferLimit()
            {
                Limit = 200,
                UserId = 12,
                CurrencyCode = "usd",
                Date = DateTime.Now
            });

            list.Add(new TransferLimit()
            {
                Limit = 300,
                UserId = 12,
                CurrencyCode = "brl",
                Date = DateTime.Now
            });

            list.Add(new TransferLimit()
            {
                Limit = 400,
                UserId = 13,
                CurrencyCode = "usd",
                Date = DateTime.Now
            });

            list.Add(new TransferLimit()
            {
                Limit = 600,
                UserId = 13,
                CurrencyCode = "brl",
                Date = DateTime.Now
            });

            return list;
        }

        private Mock<ICurrencyService> GetCurrencyServiceMock(string currency)
        {
            var mock = new Mock<ICurrencyService>();
            mock.Setup(x => x.GetCurrencyByCode(currency)).Returns(Task.FromResult(new Currency(20, 30, currency)));
            return mock;
        }

        private Mock<IDataConnection> GetDataMock()
        {
            var mock = new Mock<IDataConnection>();
            mock.Setup(x => x.GetTransfers()).Returns(GetTableMock().Object);
            return mock;
        }

        private Mock<ITable<TransferHistory>> GetTableMock()
        {
            var mock = new Mock<ITable<TransferHistory>>();
            mock.Setup(x => x.GetAll()).Returns(GetListOfHistory());
            return mock;
        }
        #endregion

        [Theory]
        [InlineData("usd", 3000, 100)]
        [InlineData("brl", 9000, 300)]
        public void TransactionSuccesTest(string currency, decimal amount, decimal expectedAmountExchanged)
        {
            var transfer = new TransferHistory()
            {
                UserId = 13,
                CurrencyCode = currency,
                ExchangeAmount = amount,
                Date = DateTime.Now
            };

            var data = GetDataMock();
            var currencyService = GetCurrencyServiceMock(currency);
            var transactionService = new TransferService(GetListOfLimits(), data.Object, currencyService.Object);

            var amountExchanged = transactionService.MakeTransaction(transfer);
            Assert.Equal(expectedAmountExchanged, amountExchanged);
        }

        [Theory]
        [InlineData("usd", 62000)]
        [InlineData("brl", 120000)]
        public void TransactionPassingLimitTest(string currency, decimal limit)
        {
            var transfer = new TransferHistory()
            {
                UserId = 13,
                CurrencyCode = currency,
                ExchangeAmount = limit,
                Date = DateTime.Now
            };

            var data = GetDataMock();
            var currencyService = GetCurrencyServiceMock(currency);
            var transactionService = new TransferService(GetListOfLimits(), data.Object, currencyService.Object);

            Assert.Throws<Exception>(() => { var amountExchanged = transactionService.MakeTransaction(transfer); });
        }

        [Theory]
        [InlineData("usd", 600, 20)]
        [InlineData("brl", 900, 30)]
        public void TransactionSuccesWithPreviousDataTest(string currency, decimal amount, decimal expectedAmountExchanged)
        {
            var transfer = new TransferHistory()
            {
                UserId = 12,
                CurrencyCode = currency,
                ExchangeAmount = amount,
                Date = DateTime.Now
            };

            var data = GetDataMock();
            var currencyService = GetCurrencyServiceMock(currency);
            var transactionService = new TransferService(GetListOfLimits(), data.Object, currencyService.Object);

            var amountExchanged = transactionService.MakeTransaction(transfer);
            Assert.Equal(expectedAmountExchanged, amountExchanged);
        }

        [Theory]
        [InlineData("usd", 3000)]
        [InlineData("brl", 9000)]
        public void UserHasNoLimitTest(string currency, decimal amount)
        {
            var transfer = new TransferHistory()
            {
                UserId = 1,
                CurrencyCode = currency,
                ExchangeAmount = amount,
                Date = DateTime.Now
            };

            var data = GetDataMock();
            var currencyService = GetCurrencyServiceMock(currency);
            var transactionService = new TransferService(GetListOfLimits(), data.Object, currencyService.Object);

            Assert.Throws<Exception>(() => { var amountExchanged = transactionService.MakeTransaction(transfer); });
        }
    }
}
