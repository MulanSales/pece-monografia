/// <summary>
/// Method for adding new cells to the ActualGeneration.
/// </summary>
/// <param name="cell">Send the cell you want to add.</param>
public void AddCell(Cell cell)
{
ActualGeneration.SetValue(cell, cell.X, cell.Y);
}