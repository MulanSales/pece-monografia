private void Dispose(bool disposing)
{
// Check to see if Dispose has already been called.
if (!disposed)
{
// If disposing equals true, dispose all managed
// and unmanaged resources.
if (disposing)
{
// Dispose managed resources.
}

// Call the appropriate methods to clean up
// unmanaged resources here.
NativeMethods.git_revwalk_free(walker);

// Note disposing has been done.
disposed = true;
}
}