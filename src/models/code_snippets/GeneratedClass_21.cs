/// <summary>Indicates that the map is empty</summary>
public bool IsEmpty => this == Empty;

/// <summary>The count of entries in the map</summary>
public virtual int Count() => 0;

internal virtual bool MayTurnToBranch2 => false;

internal virtual Entry GetMinHashEntryOrDefault() => null;
internal virtual Entry GetMaxHashEntryOrDefault() => null;

/// <summary>Lookup for the entry by hash.
/// The returned entry maybe either single entry or the `HashConflictEntry` with multiple key-value entries for the same hash.
/// If hash does not match the method returns `null`</summary>
internal virtual Entry GetEntryOrNull(int hash) => null;