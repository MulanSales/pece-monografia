// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Audio;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mania.Beatmaps;
using osu.Game.Skinning;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Mania.Skinning
{
public class ManiaLegacySkinTransformer : ISkin
{
private readonly ISkin source;
private readonly ManiaBeatmap beatmap;

/// <summary>
/// Mapping of <see cref="HitResult"/> to their corresponding
/// <see cref="LegacyManiaSkinConfigurationLookups"/> value.
/// </summary>
private static readonly IReadOnlyDictionary<HitResult, LegacyManiaSkinConfigurationLookups> hitresult_mapping
= new Dictionary<HitResult, LegacyManiaSkinConfigurationLookups>
{
{ HitResult.Perfect, LegacyManiaSkinConfigurationLookups.Hit300g },
{ HitResult.Great, LegacyManiaSkinConfigurationLookups.Hit300 },
{ HitResult.Good, LegacyManiaSkinConfigurationLookups.Hit200 },
{ HitResult.Ok, LegacyManiaSkinConfigurationLookups.Hit100 },
{ HitResult.Meh, LegacyManiaSkinConfigurationLookups.Hit50 },
{ HitResult.Miss, LegacyManiaSkinConfigurationLookups.Hit0 }
};

/// <summary>
/// Mapping of <see cref="HitResult"/> to their corresponding
/// default filenames.
/// </summary>
private static readonly IReadOnlyDictionary<HitResult, string> default_hitresult_skin_filenames
= new Dictionary<HitResult, string>
{
{ HitResult.Perfect, "mania-hit300g" },
{ HitResult.Great, "mania-hit300" },
{ HitResult.Good, "mania-hit200" },
{ HitResult.Ok, "mania-hit100" },
{ HitResult.Meh, "mania-hit50" },
{ HitResult.Miss, "mania-hit0" }
};

private Lazy<bool> isLegacySkin;

/// <summary>
/// Whether texture for the keys exists.
/// Used to determine if the mania ruleset is skinned.
/// </summary>
private Lazy<bool> hasKeyTexture;

public ManiaLegacySkinTransformer(ISkinSource source, IBeatmap beatmap)
{
this.source = source;
this.beatmap = (ManiaBeatmap)beatmap;

source.SourceChanged += sourceChanged;
sourceChanged();
}

private void sourceChanged()
{
isLegacySkin = new Lazy<bool>(() => source.GetConfig<LegacySkinConfiguration.LegacySetting, decimal>(LegacySkinConfiguration.LegacySetting.Version) != null);
hasKeyTexture = new Lazy<bool>(() => source.GetAnimation(
this.GetManiaSkinConfig<string>(LegacyManiaSkinConfigurationLookups.KeyImage, 0)?.Value
?? "mania-key1", true, true) != null);
}

public Drawable GetDrawableComponent(ISkinComponent component)
{
switch (component)
{
case GameplaySkinComponent<HitResult> resultComponent:
return getResult(resultComponent.Component);

case ManiaSkinComponent maniaComponent:
if (!isLegacySkin.Value || !hasKeyTexture.Value)
return null;

switch (maniaComponent.Component)
{
case ManiaSkinComponents.ColumnBackground:
return new LegacyColumnBackground(maniaComponent.TargetColumn == beatmap.TotalColumns - 1);

case ManiaSkinComponents.HitTarget:
return new LegacyHitTarget();

case ManiaSkinComponents.KeyArea:
return new LegacyKeyArea();

case ManiaSkinComponents.Note:
return new LegacyNotePiece();

case ManiaSkinComponents.HoldNoteHead:
return new LegacyHoldNoteHeadPiece();

case ManiaSkinComponents.HoldNoteTail:
return new LegacyHoldNoteTailPiece();

case ManiaSkinComponents.HoldNoteBody:
return new LegacyBodyPiece();

case ManiaSkinComponents.HitExplosion:
return new LegacyHitExplosion();

case ManiaSkinComponents.StageBackground:
return new LegacyStageBackground();

case ManiaSkinComponents.StageForeground:
return new LegacyStageForeground();
}

break;
}

return null;
}

private Drawable getResult(HitResult result)
{
string filename = this.GetManiaSkinConfig<string>(hitresult_mapping[result])?.Value
?? default_hitresult_skin_filenames[result];

return this.GetAnimation(filename, true, true);
}

public Texture GetTexture(string componentName) => source.GetTexture(componentName);

public SampleChannel GetSample(ISampleInfo sample) => source.GetSample(sample);

public IBindable<TValue> GetConfig<TLookup, TValue>(TLookup lookup)
{
if (lookup is ManiaSkinConfigurationLookup maniaLookup)
return source.GetConfig<LegacyManiaSkinConfigurationLookup, TValue>(new LegacyManiaSkinConfigurationLookup(beatmap.TotalColumns, maniaLookup.Lookup, maniaLookup.TargetColumn));

return source.GetConfig<TLookup, TValue>(lookup);
}
}
}
