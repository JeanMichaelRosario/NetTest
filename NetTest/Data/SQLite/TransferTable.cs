﻿using Data.Common;
using Domain.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.SQLite
{
	public class TransferTable : Table<Transfer>
	{
		public TransferTable(string conn): base(conn)
		{

		}

		public override void Add(Transfer obj)
		{
			using (var con = new SqliteConnection(Connection))
			{
				con.Open();
				string commandQuery = "INSERT INTO Transfer(UserId, ExchangeAmount, CurrencyCode, Date) Values (@userId, @amount, @code, @date)";
				using (var command = new SqliteCommand(commandQuery, con))
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

		public override IEnumerable<Transfer> GetAll()
		{
			List<Transfer> transfers = new List<Transfer>();

			using (var con = new SqliteConnection(Connection))
			{
				con.Open();
				string commandQuery = "SELECT * FROM Transfer";
				using (var command = new SqliteCommand(commandQuery, con))
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var transfer = new Transfer()
						{
							ID = reader.GetInt32(0),
							UserId = reader.GetInt32(1),
							ExchangeAmount = reader.GetDecimal(2),
							CurrencyCode = reader.GetString(3),
							Date = reader.GetDateTime(4)
						};
						transfers.Add(transfer);
					}
				}
			}
			return transfers;
		}

		public override void Remove(Transfer obj)
		{
			using (var con = new SqliteConnection(Connection))
			{
				con.Open();
				string commandQuery = "DELETE FROM Transfer WHERE ID = @Id";
				using (var command = new SqliteCommand(commandQuery, con))
				{
					command.Parameters.AddWithValue("@Id", obj.ID);

					command.Prepare();
					command.ExecuteNonQuery();
				}
			}
		}

		public override void Update(int id, Transfer obj)
		{
			using (var con = new SqliteConnection(Connection))
			{
				con.Open();
				string commandQuery = "UPDATE Transfer(UserId, ExchangeAmount, CurrencyCode, Date) Values (@userId, @amount, @code, @date) WHERE Id = @Id";
				using (var command = new SqliteCommand(commandQuery, con))
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
