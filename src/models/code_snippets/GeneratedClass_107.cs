public class LoanService
{
    public bool IsEligibleForLoan(Customer customer)
    {
        return customer.CreditScore > 700 && customer.AnnualIncome > 50000;
    }
}
