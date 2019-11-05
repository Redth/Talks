using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MyGarishApp;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientButton), typeof(MyGarishApp.iOS.Renderers.GradientButtonRendereriOS))]

namespace MyGarishApp.iOS.Renderers
{
	public class GradientButtonRendereriOS : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
				ApplyGradient();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "StartColor" || e.PropertyName == "EndColor")
				ApplyGradient();
		}

		void ApplyGradient()
		{
			var gradientButton = Element as GradientButton;

			if (gradientButton != null && Control != null)
			{
				var g = new CAGradientLayer();
				g.Frame = Control.Bounds;
				g.Colors = new CGColor[] {
					gradientButton.StartColor.ToCGColor(),
					gradientButton.EndColor.ToCGColor()
				};

				Control.Layer.InsertSublayer(g, 0);
			}
		}
	}
}