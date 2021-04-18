using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
	public interface IValidateTransfer
	{
		bool IsUnderTheLimit(TransferHistory transfer, IList<TransferHistory> transfers);
	}
}
