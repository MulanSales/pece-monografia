// HTMLContent
public string _HTMLContent;
public string HTMLContent
{
get
{
return _HTMLContent;
}
set
{
Set(() => HTMLContent, ref _HTMLContent, value);

}
}
