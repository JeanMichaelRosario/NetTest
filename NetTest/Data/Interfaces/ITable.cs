using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
	public interface ITable<T>
	{
		void Add(T obj);

		void Update(int id, T obj);

		void Remove(T obj);

		IEnumerable<T> GetAll();
	}
}
