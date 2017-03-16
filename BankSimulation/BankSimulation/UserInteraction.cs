using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSimulation
{
    class UserInteraction
    {

        /* Constants */
        const int SAVINGS = 0;
        const int CHECKING = 1;

        /*****************************
         * Function: displayWelcomeMessage
         * Description: Displays the welcome message for Splittstoser Banks
         ****************************/
        public static void displayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Splittstoser Banks! How can we help you?\n");
        }


        /*****************************
         * Function: displayUserOptions
         * Description: Determines if user has an account or needs to create one
         * PostCond: User selection option which will either exit, create account, or find account
         ****************************/
        public static int displayWelcomeOptions()
        {
            int userSelection = 0;
            Console.WriteLine("1.) I would like to create a new bank account.");
            Console.WriteLine("2.) I would like to access an existing bank account.");
            Console.Write("Please enter 1 or 2 (Any other entry will exit): ");
            try
            {
                userSelection = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) { }

            return userSelection;
        }


        /****************************
         * Funciton: handleWelcomeOptions
         * Description: handles logic based on if user has an account or needs to create one
         * PostCond: BankAccount object instantiated from either existing or new bank account
         ***************************/
        public static BankAccount handleWelcomeOptions(int userSelection)
        {
            BankAccount b = new BankAccount();
            switch (userSelection)
            {
                /* Exit the program */
                case 0:
                    Console.WriteLine("The program will now exit. Thanks for doing business!");
                    System.Environment.Exit(1107);
                    break;
                case 1:
                    b = createBankAccount();
                    break;
                case 2:
                    b = searchForUserAccount();
                    break;
            }
            return b;
        }


        /****************************
         * Function: displayAccountOptions
         * Description: Displays bank account options
         *              - Check balance
         *              - Deposit
         *              - Withdrawal
         *              - Exit program
         ***************************/
        public static void displayAccountOptions(BankAccount account)
        {
            Console.Clear();
            int userSelection = 0;

            Console.WriteLine("Account Name: " + account.getAccountName(true));
            Console.WriteLine("Account Number: " + account.getAccountNumber());

            Console.WriteLine("\nWelcome " + account.getAccountName(false) + "! How can we help you?");
            Console.WriteLine("1.) Check my Balance");
            Console.WriteLine("2.) Make a deposit");
            Console.WriteLine("3.) Make a withdrawal");
            Console.WriteLine("Please enter a number to select an option. Entering an invalid number will exit.");

            
            
            try
            {
                userSelection = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) { }

            account.handleAccountOptions(userSelection);
        }


        /**************************************
        * Function: searchForUserAccount
        * Description: searches account list for an account that matches the account #
        * PostCond: Bank account object with account #, first & last name of user
        **************************************/
        static BankAccount searchForUserAccount()
        {
            BankAccount account = new BankAccount();
            bool invalidNumber = false;
            String message, userInput;
            String path;

            Console.Clear();
            Console.WriteLine("Welcome Back! Please enter your account number so we can find your information!");
            Console.WriteLine("Enter 0 if you wish to exit.");

            /* Let user enter an account # and search for their account */
            do
            {
                message = invalidNumber ? "Could not find account. Enter account number: " : "Account Number: ";
                Console.Write(message);
                userInput = Console.ReadLine();
                path = @"../../accounts/accounts/" + userInput + ".txt";
                invalidNumber = true;
                if (userInput.Equals("0"))
                {
                    Console.Clear();
                    Console.WriteLine("Exiting Program.... Thank you for banking with us!");
                    System.Environment.Exit(1107);
                }

            } while (!File.Exists(path));


            /* Load account info from file */
            StreamReader reader = File.OpenText(path);
            string line;
            int lineNumber = 1;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(':');

                switch(lineNumber)
                {
                    /* Load account number */
                    case 1:
                        account.setAccountNumber(Convert.ToInt32(items[1].Trim()));
                        break;

                    /* Load account name */
                    case 2:
                        items = items[1].TrimStart().Split(' ');
                        account.setFirstName(items[0]);
                        account.setLastName(items[1]);
                        break;

                    /* Load savings balance */
                    case 3:
                        account.setBalance(SAVINGS, Convert.ToDouble(items[1].Trim()));
                        break;
                    case 4:
                        account.setBalance(CHECKING, Convert.ToDouble(items[1].Trim()));
                        break;
                    
                } // end switch

                
                 
                lineNumber++;

            } //end while

            reader.Close();

            return account;
        }


        /**************************************
         * Function: createBankAccount
         * Description: creates a new bank account for a new customer
         * PostCond: Bank account object with account #, first & last name
         **************************************/
        static BankAccount createBankAccount()
        {
            string firstName;
            string lastName;

            Console.Clear();
            Console.WriteLine("Thank you for your interest in Splittstoser Banks!");
            Console.WriteLine("Please answer the following questions:");
            Console.Write("What is your first name?: ");
            firstName = Console.ReadLine();
            Console.Write("What is your last name?: ");
            lastName = Console.ReadLine();

            BankAccount newBankAccount = new BankAccount(firstName, lastName);

            newBankAccount.generateAccountNumber();
            newBankAccount.saveAccountData();
            

                return newBankAccount;

        }
    }
}
