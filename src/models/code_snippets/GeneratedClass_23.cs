
public string Name
{
get
{
return this.loggerName;
}
}

/// <summary>
/// Log level property
/// </summary>
public LogLevel Level
{
get
{
return this.logLevel;
}
set
{
this.logLevel = value;
}
}