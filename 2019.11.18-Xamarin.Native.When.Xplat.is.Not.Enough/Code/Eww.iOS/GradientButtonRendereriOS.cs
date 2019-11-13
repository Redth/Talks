using System;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Eww.GradientButton), typeof(Eww.iOS.GradientButtonRendereriOS))]


namespace Eww.iOS
{
    public class GradientButtonRendereriOS : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            ApplyGradient();
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(GradientButton.StartColor)
                || e.PropertyName == nameof(GradientButton.EndColor))
                ApplyGradient();
        }

        void ApplyGradient()
        {
            var gradientButton = Element as GradientButton;

            if (gradientButton != null && Control != null)
            {

                var gradient = new CAGradientLayer();
                gradient.Frame = Control.Bounds;


                gradient.Colors = new CGColor[] {
                    gradientButton.StartColor.ToCGColor(),
                    gradientButton.EndColor.ToCGColor()
                };

                var existing = Control.Layer.Sublayers.FirstOrDefault(sl => sl is CAGradientLayer);

                if (existing != null)
                    Control.Layer.ReplaceSublayer(existing, gradient);
                else
                    Control.Layer.InsertSublayer(gradient, 0);
            }
        }
    }
}
