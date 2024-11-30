private static void PrintRegexMatch(Match m)
{
var matchCount = 0;
while (m.Success)
{
Console.WriteLine("Match" + (++matchCount));
for (int i = 1; i <= 2; i++)
{
var g = m.Groups[i];
Console.WriteLine("Group" + i + "='" + g + "'");

var cc = g.Captures;
for (int j = 0; j < cc.Count; j++)
{
var c = cc[j];
Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
}
}
m = m.NextMatch();
}
}
