delegate void ActionSheetHandler(ActionSheetAction arg0);

delegate void RippleCompletionBlock();

delegate void ActivityIndicatorAnimationHandler(nfloat strokeStart, nfloat strokeEnd);

delegate void ActionHandler(AlertAction action);

delegate void FeatureHighlightCompletionHandler(bool accepted);

delegate void FlexibleHeaderChangeContentInsetsHandler();

delegate void FlexibleHeaderShadowIntensityChangeHandler(CALayer shadowLayer, nfloat intensity);

delegate void InkCompletionHandler();

delegate void EnumerateOverlaysHandler(IOverlay overlay, nuint idx, ref bool stop);

delegate void SnackbarMessageCompletionHandler(bool arg0);

delegate void SnackbarMessageActionHandler();