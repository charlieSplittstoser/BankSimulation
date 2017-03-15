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
            BankAccount account = new BankAccount();
            UserInteraction.displayWelcomeMessage();
            account = UserInteraction.handleAccountSelection(UserInteraction.displayWelcomeOptions());
            UserInteraction.displayAccountOptions(account);

        }
    }
}
