using Data.Common;

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
