/// <summary>
///     Determines whether a specified characters occurs within this string. A parameter
///     specifies the culture, case, and sort rules used in the comparison.
/// </summary>
/// <param name="source">
///     The string to browse.
/// </param>
/// <param name="comparisonType">
///     One of the enumeration values that specifies the rules for the search.
/// </param>
/// <param name="targets">
///     The sequence of characters to seek.
/// </param>
public static bool ContainsEx(this string source, StringComparison comparisonType, params char[] targets)
{
try
{
var r = targets.Any(x => source.IndexOf(x.ToString(), 0, comparisonType) != -1);
return r;
}
catch
{
return false;
}
}
