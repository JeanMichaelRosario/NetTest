using Data.Interfaces;
using Domain.Model;

namespace Data.Common
{
	public abstract class DataConnection
	{
		protected readonly string Connection;
		
		public DataConnection(string connection)
		{
			Connection = connection;
		}
		
		public Table<Transfer> Transfers { get; set; }
	}
}
