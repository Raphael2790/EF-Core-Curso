using AprendendoEntityFrmaework.BankContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AprendendoEntityFrmaework
{
    [Table("Adresses", Schema ="Admin")]
    public class Address
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [MaxLength(200)]
        [Column("StreetName", Order =2,TypeName ="nvarchar(200)")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(200)]
        [Column("City", Order = 3, TypeName = "nvarchar(200)")]
        public string City { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(200)]
        [Column("ZipCode", Order = 5, TypeName = "nvarchar(200)")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200)]
        [Column("Country", Order = 4, TypeName = "nvarchar(200)")]
        public string Country { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }

    }
}
