public class ReportService
{
    public void GenerateReport(string title, DateTime startDate, DateTime endDate, string author, bool includeCharts, string[] filters)
    {
        Console.WriteLine($"Generating report: {title} by {author}");
        Console.WriteLine($"From {startDate} to {endDate}");
        Console.WriteLine($"Include Charts: {includeCharts}, Filters: {string.Join(", ", filters)}");
    }
}
