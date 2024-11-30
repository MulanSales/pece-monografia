private bool _isLoading;
/// <summary>
/// <code>true</code> if store is loading. Useful to show a progress indicator.
/// </summary>
public bool IsLoading {
get { return _isLoading; }
internal set {
_isLoading = value;
if (PropertyChanged != null) {
PropertyChanged(this, new PropertyChangedEventArgs("IsLoading"));
}
}
}