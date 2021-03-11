using AprendendoEntityFrmaework.BankContext.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AprendendoEntityFrmaework.BankContext.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountNumber { get; set; }
        private decimal _accountBalance;
        public decimal AccountBalance 
        {
            get { return _accountBalance; }
            set { _accountBalance = CheckValue(value); }
        }
        public DateTime Created_at { get; set; }
        public int BankAgencyId { get; set; }
        public int ClientID { get; set; }
        public EAccountType AccountType { get; set; }


        public virtual Client Client { get; set; }
        public virtual BankAgency BankAgency { get; set; }
        
        public decimal CheckValue(decimal balance)
        {
            return balance;
        }
    }
}
