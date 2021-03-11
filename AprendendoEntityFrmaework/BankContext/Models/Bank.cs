using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.Models
{
    public class Bank
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }

        public virtual ICollection<BankAgency> Agencies { get; set; }
        //Campo definido para atabela de associação para informar um relacionamento muito para muitos
        public virtual ICollection<BanksClients> BankClients { get; set; }
    }
}
