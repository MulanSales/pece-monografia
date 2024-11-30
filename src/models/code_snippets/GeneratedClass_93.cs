[Test]
public void IsEmpty3()
{
IXLWorksheet ws = new XLWorkbook().Worksheets.Add("Sheet1");
IXLCell cell = ws.Cell(1, 1);
cell.Style.Fill.BackgroundColor = XLColor.Red;
bool actual = cell.IsEmpty();
bool expected = true;
Assert.AreEqual(expected, actual);
}