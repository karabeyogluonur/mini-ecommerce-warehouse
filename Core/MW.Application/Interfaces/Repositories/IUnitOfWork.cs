using MW.Application.Interfaces.Repositories.CustomRepositories;
using MW.Application.Interfaces.Repositories.CustomRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get;}
        IStockHistoryRepository StockHistories { get;}
    }
}
