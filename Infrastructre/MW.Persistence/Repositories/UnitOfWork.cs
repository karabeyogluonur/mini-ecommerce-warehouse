using MW.Application.Interfaces.Repositories;
using MW.Application.Interfaces.Repositories.CustomRepositories;
using MW.Application.Interfaces.Repositories.CustomRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository, IProductRepository productRepository, IStockHistoryRepository stockHistoryRepository)
        {
            Users = userRepository;
            Products = productRepository;
            StockHistories = stockHistoryRepository;
        }

        public IUserRepository Users { get;}
        public IProductRepository Products { get; }

        public IStockHistoryRepository StockHistories { get; }
    }
}
