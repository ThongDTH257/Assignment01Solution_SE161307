using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly FstoreDbContext context;
        public ProductRepository(FstoreDbContext context) : base(context) 
        {
            this.context = context;
        }

        public List<Product> FilterSearch(string? name, decimal? minPrice, decimal? maxPrice)
        {
            var query = context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(name) ) 
                query = query.Where(p=>p.ProductName.Contains(name));
            
            if(minPrice.HasValue)
                query = query.Where(p=>p.UnitPrice >= minPrice.Value);
            if(maxPrice.HasValue)
                query = query.Where(p=>p.UnitPrice <= maxPrice.Value);
            return query.ToList();
        }

        public List<ProductView> GetFullInfo()
        {
            var products = context.Products.Include(c=>c.Category).ToList();
            List<ProductView> productsView = new List<ProductView>();
            foreach(var p in products)
            {
                productsView.Add(new ProductView
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    CategoryId = p.CategoryId,
                    Weight = p.Weight,
                    UnitsInStock = p.UnitsInStock,
                    Category = p.Category.CategoryName
                });
            }
            return productsView;
        }
    }
}
