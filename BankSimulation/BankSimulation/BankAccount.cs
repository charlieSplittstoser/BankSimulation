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
                sr.Close();
            }

            /* Overwrite Latest Account Number */
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(accountNumber);
                sw.Close();
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
                sw.Close();
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
                    this.makeDeposit();
                    // Console.WriteLine("Make a deposit");
                    break;
                case 3:
                    this.makeWithdrawal();
                    //Console.WriteLine("Make a withdrawal");
                    break;
            }
        }



        /**********************************
         * Function: makeDeposit
         * Description: Let's a user make a deposit
         * PostCond: Adds x dollar amount into either checkings or savings account
         *********************************/
        void makeDeposit()
        {
            string userOption;
            string userInput;
            double depositAmount;
            bool isDouble = false;

            Console.Clear();
            Console.WriteLine("Account Number: " + this.getAccountNumber());
            Console.WriteLine("\n" + this.getAccountName(false) + ", which account would you like to deposit money into?");
            Console.WriteLine("1.) Savings ($" + this.getBalance(SAVINGS) + ")");
            Console.WriteLine("2.) Checking ($" + this.getBalance(CHECKING) + ")");

            /* User selection */
            do
            {
                Console.WriteLine("Please enter a valid option(Enter 0 to quit):");
                userOption = Console.ReadLine();
            } while (!userOption.Equals("0") && !userOption.Equals("1") && !userOption.Equals("2"));


            /* Deposit functionality based on user selection */
            switch(userOption)
            {
                case "0":
                    Console.WriteLine("Thank you for banking with us! Exiting the program ... ");
                    System.Environment.Exit(1107);
                    break;
                case "1":
                case "2":
                    string account = userOption == "1" ? "savings" : "checking";
                    do
                    {
                        Console.WriteLine("How much would you like to deposit into your " + account + " account?");
                        Console.Write("$");
                        userInput = Console.ReadLine();
                        isDouble = Double.TryParse(userInput, out depositAmount);
                    } while (!isDouble);

                    int accountType = Convert.ToInt32(userOption) - 1; 
                    this.setBalance(accountType, this.getBalance(accountType) + depositAmount);

                    this.saveAccountData();
                    
                    break;

                    
            }
        }



        /**********************************
         * Function: makeWithdrawal
         * Description: Let's a user make a withdrawal
         * PostCond: subtracts x dollar amount into either checkings or savings account
         *********************************/
        void makeWithdrawal()
        {
            string userOption;
            string userInput;
            double withdrawalAmount;
            bool isDouble = false;

            Console.Clear();
            Console.WriteLine("Account Number: " + this.getAccountNumber());
            Console.WriteLine("\n" + this.getAccountName(false) + ", which account would you like to withdraw money from?");
            Console.WriteLine("1.) Savings ($" + this.getBalance(SAVINGS) + ")");
            Console.WriteLine("2.) Checking ($" + this.getBalance(CHECKING) + ")");

            /* User selection */
            do
            {
                Console.WriteLine("Please enter a valid option(Enter 0 to quit):");
                userOption = Console.ReadLine();
            } while (!userOption.Equals("0") && !userOption.Equals("1") && !userOption.Equals("2"));


            /* Deposit functionality based on user selection */
            switch (userOption)
            {
                case "0":
                    Console.WriteLine("Thank you for banking with us! Exiting the program ... ");
                    System.Environment.Exit(1107);
                    break;
                case "1":
                case "2":
                    string account = userOption == "1" ? "savings" : "checking";
                    do
                    {
                        Console.WriteLine("How much would you like to withdraw from your " + account + " account?");
                        Console.Write("$");
                        userInput = Console.ReadLine();
                        isDouble = Double.TryParse(userInput, out withdrawalAmount);
                    } while (!isDouble);

                    int accountType = Convert.ToInt32(userOption) - 1;
                    if (withdrawalAmount > this.getBalance(accountType))
                        this.setBalance(accountType, 0);
                    else
                        this.setBalance(accountType, this.getBalance(accountType) - withdrawalAmount);

                    this.saveAccountData();

                    break;


            }
        }


        /**********************************
         * Function: checkBalance
         * Description: Let's a user check their balance in both savings and checking accounts
         *********************************/
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
