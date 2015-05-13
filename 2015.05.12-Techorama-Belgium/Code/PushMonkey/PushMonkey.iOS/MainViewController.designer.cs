// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PushMonkey.iOS
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelColour { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelSpecies { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (labelColour != null) {
				labelColour.Dispose ();
				labelColour = null;
			}
			if (labelSpecies != null) {
				labelSpecies.Dispose ();
				labelSpecies = null;
			}
		}
	}
}
