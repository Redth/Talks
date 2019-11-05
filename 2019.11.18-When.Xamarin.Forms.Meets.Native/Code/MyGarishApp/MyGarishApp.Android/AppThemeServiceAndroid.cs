using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyGarishApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppThemeServiceAndroid))]

namespace MyGarishApp.Droid
{
	public class AppThemeServiceAndroid : IAppThemeService
	{
		public bool IsDarkMode
			=> (Android.App.Application.Context.Resources.Configuration.UiMode
				& Android.Content.Res.UiMode.NightMask) == Android.Content.Res.UiMode.NightMask;
	}
}