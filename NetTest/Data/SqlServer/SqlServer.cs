using Data.Common;
using Microsoft.Extensions.Options;

namespace Data.SqlServer
{
	public class SqlServer : DataConnection
	{
		public SqlServer(IOptionsMonitor<string> connection) : base(connection)
		{
			Transfers = new TransferTable(connection.CurrentValue);
		}

	}
}
