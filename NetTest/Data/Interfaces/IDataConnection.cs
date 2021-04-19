using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IDataConnection
    {
        ITable<TransferHistory> GetTransfers();
    }
}
