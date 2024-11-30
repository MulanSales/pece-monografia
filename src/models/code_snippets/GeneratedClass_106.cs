public class ReportGenerator
{
    public string GetFormattedAddress(Customer customer)
    {
        return $"{customer.Street}, {customer.City}, {customer.ZipCode}";
    }
}