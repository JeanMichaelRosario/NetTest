using Data.Common;
using Microsoft.Extensions.Options;

namespace Data.SqlServer
{
	public class SqlServer : DataConnection
	{
		public SqlServer(string connection) : base(connection)
		{
			Transfers = new TransferTable(connection);
		}

	}
}
