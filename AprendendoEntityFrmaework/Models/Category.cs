using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
