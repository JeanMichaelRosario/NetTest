using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
	public interface ITransferService
	{
		decimal MakeTransaction(TransferHistory transfer);
	}
}
