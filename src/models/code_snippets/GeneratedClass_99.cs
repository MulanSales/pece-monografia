/// <summary>
/// Creates the package manager.
/// </summary>
/// <param name="fileSystem">The file system.</param>
/// <param name="useMachineCache">if set to <c>true</c> [use machine cache].</param>
/// <returns></returns>
protected virtual PackageManager CreatePackageManager(IFileSystem fileSystem, bool useMachineCache = false)
{
var pathResolver = new DefaultPackagePathResolver(fileSystem, useSideBySidePaths: AllowMultipleVersions);
var packageManager = new PackageManager(_repository, pathResolver, fileSystem, new LocalPackageRepository(pathResolver, fileSystem));
packageManager.Logger = Console;

return packageManager;
}
