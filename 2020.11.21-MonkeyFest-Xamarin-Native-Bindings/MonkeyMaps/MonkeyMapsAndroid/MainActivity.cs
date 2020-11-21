using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Codes.Redth.Mylibrary;
using Java.Lang;

namespace MonkeyMapsAndroid
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, IMapBoxSdkCallback
	{
		MapBoxSdk mapbox = null;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			JavaSystem.LoadLibrary("mapbox-gl");

			mapbox = new MapBoxSdk(this, this);
			mapbox.Init(Resources.GetString(Resource.String.mapbox_access_token));
			mapbox.OnCreate(savedInstanceState,
				new Java.Lang.Double(41.921311934646326),
				new Java.Lang.Double(-82.51155995084129),
				new Java.Lang.Integer(11));
		}

		public void MapReady()
		{
			mapbox.AddMarker(41.921311934646326,
				-82.51155995084129,
				"My Backyard",
				"Can you see the point?");
		}

		public void MarkerClicked(string title)
		{
			Toast.MakeText(this, $"{title} was tapped!", ToastLength.Long).Show();
		}

		protected override void OnPause()
		{
			mapbox?.OnPause();
			base.OnPause();
		}

		protected override void OnResume()
		{
			base.OnResume();
			mapbox?.OnResume();
		}

		protected override void OnStart()
		{
			base.OnStart();
			mapbox?.OnStart();
		}

		protected override void OnStop()
		{
			mapbox?.OnStop();
			base.OnStop();
		}

		protected override void OnDestroy()
		{
			mapbox?.OnDestory();
			base.OnDestroy();
		}

		public override void OnLowMemory()
		{
			mapbox.OnLowMemory();
			base.OnLowMemory();
		}
	}
}
