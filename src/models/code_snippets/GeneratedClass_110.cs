public class BalanceControlService
{
    public decimal TotalBalance { get; set; }

    public decimal GetTotalBalance(Account account)
    {
        return account.BankBalance + account.CreditBalance;
    }
}
