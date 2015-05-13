using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace LoginSample.iOS
{
	partial class LoginViewController : UIViewController
    {
        public LoginViewController (IntPtr handle) : base (handle)
        {            
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            textEmail.AccessibilityIdentifier = "textEmail";
            textPassword.AccessibilityIdentifier = "textPassword";
            buttonLogin.AccessibilityIdentifier = "buttonLogin";

            textEmail.ShouldReturn = (textField) => {
                textField.ResignFirstResponder ();
                textPassword.BecomeFirstResponder ();
                return true;
            };

            textPassword.ShouldReturn = (textField) => {                
                textPassword.ResignFirstResponder ();
                Login ();
                return true;
            };
        }

        void ShowAlert (string title, string message)
        {
            var av = new UIAlertView (title, message, null, "OK");
            av.Show ();
        }

        partial void buttonLogin_TouchUpInside (UIButton sender)
        {
            Login ();
        }

        void Login ()
        {
            var email = textEmail.Text;
            var pwd = textPassword.Text;

            if (email == "foo@user.com" && pwd == "bar") {
                PerformSegue ("segueHome", this);
            } else {
                ShowAlert ("Login Failed", "Incorrect email and/or password!");
            }
        }
	}
}
