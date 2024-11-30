public class RegistrationService
{
    public string GetFullName(User user)
    {
        return $"{user.FirstName} {user.LastName}";
    }
}

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}