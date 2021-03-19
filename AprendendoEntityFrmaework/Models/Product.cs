using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AprendendoEntityFrmaework.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int CategoryID { get; set; }
        //public byte[] RowVersion { get; set; }

        public virtual Category Category { get; set; }
    }
}
