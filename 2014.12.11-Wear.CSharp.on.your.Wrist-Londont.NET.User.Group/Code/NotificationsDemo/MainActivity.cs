using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace NotificationsDemo
{
    [Activity (Label = "NotificationsDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FindViewById<Button> (Resource.Id.buttonSimple).Click += delegate {
                SimpleNotification ();
            };
            FindViewById<Button> (Resource.Id.buttonVoice).Click += delegate {
                VoiceInput ();
            };      
        }
    
        void SimpleNotification ()
        {
            var viewIntent = new Intent (this, typeof(SecondActivity));
            var pendingIntent = PendingIntent.GetActivity (this, 0, viewIntent, PendingIntentFlags.UpdateCurrent);

            var builder = new Android.Support.V4.App.NotificationCompat.Builder (this)
                .SetSmallIcon (Android.Resource.Drawable.IcDialogAlert)
                .SetContentTitle ("Check out 2nd Activity")
                .SetContentText ("This notification will let you open the Second Activity")
                .SetContentIntent (pendingIntent);

            var manager = Android.Support.V4.App.NotificationManagerCompat.From (this);

            manager.Notify (1, builder.Build ());
        }

        void VoiceInput ()
        {
            var viewIntent = new Intent (this, typeof(SecondActivity));
            var pendingIntent = PendingIntent.GetActivity (this, 0, viewIntent, PendingIntentFlags.UpdateCurrent);

            var remoteInput = new Android.Support.V4.App.RemoteInput.Builder("extra_voice_reply")
                .SetLabel ("Reply")
                .Build();

            var action = new Android.Support.V4.App.NotificationCompat.Action.Builder (
                Android.Resource.Drawable.IcButtonSpeakNow,
                "Reply",
                pendingIntent)
                .AddRemoteInput (remoteInput)
                .Build ();

            var extender = new Android.Support.V4.App.NotificationCompat.WearableExtender ()
                .AddAction (action);

            var builder = new Android.Support.V4.App.NotificationCompat.Builder (this)
                .SetSmallIcon (Android.Resource.Drawable.IcDialogAlert)
                .SetContentTitle ("Speak to the 2nd Activity")
                .SetContentText ("This notification will let you speak to the Second Activity")
                .SetContentIntent (pendingIntent)
                .Extend (extender);

            var manager = Android.Support.V4.App.NotificationManagerCompat.From (this);

            manager.Notify (2, builder.Build ());
        }
    }
}


