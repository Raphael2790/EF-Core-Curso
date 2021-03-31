using AprendendoEntityFramework.Repositorios.Interfaces;
using AprendendoEntityFrmaework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AprendendoEntityFramework.Repositorios
{
    public class CategoryRepository : ICategoryRepository
    {
        private ProductsRegionDbContext _context;
        public CategoryRepository(ProductsRegionDbContext context)
        {
            _context = context;
        }
        public void DeleteCategory()
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
