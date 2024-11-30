try
{
await _certifyClient.GetAppVersion();
isAvailable = true;
}
catch (Exception)
{
isAvailable = false;
}

