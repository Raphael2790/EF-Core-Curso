using AprendendoEntityFrmaework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.Services.Interfaces
{
    interface IProductService
    {
        ICollection<Product> GetProductsWithLowerPrice();
    }
}
