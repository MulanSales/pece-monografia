string[] dirs = { "data", "foreign_keys", "functions",
"indexes", "procs", "tables", "triggers" };
foreach (string dir in dirs) {
if (!Directory.Exists(args[2] + "/" + dir)) {
Directory.CreateDirectory(args[2] + "/" + dir);
}
}