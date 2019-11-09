using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyGarishApp
{
	public class GradientButton : Xamarin.Forms.Button
	{
		Color startColor = Color.Transparent;
		Color endColor = Color.Transparent;

		public Color StartColor
		{
			get => startColor;
			set
			{
				startColor = value;
				OnPropertyChanged(nameof(StartColor));
			}
		}

		public Color EndColor
		{
			get => endColor;
			set
			{
				endColor = value;
				OnPropertyChanged(nameof(EndColor));
			}
		}
	}
}
