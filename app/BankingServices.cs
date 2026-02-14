namespace ATMApp.Services
{
    public class BankingServices
    {
        private double lastTransaction = 0;

        // Option 1: Pass-b-valyue
        public double CheckBalance(double balance)
        {
            return balance;
        }
        // Option 2: ref (Deposit)
        public bool Deposit(ref double balance, double amount)
        {
            if (amount <= 0)
                return false;

            balance += amount;
            lastTransaction = amount;
            return true;
        }
        // Option 3: ref + out (Withdraw)
        public void Withdraw(ref double balance, double amount, out bool success)
        {
            if (amount <= 0 || amount > balance)
            {
                success = false;
                return;
            }

            balance -= amount;
            lastTransaction = -amount;
            success = true;
        }

        public (double, double) GetMiniStatement(double balance)
        {
            return (balance, lastTransaction);
        }
    }
}
