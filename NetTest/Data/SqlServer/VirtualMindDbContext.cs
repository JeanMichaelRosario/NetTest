using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.SqlServer
{
	internal class VirtualMindDbContext : DbContext
	{
		private readonly string Connection;
		public VirtualMindDbContext(string connection)
		{
			Connection = connection;
		}
		public DbSet<TransferHistory> Transfers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Connection);
		}
	}
}
