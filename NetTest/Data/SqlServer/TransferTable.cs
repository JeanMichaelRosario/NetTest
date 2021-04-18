using Data.Common;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.SqlServer
{
	public class TransferTable : Table<Transfer>
	{
		private readonly VirtualMindDbContext dbContext;

		public TransferTable(string connection) : base(connection)
		{
			dbContext = new VirtualMindDbContext(connection);
		}

		public override void Add(Transfer obj)
		{
			dbContext.Transfers.Add(obj);
			dbContext.SaveChanges();
		}

		public override IEnumerable<Transfer> GetAll()
		{
			return dbContext.Transfers;
		}

		public override void Remove(Transfer obj)
		{
			dbContext.Transfers.Remove(obj);
			dbContext.SaveChanges();
		}

		public override void Update(int id, Transfer obj)
		{
			dbContext.Transfers.Update(obj);
			dbContext.SaveChanges();
		}
	}
}
