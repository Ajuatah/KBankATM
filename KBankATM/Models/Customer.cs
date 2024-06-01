using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBankATM.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public bool IsKBankCustomer { get; set; }
        public decimal Balance { get; set; }
        public string Pin { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Customer(string name, bool isKBankCustomer, decimal balance, string pin)
        {
            Name = name;
            IsKBankCustomer = isKBankCustomer;
            Balance = balance;
            Pin = pin;
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
