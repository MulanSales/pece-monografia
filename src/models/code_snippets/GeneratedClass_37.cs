// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Linq;
using Avalonia.Logging;
using Avalonia.VisualTree;

namespace Avalonia.Layout
{
/// <summary>
/// Defines how a control aligns itself horizontally in its parent control.
/// </summary>
public enum HorizontalAlignment
{
/// <summary>
/// The control stretches to fill the width of the parent control.
/// </summary>
Stretch,

/// <summary>
/// The control aligns itself to the left of the parent control.
/// </summary>
Left,

/// <summary>
/// The control centers itself in the parent control.
/// </summary>
Center,

/// <summary>
/// The control aligns itself to the right of the parent control.
/// </summary>
Right,
}

/// <summary>
/// Defines how a control aligns itself vertically in its parent control.
/// </summary>
public enum VerticalAlignment
{
/// <summary>
/// The control stretches to fill the height of the parent control.
/// </summary>
Stretch,

/// <summary>
/// The control aligns itself to the top of the parent control.
/// </summary>
Top,

/// <summary>
/// The control centers itself within the parent control.
/// </summary>
Center,

/// <summary>
/// The control aligns itself to the bottom of the parent control.
/// </summary>
Bottom,
}

}
