using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using AndroidButton = Android.Widget.Button;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Eww.GradientButton), typeof(Eww.Droid.GradientButtonRendererAndroid))]

namespace Eww.Droid
{
    public class GradientButtonRendererAndroid : ButtonRenderer
    {
        public GradientButtonRendererAndroid(Context context)
            : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
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
                var gd = new GradientDrawable(
                    GradientDrawable.Orientation.TopBottom,
                    new int[] {
                        gradientButton.StartColor.ToAndroid(),
                        gradientButton.EndColor.ToAndroid()
                    });

                Control.SetBackground(gd);
            }
        }
    }
}
