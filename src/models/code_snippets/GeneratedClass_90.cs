/// <summary>
/// Image Version to be generated automatically
/// </summary>
/// <remarks>
/// Specify either Width or Height you don't need to have both.
/// </remarks>
public interface IImageVersion
{
#region Properties
/// <summary>
/// Image Width
/// </summary>
int Width
{
get;
set;
}

/// <summary>
/// Image Height
/// </summary>
int Height
{
get;
set;
}
#endregion
}