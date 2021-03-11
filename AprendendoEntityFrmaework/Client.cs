using AprendendoEntityFrmaework.BankContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Alguns atributos: Key, Required, MaxLength, StringLength, Timestamp
using System.ComponentModel.DataAnnotations.Schema;//ALguns atributos: Foreign Key, Not Mapped, Column, Table, Databased Generated

//Data Annotations são mais utilizados para validar modelos de entra de dados DTO , Models etc
namespace AprendendoEntityFrmaework
{
    [Table("Client", Schema ="Admin")]
    public class Client
    {
        //Database Generate Computed recalcula a propriedade auto gerada sempre que modificada
        //Entity mapea apenas propriedades não metodos nem atributos
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        [Column("ClientName", TypeName = "varchar(200)")]
        [Required(ErrorMessage ="Campo {0} obrigatório")]
        [MaxLength(200)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegisterDate { get; set; }
        [NotMapped] //Not mapped não mapeará a propriedade para a tabela
        public bool HasRegisteredEven { get; set; }

        //Campo definido para atabela de associação para informar um relacionamento muito para muitos
        public virtual ICollection<BanksClients> BankClients { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Address> Address { get; set; }

        void UpdateName() { }
    }
}
