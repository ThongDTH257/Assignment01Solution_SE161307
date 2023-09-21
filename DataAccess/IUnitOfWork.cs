using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository Member { get; }
        IProductRepository Product { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
        ICategoryRepository Category { get; }

        int Save();
    }
}
