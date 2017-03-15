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
         * Funciton: handleAccountSelection
         * Description: handles logic based on if user has an account or needs to create one
         * PostCond: BankAccount object instantiated from either existing or new bank account
         ***************************/
        public static BankAccount handleAccountSelection(int userSelection)
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
         *              - Withdrawl
         *              - Exit program
         ***************************/
        public static void displayAccountOptions(BankAccount account)
        {
            int userSelection = 0;
            Console.WriteLine("Welcome " + account.getAccountName(false) + "! How can we help you?");
            Console.WriteLine("1.) Check my Balance");
            Console.WriteLine("2.) Make a deposit");
            Console.WriteLine("3.) Make a withdrawl");
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
            //Add file processing & logic
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
