/// <summary>
/// Installs the package.
/// </summary>
/// <param name="packageManager">The package manager.</param>
/// <param name="fileSystem">The file system.</param>
/// <param name="packageId">The package id.</param>
/// <param name="version">The version.</param>
/// <param name="allowPreReleaseVersion"> </param>
/// <returns></returns>
internal bool InstallPackage(PackageManager packageManager, IFileSystem fileSystem, string packageId, SemanticVersion version, Boolean allowPreReleaseVersion = true)
{
if (packageManager.IsPackageInstalled(packageId, version))
{
return false;
}