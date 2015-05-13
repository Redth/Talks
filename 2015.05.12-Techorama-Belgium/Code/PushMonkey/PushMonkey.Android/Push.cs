using System;
using Android.Content;
using Gcm;
using Android.App;
using WindowsAzure.Messaging;

[assembly: UsesPermission (Android.Manifest.Permission.ReceiveBootCompleted)]

namespace PushMonkey
{
    #region 1. Android Manifest
    [BroadcastReceiver(Permission=Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })] // Allow GCM on boot and when app is closed   
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    #endregion
    public class SampleGcmBroadcastReceiver : GcmBroadcastReceiverBase<SampleGcmService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = { Shared.Constants.GCM_SENDER_ID };
    }

    [Service] //Must use the service tag
    public class SampleGcmService : GcmServiceBase
    {
        #region 2. Initialization & Registration
        static NotificationHub hub;

        public static void InitializeAndRegister (Context context)
        {
            // Azure Connection String
            var cs = Shared.Constants.AZURE_CONNECTION_STRING;

            // Azure hub name
            var hubName = "YOUR-HUB-NAME";

            // Create a hub instance
            hub = new NotificationHub (hubName, cs, context);
                
            // Makes this easier to call from our Activity
            GcmClient.Register (context, SampleGcmBroadcastReceiver.SENDER_IDS);
        }
        #endregion

        public SampleGcmService() : base(SampleGcmBroadcastReceiver.SENDER_IDS)
        {
        }

        protected override void OnRegistered (Context context, string registrationId)
        {
            #region 3. Handle Registration
            //Receive registration Id for sending GCM Push Notifications to
            var choices = new Choices (this);

            // Create the tags to register our device to
            var tags = new [] { 
                choices.Colour,  // Colour
                choices.Species, // Species
                choices.Colour + "-" + choices.Species // Colour-Species
            };

            if (hub != null)
                hub.Register (registrationId, tags);            
            #endregion
        }

        protected override void OnUnRegistered (Context context, string registrationId)
        {
            if (hub != null)
                hub.Unregister ();
        }
            
        protected override void OnMessage (Context context, Intent intent)
        {
            // Pass the notification data and create a UI Notification
            CreateNotification (intent.Extras);
        }

        #region 4. Create Notification
        void CreateNotification (Android.OS.Bundle extras)
        {
            // What should happen when the notification is pressed
            var intent = new Intent (this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity (this, 0, intent, PendingIntentFlags.OneShot);
            
            // Instantiate the builder and set notification elements:
            var builder = new Notification.Builder (this)
                .SetContentIntent (pendingIntent)
                .SetContentTitle (extras.GetString ("title", "Push Monkey Notification"))
                .SetContentText (extras.GetString ("message", "No Text"))
                .SetSmallIcon (Resource.Drawable.Icon)
                .SetDefaults (NotificationDefaults.All);

            // Build the notification:
            var notification = builder.Build();

            // Get the notification manager:
            var notificationManager = NotificationManager.FromContext (this);
                
            // Publish the notification:
            notificationManager.Notify (0, notification);
        }
        #endregion

        protected override bool OnRecoverableError (Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError (Context context, string errorId)
        {
            //Some more serious error happened
        }
    }
}

