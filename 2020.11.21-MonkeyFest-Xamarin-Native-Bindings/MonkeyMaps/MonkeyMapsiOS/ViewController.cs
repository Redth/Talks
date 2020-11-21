using Foundation;
using System;
using UIKit;
using MapboxWrapper;

namespace MonkeyMapsiOS
{
	public partial class ViewController : UIViewController, IMapboxWrapperCallback
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		MapboxWrapper.MapboxWrapper mapbox;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.


			mapbox = new MapboxWrapper.MapboxWrapper(View.Frame, this);
			

			View.AddSubview(mapbox.View);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public void MarkerClickedWithTitle(string title)
		{
			var alert = UIAlertController.Create(title, "Was Tapped!", UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			
			PresentViewController(alert, true, null);
		}

		public void MapReady()
		{
			mapbox.AddMarkerWithLat(
				41.921311934646326,
				-82.51155995084129,
				"My Backyard",
				"Can you see the point?");
		}
	}
}