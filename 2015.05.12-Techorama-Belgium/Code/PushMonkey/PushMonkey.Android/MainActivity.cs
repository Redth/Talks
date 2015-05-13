using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PushMonkey
{
    [Activity (Label = "PushMonkey", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {       
        TextView textViewColor;
        TextView textViewSpecies;

        Choices choices;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Wire up our controls
            textViewColor = FindViewById<TextView> (Resource.Id.textViewColor);
            textViewSpecies = FindViewById<TextView> (Resource.Id.textViewSpecies);

            choices = new Choices (this);

            // Set our text and colours
            textViewColor.SetBackgroundColor (choices.GetColor ());
            textViewColor.Text = choices.Colour;
            textViewSpecies.Text = choices.Species;

            // Initialize our Gcm Service Hub
            SampleGcmService.InitializeAndRegister (this);
        }
    }
}


