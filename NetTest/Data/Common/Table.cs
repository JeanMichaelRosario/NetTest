using Data.Interfaces;
using System.Collections.Generic;

namespace Data.Common
{
	public abstract class Table<T> : ITable<T>
	{
		protected readonly string Connection;
		
		public Table(string conn)
		{
			Connection = conn;
		}

		public abstract void Add(T obj);
		
		public abstract void Update(int id, T obj);
		
		public abstract void Remove(T obj);
		
		public abstract IEnumerable<T> GetAll();
	}
}
