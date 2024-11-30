protected override void OnUserScroll(float value, bool animated = true, double? distanceDecay = default)
{
UserScrolling = true;
base.OnUserScroll(value, animated, distanceDecay);
}