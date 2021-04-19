using Data.Interfaces;
using Domain.Model;
using Microsoft.Extensions.Options;

namespace Data.Common
{
	public abstract class DataConnection : IDataConnection
	{
		protected readonly string Connection;
		
		public DataConnection(IOptionsMonitor<string> connection)
		{
			Connection = connection.CurrentValue;
		}
		
		protected Table<TransferHistory> Transfers { get; set; }
		public ITable<TransferHistory> GetTransfers() => Transfers;
	}
}
