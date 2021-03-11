using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.Models
{
    //Sempre devemos criar uma tabela de associação entre entidades com relacionamentos muitos para muitos
    public class BanksClients
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public DateTime AssociatedAt { get; set; }
    }
}
