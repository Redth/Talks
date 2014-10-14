using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.OS;

namespace Monkeys
{
	[Activity (Label = "Monkeys 'R Us!", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{		
		// TODO: 1 Main Layout
		
		// TODO: 2 Instance Variables

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//TODO: 3 OnCreate
		}

		protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

		}
	}
}
