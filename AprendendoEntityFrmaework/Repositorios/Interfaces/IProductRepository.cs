using AprendendoEntityFrmaework.Models;
using System.Collections.Generic;

namespace AprendendoEntityFramework.Repositorios.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        void DeleteProduct();
    }
}