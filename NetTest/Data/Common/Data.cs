using Data.Interfaces;
using Domain.Model;

namespace Data.Common
{
	public abstract class Data
	{
		protected readonly string Connection;
		
		public Data(string connection)
		{
			Connection = connection;
		}
		
		public Table<Transfer> Transfers { get; set; }
	}
}
