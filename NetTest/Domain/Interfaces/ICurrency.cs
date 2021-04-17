using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
	public interface ICurrency
	{
		decimal GetBuyPrice();
		decimal GetSellPrice();
	}
}
