using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PushMonkey.iOS
{
	partial class MainViewController : UIViewController
	{
		public MainViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad ()
        {
            var choices = new Choices ();

            // Set the text and background colour
            labelColour.Text = choices.Colour;
            labelColour.BackgroundColor = choices.GetColour ();

            // Set the text
            labelSpecies.Text = choices.Species;
        }
	}
}
