public Report GenerateReport(
    DateTime startDate,
    DateTime endDate,
    string[] accountNumbers,
    string reportType,
    string currency,
    string[] costCenters,
    string[] projectCodes,
    bool includeDetails,
    bool includeTransactions,
    string approverName,
    bool isFinalVersion)
{
    // Validação dos parâmetros
    if (startDate >= endDate)
        throw new ArgumentException("Start date must be earlier than end date.");
    if (accountNumbers == null || accountNumbers.Length == 0)
        throw new ArgumentException("At least one account number must be provided.");
    if (string.IsNullOrWhiteSpace(reportType))
        throw new ArgumentException("Report type is required.");
    if (string.IsNullOrWhiteSpace(currency))
        throw new ArgumentException("Currency is required.");
    if (string.IsNullOrWhiteSpace(approverName))
        throw new ArgumentException("Approver name is required.");

    // Simulando a busca de transações com base nos parâmetros
    var transactions = new List<string>();
    if (includeTransactions)
    {
        foreach (var account in accountNumbers)
        {
            transactions.Add($"Transaction for Account: {account}, Date: {DateTime.Now}, Amount: 1000 {currency}");
        }
    }

    // Gerar título do relatório
    var title = $"{reportType} Report - {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}";

    // Simulação de detecção de status baseado em dados
    var status = isFinalVersion ? "Approved" : "Draft";

    // Retornar o relatório gerado
    return new Report
    {
        Title = title,
        StartDate = startDate,
        EndDate = endDate,
        Currency = currency,
        GeneratedBy = approverName,
        GeneratedOn = DateTime.Now,
        Transactions = transactions,
        Status = status,
        IsFinalVersion = isFinalVersion
    };
}
