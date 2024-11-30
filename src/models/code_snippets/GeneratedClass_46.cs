// Split message into parts to avoid SQL errors.
for (var i = 0; i < message.Length; i += 3000)
{
var length = Math.Min(3000, message.Length - i);
var part = message.Substring(i, length);
if (parts.Count > 0)
{
part = "(Truncated message continued) " + part;
}
parts.Add(part);
}