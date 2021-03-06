using Data.Common;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.SqlServer
{
	public class TransferTable : Table<TransferHistory>
	{
		private readonly VirtualMindDbContext dbContext;

		public TransferTable(string connection) : base(connection)
		{
			dbContext = new VirtualMindDbContext(connection);
		}

		public override void Add(TransferHistory obj)
		{
			dbContext.Transfer.Add(obj);
			dbContext.SaveChanges();
		}

		public override IEnumerable<TransferHistory> GetAll()
		{
			return dbContext.Transfer;
		}

		public override void Remove(TransferHistory obj)
		{
			dbContext.Transfer.Remove(obj);
			dbContext.SaveChanges();
		}

		public override void Update(int id, TransferHistory obj)
		{
			dbContext.Transfer.Update(obj);
			dbContext.SaveChanges();
		}
	}
}
