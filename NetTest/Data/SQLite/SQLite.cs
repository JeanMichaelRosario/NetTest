using Data.Common;
using Microsoft.Extensions.Options;

namespace Data.SQLite
{
	public class SQLite : DataConnection
	{
		public SQLite(IOptionsMonitor<string> connection): base(connection)
		{
			Transfers = new TransferTable(connection.CurrentValue);
		}
	}
}
