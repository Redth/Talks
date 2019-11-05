using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyGarishApp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidButton = Android.Widget.Button;

[assembly: ExportRenderer(typeof(GradientButton), typeof(MyGarishApp.Droid.Renderers.GradientButtonRendererAndroid))]

namespace MyGarishApp.Droid.Renderers
{
	public class GradientButtonRendererAndroid : ButtonRenderer
	{
		public GradientButtonRendererAndroid(Context context)
			: base(context)
		{
			this.context = context;
		}

		Context context;
		
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

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
				var gd = new GradientDrawable(GradientDrawable.Orientation.TopBottom,
					new int[] { gradientButton.StartColor.ToAndroid(), gradientButton.EndColor.ToAndroid() });

				Control.SetBackground(gd);
			}
		}
	}
}