/// <summary>
/// Initializes a new instance of the <see cref="Line" /> class.
/// </summary>
/// <param name="scintilla">The <see cref="Scintilla" /> control that created this line.</param>
/// <param name="index">The index of this line within the <see cref="LineCollection" /> that created it.</param>
public Line(Scintilla scintilla, int index)
{
this.scintilla = scintilla;
Index = index;
}