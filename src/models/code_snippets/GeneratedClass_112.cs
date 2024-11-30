public class DatabaseService
{
    public void ConfigureDatabase(string server, int port, string username, string password, string databaseName, bool useSSL)
    {
        Console.WriteLine($"Configuring database at {server}:{port} using SSL: {useSSL}");
        Console.WriteLine($"User: {username}, Database: {databaseName}");
    }
}

var dbService = new DatabaseService();
dbService.ConfigureDatabase("localhost", 5432, "admin", "password123", "MyDatabase", true);
