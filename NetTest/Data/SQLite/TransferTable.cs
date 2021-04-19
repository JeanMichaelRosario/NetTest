using Data.Common;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Data.SQLite
{
	public class TransferTable : Table<TransferHistory>
	{
		public TransferTable(string conn): base(conn)
		{

		}

		public override void Add(TransferHistory obj)
		{
			using (var con = new SQLiteConnection(Connection))
			{
				con.Open();
				string commandQuery = "INSERT INTO Transfer(UserId, ExchangeAmount, CurrencyCode, Date) Values (@userId, @amount, @code, @date)";
				using (var command = new SQLiteCommand(commandQuery, con))
				{
					command.Parameters.AddWithValue("@userId", obj.UserId);
					command.Parameters.AddWithValue("@amount", obj.ExchangeAmount);
					command.Parameters.AddWithValue("@code", obj.CurrencyCode);
					command.Parameters.AddWithValue("@date", obj.Date);

					command.Prepare();
					command.ExecuteNonQuery();
				}
			}
		}

		public override IEnumerable<TransferHistory> GetAll()
		{
			List<TransferHistory> transfers = new List<TransferHistory>();

			using (var con = new SQLiteConnection(Connection))
			{
				con.Open();
				string commandQuery = "SELECT * FROM Transfer;";
				using (var command = new SQLiteCommand(commandQuery, con))
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var transfer = new TransferHistory()
						{
							ID = reader.GetInt32(0),
							UserId = reader.GetInt32(1),
							CurrencyCode = reader.GetString(2),
							ExchangeAmount = reader.GetDecimal(3),
							Date = reader.GetDateTime(4)
						};
						transfers.Add(transfer);
					}
				}
			}
			return transfers;
		}

		public override void Remove(TransferHistory obj)
		{
			using (var con = new SQLiteConnection(Connection))
			{
				con.Open();
				string commandQuery = "DELETE FROM Transfer WHERE ID = @Id";
				using (var command = new SQLiteCommand(commandQuery, con))
				{
					command.Parameters.AddWithValue("@Id", obj.ID);

					command.Prepare();
					command.ExecuteNonQuery();
				}
			}
		}

		public override void Update(int id, TransferHistory obj)
		{
			using (var con = new SQLiteConnection(Connection))
			{
				con.Open();
				string commandQuery = "UPDATE Transfer(UserId, ExchangeAmount, CurrencyCode, Date) Values (@userId, @amount, @code, @date) WHERE Id = @Id";
				using (var command = new SQLiteCommand(commandQuery, con))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.Parameters.AddWithValue("@userId", obj.UserId);
					command.Parameters.AddWithValue("@amount", obj.ExchangeAmount);
					command.Parameters.AddWithValue("@code", obj.CurrencyCode);
					command.Parameters.AddWithValue("@date", obj.Date);

					command.Prepare();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
