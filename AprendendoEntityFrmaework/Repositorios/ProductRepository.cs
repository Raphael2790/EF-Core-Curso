using AprendendoEntityFramework.Repositorios.Interfaces;
using AprendendoEntityFrmaework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AprendendoEntityFramework.Repositorios
{
    public class ProductRepository : IProductRepository
    {
        private ProductsRegionDbContext _context;

        public ProductRepository(ProductsRegionDbContext context)
        {
            _context = context;
        }

        public void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
