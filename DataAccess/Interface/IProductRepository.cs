using BusinessObject.DTO;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> FilterSearch(string? name, decimal? minPrice, decimal? maxPrice);
        List<ProductView> GetFullInfo();
    }
}
