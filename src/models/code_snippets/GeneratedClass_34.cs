
/// <summary>
/// Logs a message as failure. Halts execution.
/// </summary>
[ContractAnnotation("=> halt")]
public static void Fail(string text)
{
throw new Exception(text);
}