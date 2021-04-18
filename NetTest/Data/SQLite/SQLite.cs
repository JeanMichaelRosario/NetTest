using Data.Common;

namespace Data.SQLite
{
	public class SQLite : DataConnection
	{
		public SQLite(string connection): base(connection)
		{
			Transfers = new TransferTable(connection);
		}
	}
}
