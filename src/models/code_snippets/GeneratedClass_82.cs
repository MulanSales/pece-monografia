private class JoystickDevice
{
/// <summary>
/// Amount of axes supported by OpenTK.
/// </summary>
public const int MAX_AXES = 64;

/// <summary>
/// Amount of buttons supported by OpenTK.
/// </summary>
public const int MAX_BUTTONS = 64;

/// <summary>
/// Amount of hats supported by OpenTK.
/// </summary>
public const int MAX_HATS = 4;

/// <summary>
/// Amount of movement around the "centre" of the axis that counts as moving within the deadzone.
/// </summary>
private const float deadzone_threshold = 0.05f;
}