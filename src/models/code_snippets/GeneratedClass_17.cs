private struct DepthData
{
public DepthData(ushort depth, bool isObstacle)
{
Depth = depth;
IsObstacle = isObstacle;
}

public ushort Depth { get; }
public bool IsObstacle { get; }
}