using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulation
{
    class BankAccount
    {

        /* Constants */
        const int SAVINGS = 0;
        const int CHECKING = 1;
        
        /******** Constructors ********/
        public BankAccount()
        {
            this.firstName = "N/A";
            this.lastName = "N/A";
            this.accountBalance[SAVINGS] = 0;
            this.accountBalance[CHECKING] = 0;
        }

        public BankAccount(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.accountBalance[SAVINGS] = 0;
            this.accountBalance[CHECKING] = 0;
        }


        /************************ Methods *********************************/
        
        /**********************************
         * Function: generateAccountNumber
         * Description: Checks latest account number and returns that number + 1
         * PostCond: Assigns an account # to the current account objct in scope
         *********************************/ 
        public int generateAccountNumber()
        {
            int accountNumber = 0;
            string path = @"../../accounts/latestAccountNumber.txt";

            /* Read Latest Account Number */
            using (StreamReader sr = File.OpenText(path))
            {
                accountNumber = Convert.ToInt32(sr.ReadLine()) + 1;
            }

            /* Overwrite Latest Account Number */
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(accountNumber);
            }

            this.accountNumber = accountNumber;

                return accountNumber;
        }

        /**********************************
         * Function: saveAccountData
         * Description: creates a file names account# and saves info to it
         * PostCond: Save first, last, account #, and account balances to the file
         *********************************/
        public void saveAccountData()
        {
            string path = @"../../accounts/accounts/" + this.accountNumber + ".txt";

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Account Number: " + this.accountNumber);
                sw.WriteLine("Account Holder Name: " + this.firstName + " " + this.lastName);
                sw.WriteLine("Savings: " + this.accountBalance[SAVINGS]);
                sw.WriteLine("Checking: " + this.accountBalance[CHECKING]);
            }
        }


        /****************************
        * Funciton: handleAccountOptionsSelection
        * Description: handles user selection for if they want to:
        *              - Check balance
        *              - Deposit
        *              - Withdrawal
        *              - Exit program
        ***************************/
        public void handleAccountOptions(int userSelection)
        {
            switch (userSelection)
            {
                /* Exit the program */
                case 0:
                    Console.WriteLine("The program will now exit. Thanks for doing business!");
                    System.Environment.Exit(1107);
                    break;
                case 1:
                    this.checkBalance();
                    break;
                case 2:
                    // Make a deposit
                    Console.WriteLine("Make a deposit");
                    break;
                case 3:
                    // Make a withdrawal
                    Console.WriteLine("Make a withdrawal");
                    break;
            }
        }

        void checkBalance()
        {
            Console.Clear();

            Console.WriteLine("Account balance for " + this.getAccountName(true) + ":");
            Console.WriteLine("Account Number: " + this.getAccountNumber());
            Console.WriteLine("Savings: $" + this.getBalance(SAVINGS));
            Console.WriteLine("Checking: $" + this.getBalance(CHECKING));
        }


        /******** Getters & Setters ********/

        public int getAccountNumber()
        {
            return this.accountNumber;
        }

        public void setAccountNumber(int accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public void setBalance(int account, double balance)
        {
            this.accountBalance[account] = balance;
        }

        public double getBalance(int account)
        {
            return this.accountBalance[account];
        }

        public string getAccountName(bool fullName)
        {
            if (fullName)
                return this.firstName + " " + this.lastName;
            else
                return this.firstName;
        }

        /******** Data Members *************/
        private double[] accountBalance = new double[2];
        private string firstName;
        private string lastName;
        private int accountNumber;


    }
}
