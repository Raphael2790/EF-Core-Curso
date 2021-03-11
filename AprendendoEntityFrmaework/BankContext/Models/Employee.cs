using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }

        public virtual Address Address {get; set;}
    }
}
