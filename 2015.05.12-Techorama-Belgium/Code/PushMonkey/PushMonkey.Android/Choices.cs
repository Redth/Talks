using System;
using Android.Content;

namespace PushMonkey
{
    public class Choices
    {
        string[] colours = new string[] {
            "BLUE",
            "GREEN",
            "PURPLE"
        };

        string[] species = new string[] {
            "Tamarin",
            "Spider",
            "Howler"
        };

        public string Colour { get; private set; }

        public string Species { get; private set; }

        public Choices (Context context)
        {
            var r = new Random ();

            var prefs = context.GetSharedPreferences ("default", FileCreationMode.Private);

            if (!prefs.Contains ("colour") || !prefs.Contains ("species")) {

                var editor = prefs.Edit ();
                editor.PutString ("colour", colours [r.Next (0, colours.Length)]);
                editor.PutString ("species", species [r.Next (0, species.Length)]);
                editor.Commit ();
            }

            Colour = prefs.GetString ("colour", colours[0]);
            Species = prefs.GetString ("species", species[0]);
        }

        public Android.Graphics.Color GetColor ()
        {
            var colourName = Colour;

            switch (colourName) {
            case "BLUE":
                return Android.Graphics.Color.Argb (255, 52, 152, 219);
            case "GREEN":
                return Android.Graphics.Color.Argb (255, 119, 208, 101);
            case "PURPLE":
                return Android.Graphics.Color.Argb (255, 180, 85, 182);
            }

            return Android.Graphics.Color.Argb (255, 52, 152, 219);
        }
    }
}

