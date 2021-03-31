using AprendendoEntityFrmaework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.Repositorios.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        void DeleteCategory();
    }
}
