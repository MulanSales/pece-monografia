protected virtual void Write(IGTFSTargetFile levelsFile, IEnumerable<Level> levels)
{
if (levelsFile == null) return;

bool initialized = false;
var data = new string[3];
foreach (var level in levels)
{
if (!initialized)
{
if (levelsFile.Exists)
{
levelsFile.Clear();
}

// write headers.
data[0] = "level_id";
data[1] = "level_index";
data[2] = "level_name";
levelsFile.Write(data);
initialized = true;
}