using System;
using System.Collections.Generic;

namespace KBankATM
{
    public enum TransactionType
    {
        Withdrawal
    }

    // Class to store customer details and transactions
    public class Customer
    {
        public string Name { get; set; }
        public bool IsKBankCustomer { get; set; }
        public decimal Balance { get; set; }
        public string Pin { get; set; }
        public List<Transaction.Transaction> Transactions { get; set; }

        public Customer(string name, bool isKBankCustomer, decimal balance, string pin)
        {
            Name = name;
            IsKBankCustomer = isKBankCustomer;
            Balance = balance;
            Pin = pin;
            Transactions = new List<Transaction.Transaction>();
        }

        public void AddTransaction(Transaction.Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }

    

    // ATM class to handle operations
    public class ATM
    {
        private Dictionary<string, Customer> customers;
        private int pinAttempts;
        private const int MaxPinAttempts = 3;

        public ATM()
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
            customer.AddTransaction(new Transaction.Transaction(TransactionType.Withdrawal, amount, customer.Balance));
            Console.WriteLine($"Withdrawal successful. New balance: {customer.Balance}");
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();

            // Adding customers
            Console.WriteLine("Enter details for K Bank customer:");
            Console.Write("Name: ");
            string kBankName = Console.ReadLine();
            Console.Write("Initial Balance: ");
            decimal kBankBalance = decimal.Parse(Console.ReadLine());
            Console.Write("PIN: ");
            string kBankPin = Console.ReadLine();
            Customer kBankCustomer = new Customer(kBankName, true, kBankBalance, kBankPin);
            atm.AddCustomer(kBankCustomer);

            Console.WriteLine("Enter details for Other Bank customer:");
            Console.Write("Name: ");
            string otherBankName = Console.ReadLine();
            Console.Write("Initial Balance: ");
            decimal otherBankBalance = decimal.Parse(Console.ReadLine());
            Console.Write("PIN: ");
            string otherBankPin = Console.ReadLine();
            Customer otherBankCustomer = new Customer(otherBankName, false, otherBankBalance, otherBankPin);
            atm.AddCustomer(otherBankCustomer);

            // Simulating withdrawals
            Console.WriteLine("Welcome to K Bank ATM!");

            Console.WriteLine("Enter customer name to withdraw:");
            string withdrawCustomerName = Console.ReadLine();
            Console.WriteLine($"Enter PIN for {withdrawCustomerName}:");
            string withdrawPin = Console.ReadLine();

            if (atm.ValidatePin(withdrawCustomerName, withdrawPin))
            {
                Console.Write("Enter amount to withdraw: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                atm.Withdraw(withdrawCustomerName, withdrawAmount);
            }

            // Print transactions for K Bank customer
            atm.PrintTransactions(kBankName);
        }
    }
}
