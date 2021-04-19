using Data.Interfaces;
using Domain.Model;

namespace Data.Common
{
	public abstract class DataConnection : IDataConnection
	{
		protected readonly string Connection;
		
		public DataConnection(string connection)
		{
			Connection = connection;
		}
		
		protected Table<TransferHistory> Transfers { get; set; }
		public ITable<TransferHistory> GetTransfers() => Transfers;
	}
}
