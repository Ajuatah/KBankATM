using KBankATM.Enum;
using KBankATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBankATM.Services
{
    public class ATMService
    {
        private Dictionary<string, Customer> customers;
        private int pinAttempts;
        private const int MaxPinAttempts = 3;

        public ATMService()
        {
            customers = new Dictionary<string, Customer>();
            pinAttempts = 0;
        }

        public void AddCustomer(Customer customer)
        {
            customers[customer.Name] = customer;
        }

        public bool ValidatePin(string customerName, string pin)
        {
            if (!customers.ContainsKey(customerName))
            {
                Console.WriteLine("Customer not found.");
                return false;
            }

            if (customers[customerName].Pin != pin)
            {
                pinAttempts++;
                if (pinAttempts >= MaxPinAttempts)
                {
                    Console.WriteLine("Card captured due to too many incorrect PIN attempts.");
                    return false;
                }
                Console.WriteLine("Incorrect PIN.");
                return false;
            }

            pinAttempts = 0;
            Console.WriteLine("PIN validated successfully.");
            return true;
        }

        public bool Withdraw(string customerName, decimal amount)
        {
            if (!customers.ContainsKey(customerName))
            {
                Console.WriteLine("Customer not found.");
                return false;
            }

            Customer customer = customers[customerName];
            if (customer.Balance < amount)
            {
                Console.WriteLine("Insufficient balance.");
                return false;
            }

            customer.Balance -= amount;
            customer.AddTransaction(new Transaction(TransactionType.Withdrawal, amount, customer.Balance));
            Console.WriteLine($"Withdrawal successful. New balance: {customer.Balance}");
            return true;
        }

        public bool Deposit(string customerName, decimal amount)
        {
            if (!customers.ContainsKey(customerName))
            {
                Console.WriteLine("Customer not found.");
                return false;
            }

            Customer customer = customers[customerName];
            customer.Balance += amount;
            customer.AddTransaction(new Transaction(TransactionType.Deposit, amount, customer.Balance));
            Console.WriteLine($"Deposit successful. New balance: {customer.Balance}");
            return true;
        }

        public void PrintTransactions(string customerName)
        {
            if (!customers.ContainsKey(customerName))
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Customer customer = customers[customerName];
            Console.WriteLine($"Transactions for {customer.Name}:");
            foreach (var transaction in customer.Transactions)
            {
                Console.WriteLine($"Type: {transaction.Type}, Amount: {transaction.Amount}, Balance After: {transaction.BalanceAfter}");
            }
        }

        public void PreloadCustomers()
        {
            AddCustomer(new Customer("Andropov", true, 5000, "1234"));
            AddCustomer(new Customer("Bob", false, 3000, "5678"));
            AddCustomer(new Customer("Charlie", true, 7000, "9101"));
        }
    }
}
