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

namespace LoginSample.iOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textPassword { get; set; }

		[Action ("buttonLogin_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void buttonLogin_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (buttonLogin != null) {
				buttonLogin.Dispose ();
				buttonLogin = null;
			}
			if (textEmail != null) {
				textEmail.Dispose ();
				textEmail = null;
			}
			if (textPassword != null) {
				textPassword.Dispose ();
				textPassword = null;
			}
		}
	}
}
