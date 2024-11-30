public class AccountingService
{
    private readonly DatabaseContext _dbContext;

    public AccountingService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void RecordTransaction(
        string accountNumber,
        DateTime transactionDate,
        string description,
        decimal debitAmount,
        decimal creditAmount,
        string currency,
        string transactionType,
        string costCenter)
    {
        // Validação básica
        if (debitAmount < 0 || creditAmount < 0)
            throw new ArgumentException("Debit or Credit amounts cannot be negative.");

        // Criação e persistência da transação
        var transaction = new FinancialTransaction
        {
            AccountNumber = accountNumber,
            TransactionDate = transactionDate,
            Description = description,
            DebitAmount = debitAmount,
            CreditAmount = creditAmount,
            Currency = currency,
            TransactionType = transactionType,
            CostCenter = costCenter
        };

        _dbContext.FinancialTransactions.Add(transaction);
        _dbContext.SaveChanges();
    }
}