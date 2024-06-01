using KBankATM.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBankATM.Models
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }

        public Transaction(TransactionType type, decimal amount, decimal balanceAfter)
        {
            Type = type;
            Amount = amount;
            BalanceAfter = balanceAfter;
        }
    }

}
