using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.Models
{
    public class BankAgency
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyNumber { get; set; }
        public DateTime OpenedSince { get; set; }

        public virtual Address Adress { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
