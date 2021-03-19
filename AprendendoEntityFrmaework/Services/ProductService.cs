using AprendendoEntityFramework.Repositorios.Interfaces;
using AprendendoEntityFramework.Services.Interfaces;
using AprendendoEntityFrmaework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AprendendoEntityFramework.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICollection<Product> GetProductsWithLowerPrice()
        {
            return _productRepository.GetProducts().Where(x => x.ProductPrice <= 3.0M).ToList();
        }
    }
}
