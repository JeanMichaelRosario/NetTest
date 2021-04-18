using Domain.Model;
using Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ServicesTest
{
    public class ValidateTransferTest
    {
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

            return list;
        }

        [Theory]
        [InlineData("usd", 200)]
        [InlineData("brl", 300)]
        public void TransferWithoutPreviousCanBeDone(string currency, decimal amount)
        {
            var transfer = new TransferHistory()
            {
                UserId = 11,
                ExchangeAmount = amount,
                CurrencyCode = currency,
                Date = DateTime.Now
            };

            var validate = new ValidateTransfer(GetListOfLimits());
            var result = validate.IsUnderTheLimit(transfer, GetListOfHistory());
            Assert.True(result);
        }

        [Theory]
        [InlineData("usd", 201)]
        [InlineData("brl", 301)]
        public void TransferWithoutPreviousCanNotBeDone(string currency, decimal amount)
        {
            var transfer = new TransferHistory()
            {
                UserId = 11,
                ExchangeAmount = amount,
                CurrencyCode = currency,
                Date = DateTime.Now
            };

            var validate = new ValidateTransfer(GetListOfLimits());
            var result = validate.IsUnderTheLimit(transfer, GetListOfHistory());
            Assert.False(result);
        }

        [Theory]
        [InlineData("usd", 20)]
        [InlineData("brl", 30)]
        public void TransferWithPreviousCanBeDone(string currency, decimal amount)
        {
            var transfer = new TransferHistory()
            {
                UserId = 12,
                ExchangeAmount = amount,
                CurrencyCode = currency,
                Date = DateTime.Now
            };

            var validate = new ValidateTransfer(GetListOfLimits());
            var result = validate.IsUnderTheLimit(transfer, GetListOfHistory());
            Assert.True(result);
        }

        [Theory]
        [InlineData("usd", 21)]
        [InlineData("brl", 31)]
        public void TransferWithPreviousCanNotBeDone(string currency, decimal amount)
        {
            var transfer = new TransferHistory()
            {
                UserId = 12,
                ExchangeAmount = amount,
                CurrencyCode = currency,
                Date = DateTime.Now
            };

            var validate = new ValidateTransfer(GetListOfLimits());
            var result = validate.IsUnderTheLimit(transfer, GetListOfHistory());
            Assert.False(result);
        }
    }
}
