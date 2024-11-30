public class AddressConverter
{
    public string GetAddress(Customer customer)
    {
        return $"{customer.Street}, {customer.City}, {customer.ZipCode}";
    }
}