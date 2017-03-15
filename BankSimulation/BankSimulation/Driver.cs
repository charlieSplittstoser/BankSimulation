using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulation
{
    class Driver
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test1");
            BankAccount account = new BankAccount();
            Console.WriteLine("Test2");
            UserInteraction.displayWelcomeMessage();
            account = UserInteraction.handleAccountSelection(UserInteraction.displayWelcomeOptions());
            UserInteraction.displayAccountOptions(account);

        }
    }
}
