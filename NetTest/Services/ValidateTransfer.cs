﻿using Domain.Interfaces;
using Domain.Model;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Services
{
	public class ValidateTransfer : IValidateTransfer
	{
		private readonly IList<TransferLimit> _transferLimits;

        public ValidateTransfer(IList<TransferLimit> limits)
        {
			_transferLimits = limits;
        }

		public bool IsUnderTheLimit(TransferHistory transfer, IList<TransferHistory> transfers)
		{
			bool result = false;
			decimal limit = GetLimitForThisTransfer(transfer);
			if (TransferIsDoable(limit, transfer))
			{
				if (TransferByUserDoesNotExist(transfer, transfers))
				{
					result = true;
				}
				else
				{
					var transferLimit = GetLimitForThisTransfer(transfer, transfers, limit);
					result = transferLimit >= transfer.ExchangeAmount;
				}
			}

			return result;
		}

		private decimal GetLimitForThisTransfer(TransferHistory transfer)
		{

			var limit = _transferLimits.FirstOrDefault(t => t.CurrencyCode == transfer.CurrencyCode &&
														t.Date.Month == transfer.Date.Month);
			if (limit == null)
			{
				throw new System.Exception("This user cannot perform a transfer");
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
		private bool TransferIsDoable(decimal transferLimit, TransferHistory transfer)
		{
			return transferLimit >= transfer.ExchangeAmount;
		}
	}
}
