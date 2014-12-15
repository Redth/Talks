
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

namespace NotificationsDemo
{
    [Activity (Label = "SecondActivity")]           
    public class SecondActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.SecondLayout);

            // Check for remote input voice
            var remoteInput = Android.Support.V4.App.RemoteInput.GetResultsFromIntent (Intent);

            if (remoteInput != null) {
                var spoken = remoteInput.GetCharSequence ("extra_voice_reply");

                if (string.IsNullOrEmpty (spoken))
                    spoken = "Nothing Said";

                Toast.MakeText (this, spoken, ToastLength.Long).Show ();
            }
        }
    }
}

