using Domain.Interfaces;
using Domain.Model;
using System.Linq;
using System.Collections.Generic;
using Data.Interfaces;
using System;

namespace Services
{
	public class TransferService : ITransferService
	{
		private readonly IList<TransferLimit> _transferLimits;
		private readonly IDataConnection _dataConnection;
		private readonly ICurrencyService _currencyService;

        public TransferService(IList<TransferLimit> limits, IDataConnection data, ICurrencyService currencyService)
        {
			_transferLimits = limits;
			_dataConnection = data;
			_currencyService = currencyService;
        }

		public decimal MakeTransaction(TransferHistory transfer)
		{
			var exchangeAmount = GetExchangeAmount(transfer.ExchangeAmount, transfer.CurrencyCode);
			var listOfPreviousTransactions = _dataConnection.GetTransfers().GetAll().ToList();
			if (IsUnderTheLimit(exchangeAmount, transfer, listOfPreviousTransactions))
			{
				transfer.ExchangeAmount = exchangeAmount;
				SaveTransaction(transfer);
				return transfer.ExchangeAmount;
			}
            else
            {
				throw new System.Exception("This user can not perform this transfer. This purchase is over the limit for this user");
            }
		}

        private bool IsUnderTheLimit(decimal exchangeAmount, TransferHistory transfer, IList<TransferHistory> transfers)
		{
			bool result = false;
			decimal limit = GetLimitForThisTransfer(transfer);
			if (TransferIsDoable(limit, exchangeAmount))
			{
				if (TransferByUserDoesNotExist(transfer, transfers))
				{
					result = true;
				}
				else
				{
					var transferLimit = GetLimitForThisTransfer(transfer, transfers, limit);
					result = TransferIsDoable(transferLimit, exchangeAmount);
				}
			}

			return result;
		}

		private decimal GetLimitForThisTransfer(TransferHistory transfer)
		{

			var limit = _transferLimits.FirstOrDefault(t => t.UserId == transfer.UserId &&
														t.CurrencyCode == transfer.CurrencyCode &&
														t.Date.Month == transfer.Date.Month);
			if (limit == null)
			{
				throw new System.Exception("This user cannot perform a transfer, limit is not defined");
			}

			return limit.Limit;
		}

		/// <summary>
		/// check if the user has not make a transfer yet
		/// </summary>
		/// <param name="transfer"></param>
		/// <param name="transfers"></param>
		/// <returns></returns>
		private bool TransferByUserDoesNotExist(TransferHistory transfer, IList<TransferHistory> transfers)
		{
			var exist = transfers.Any(t => t.UserId == transfer.UserId);
			return !exist;
		}

		/// <summary>
		/// Get amount of transfer the user can do
		/// </summary>
		/// <param name="transfer"></param>
		/// <param name="transfers"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		private decimal GetLimitForThisTransfer(TransferHistory transfer, IList<TransferHistory> transfers, decimal limit)
		{
			var transferLimit = transfers.Where(t => t.UserId == transfer.UserId && 
												t.CurrencyCode == transfer.CurrencyCode &&
												t.Date.Month == transfer.Date.Month).Sum(t => t.ExchangeAmount);
			return limit - transferLimit;
		}

		/// <summary>
		/// If transferLimit is under the amount transaction then it can be done
		/// </summary>
		/// <param name="transferLimit"></param>
		/// <param name="transfer"></param>
		/// <returns></returns>
		private bool TransferIsDoable(decimal transferLimit, decimal exchangeAmount)
		{
			return transferLimit >= exchangeAmount;
		}

        private decimal GetExchangeAmount(decimal exchangeAmount, string currencyCode)
        {
			var currency = _currencyService.GetCurrencyByCode(currencyCode).Result;
			return Math.Round(exchangeAmount / currency.Sell, 2);
        }
		private void SaveTransaction(TransferHistory transfer)
		{
			_dataConnection.GetTransfers().Add(transfer);
		}
	}
}
