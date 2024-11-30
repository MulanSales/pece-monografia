/// <summary>
/// The default span of time visible by the length of the scrolling axes.
/// This is clamped between <see cref="time_span_min"/> and <see cref="time_span_max"/>.
/// </summary>
private const double time_span_default = 1500;
/// <summary>
/// The minimum span of time that may be visible by the length of the scrolling axes.
/// </summary>
private const double time_span_min = 50;
/// <summary>
/// The maximum span of time that may be visible by the length of the scrolling axes.
/// </summary>
private const double time_span_max = 10000;
/// <summary>
/// The step increase/decrease of the span of time visible by the length of the scrolling axes.
/// </summary>
private const double time_span_step = 50;
