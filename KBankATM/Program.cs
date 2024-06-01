using KBankATM.Services;
using System;
using System.Collections.Generic;

namespace KBankATM
{
    class Program
    {
        static void Main(string[] args)
        {
            ATMService atm = new ATMService();
            atm.PreloadCustomers();

            try
            {
                Console.WriteLine("Welcome to K Bank ATM!");

                Console.Write("Enter customer name: ");
                string customerName = Console.ReadLine();

                if (string.IsNullOrEmpty(customerName))
                {
                    throw new ArgumentException("Customer name cannot be empty.");
                }

                Console.Write($"Enter PIN for {customerName}: ");
                string pin = Console.ReadLine();

                if (string.IsNullOrEmpty(pin))
                {
                    throw new ArgumentException("PIN cannot be empty.");
                }

                if (atm.ValidatePin(customerName, pin))
                {
                    Console.WriteLine("Select transaction type:");
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Deposit");

                    if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
                    {
                        throw new ArgumentException("Invalid transaction type.");
                    }

                    Console.Write("Enter amount: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                    {
                        throw new ArgumentException("Invalid amount.");
                    }

                    if (choice == 1)
                    {
                        atm.Withdraw(customerName, amount);
                    }
                    else if (choice == 2)
                    {
                        atm.Deposit(customerName, amount);
                    }

                    atm.PrintTransactions(customerName);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
