using Foundation;
using UIKit;
using WindowsAzure.Messaging;
using System;

namespace PushMonkey.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {        
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {           
            #region 1. Registration
            // Create our settings for notifications
            var pushSettings = UIUserNotificationSettings.GetSettingsForTypes (
                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                new NSSet ());

            // Apply our settings and register
            UIApplication.SharedApplication.RegisterUserNotificationSettings (pushSettings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications ();
            #endregion

            return true;
        }

        #region 2. Registration Callback
        public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
        {         
            #region 3. Azure Registration
            // Connection string from your azure dashboard
            var cs = Shared.Constants.AZURE_CONNECTION_STRING;

            var hubName = "YOUR-HUB-NAME";

            // Register our info with Azure
            var hub = new SBNotificationHub (cs, hubName);

            var choices = new Choices ();

            // Build tags to register on azure with
            var tags = new NSSet (
                choices.Colour, 
                choices.Species, 
                choices.Colour + "-" + choices.Species);

            // Register with Azure
            hub.RegisterNativeAsync (deviceToken, tags, err => {

                // Show the User if we succeeded or failed
                ShowToast (err != null 
                    ? "Error: " + err.Description 
                    : "Registered for Notifications");
            });
            #endregion
        }
        #endregion

        #region 4. Failure Callback
        public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
        {
            // Failed to register for some reason
            ShowToast ("Failed to Register: " + error.Description);
        }   
        #endregion

        #region 5. In-App Notification Receiving
        // Notifications received in the app won't show any UI, but will call this method
        public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
        {
            #region 6. Extract Notification Data
            // Deconstruct the Dictionary (basically JSON)
            var aps = userInfo.ObjectForKey (new NSString ("aps")) as NSDictionary;

            if (aps != null) {

                // Get the Alert message
                var alert = aps.ObjectForKey (new NSString ("alert")) as NSString;

                // Show the aler
                if (alert != null)
                    ShowAlert ("Received Notification", alert);
            }
            #endregion
        }
        #endregion

        #region Helpers
        void ShowToast (string message) 
        {
            BigTed.BTProgressHUD.ShowToast (message, BigTed.ProgressHUD.MaskType.Black, true, 1500);
        }

        void ShowAlert (string title, string message)
        {
            var av = new UIAlertView (title, message, null, "OK");
            av.Show ();
        }
        #endregion
    }
}


