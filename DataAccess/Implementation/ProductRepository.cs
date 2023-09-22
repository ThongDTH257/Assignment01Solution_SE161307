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

        public List<Product> GetFullInfo()
        {
            var products = new List<Product>();
            products = context.Products.ToList();
            products.ForEach(p =>
            {
                p.Category = context.Categories.Find(p.CategoryId);
            });
            return products;
        }
    }
}
