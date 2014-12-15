using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;

namespace SensorDemo
{
    [Activity (Label = "SensorDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ISensorEventListener
    {
        TextView textHeading;
        SensorManager sensorManager;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            textHeading = FindViewById<TextView> (Resource.Id.textHeading);


            sensorManager = SensorManager.FromContext (this);

            var sensor = sensorManager.GetDefaultSensor (SensorType.Orientation);
            sensorManager.RegisterListener (this, sensor, SensorDelay.Ui);
        }

        public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
        {
            Console.WriteLine ("Accuracy Changed: {0} => {1}", sensor.Name, accuracy);
        }

        public void OnSensorChanged (SensorEvent e)
        {
            var degrees = e.Values[0];

            var cardinal = DegreesToCardinal (degrees);

            textHeading.Text = cardinal;
        }

        string DegreesToCardinal(float degrees)
        {
            string[] caridnals = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
            return caridnals[ (int)Math.Round(((float)degrees % 360) / 45) ];
        }

    }
}


