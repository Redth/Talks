using System;
using UIKit;
using Foundation;

namespace PushMonkey.iOS
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

        public Choices ()
        {
            var r = new Random ();

            Colour = NSUserDefaults.StandardUserDefaults.StringForKey ("colour");

            if (string.IsNullOrEmpty (Colour)) {
                Colour = colours [r.Next (0, colours.Length)];
                NSUserDefaults.StandardUserDefaults.SetString (Colour, "colour");
            }

            Species = NSUserDefaults.StandardUserDefaults.StringForKey ("species");

            if (string.IsNullOrEmpty (Species)) {
                Species = species [r.Next (0, species.Length)];
                NSUserDefaults.StandardUserDefaults.SetString (Species, "species");
            }
        }

        public UIColor GetColour ()
        {
            var colourName = Colour;

            switch (colourName) {
            case "BLUE":
                return UIColor.FromRGB (52, 152, 219);
            case "GREEN":
                return UIColor.FromRGB (119, 208, 101);
            case "PURPLE":
                return UIColor.FromRGB (180, 85, 182);
            }

            return UIColor.FromRGB (52, 152, 219);
        }
    }
}

