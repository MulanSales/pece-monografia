
/// <summary>
///     Type (name) of this language object.
/// </summary>
public LanguageType Type { get; }

internal Orthography Orthography { get; }
internal Morphotactics Morphotactics { get; }

internal MorphemeContainer<Suffix> Suffixes { get; }
internal MorphemeContainer<Root> Roots { get; }

private WordAnalyzer Analyzer { get; }