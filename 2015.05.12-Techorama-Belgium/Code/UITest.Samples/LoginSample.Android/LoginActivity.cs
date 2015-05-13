using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using V7AlertDialog = Android.Support.V7.App.AlertDialog;

namespace LoginSample
{
    [Activity (Label = "Login Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : AppCompatActivity
    {
        Button buttonLogin;
        EditText editTextEmail;
        EditText editTextPassword;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.login);

            // Wire up our controls
            buttonLogin = FindViewById<Button> (Resource.Id.buttonLogin);
            editTextEmail = FindViewById<EditText> (Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText> (Resource.Id.editTextPassword);

            // Wire up events
            buttonLogin.Click += ButtonLogin_Click;
        }

        void ButtonLogin_Click (object sender, EventArgs e)
        {
            var email = editTextEmail.Text;
            var pwd = editTextPassword.Text;

            if (email == "foo@user.com" && pwd == "bar") {
                StartActivity (typeof(HomeActivity));
            } else {
                ShowAlert ("Login Failed", "Incorrect email and/or password!");
            }
        }

        void ShowAlert (string title, string message)
        {
            V7AlertDialog dg = null;

            dg = new V7AlertDialog.Builder (this)
                .SetTitle (title)
                .SetMessage (message)
                .SetPositiveButton ("OK", delegate {
                    dg.Cancel ();        
                })
                .Show ();
        }
    }
}


