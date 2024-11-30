private bool GestureMagnify(IMouseDevice device, ulong timestamp, IInputRoot root, Point p,
PointerPointProperties props, Vector delta, KeyModifiers inputModifiers)
{
device = device ?? throw new ArgumentNullException(nameof(device));
root = root ?? throw new ArgumentNullException(nameof(root));

var hit = HitTest(root, p);

if (hit != null)
{
var source = GetSource(hit);
var e = new PointerDeltaEventArgs(Gestures.PointerTouchPadGestureMagnifyEvent, source,
_pointer, root, p, timestamp, props, inputModifiers, delta);

source?.RaiseEvent(e);
return e.Handled;
}

return false;
}