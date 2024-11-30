while (index.ContainsKey(sfmId)) // is a duplicate
{
++homograph;
sfmId = adaptedEntry.SfmID + "_" + homograph;
}