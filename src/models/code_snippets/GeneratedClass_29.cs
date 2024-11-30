var RunProcess = new Action<FilePath, ProcessSettings> ((process, settings) =>
{
var result = StartProcess(process, settings);
if (result != 0) {
throw new Exception ("Process '" + process + "' failed with error: " + result);
}
});
