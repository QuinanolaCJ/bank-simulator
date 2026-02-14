using System;
using ATMApp.Services;

namespace ATMApp.View
{
    public static class BankingView
    {
        public static void Run()
        {
            BankingService service = new BankingService();

            double balance = 1000.00;
            bool running = true;

            Console.WriteLine("=== Simple ATM System ===");
            Console.WriteLine("Name: CJ Quiñanola");
            Console.WriteLine();
            Console.WriteLine($"Initial Balance: ₱{balance:F2}");
            Console.WriteLine();

            while (running)
            {
                ShowMenu();

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ShowInvalid();
                    continue;
                }

                switch (choice)
                {
                    case 1: //Check Balance
                        Console.WriteLine($"Current Balance: ₱{service.CheckBalance(balance):F2}");
                        break;

                    case 2: //Deposit
                        Console.Write("Enter amount to deposit: ");

                        if (!double.TryParse(Console.ReadLine(), out double deposit) ||
                            !service.Deposit(ref balance, deposit))
                        {
                            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                            continue;
                        }

                        Console.WriteLine("Deposit successful.");
                        Console.WriteLine($"Updated Balance: ₱{balance:F2}");
                        break;

                    case 3: //Withdraw
                        Console.Write("Enter amount to withdraw: ");

                        if (!double.TryParse(Console.ReadLine(), out double withdraw))
                        {
                            Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                            continue;
                        }

                        service.Withdraw(ref balance, withdraw, out bool success);

                        if (!success)
                        {
                            if (withdraw <= 0)
                            {
                                Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                                continue;
                            }

                            Console.WriteLine("Withdrawal failed. Insufficient balance.");
                            break;
                        }

                        Console.WriteLine("Withdrawal successful.");
                        Console.WriteLine($"Updated Balance: ₱{balance:F2}");
                        break;

                    case 4: //Mini Statement
                        var data = service.GetMiniStatement(balance);

                        Console.WriteLine("--- Mini Statement ---");
                        Console.WriteLine($"Current Balance: ₱{data.Item1:F2}");
                        Console.WriteLine($"Last Transaction Amount: ₱{data.Item2:F2}");
                        break;

                    case 5: //Exit
                        Console.WriteLine("Thank you for using the ATM. Goodbye!");
                        running = false;
                        break;

                    default:
                        ShowInvalid();
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1: Check Balance");
            Console.WriteLine("2: Deposit Money");
            Console.WriteLine("3: Withdraw Money");
            Console.WriteLine("4: Print Mini Statement");
            Console.WriteLine("5: Exit");
            Console.Write("Select an option: ");
        }

        private static void ShowInvalid()
        {
            Console.WriteLine("Invalid option selected. Please try again.");
        }
    }
}
