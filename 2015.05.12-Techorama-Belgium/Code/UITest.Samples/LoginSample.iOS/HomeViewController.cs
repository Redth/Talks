using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MonoTouch.Dialog;

namespace LoginSample.iOS
{
	partial class HomeViewController : DialogViewController
	{
		public HomeViewController (IntPtr handle) : base (handle)
		{
            Pushing = true;

            EnableSearch = true;
            SearchPlaceholder = "Filter...";
		}
       
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();           

            var items = new [] {
                "One", "Two", "Three", "Four", "Five", "Six", "Seven",
                "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen",
                "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen",
                "Nineteen", "Twenty", "Twentyone", "Twentytwo", "Twentythree",
                "Twentyfour", "Twentyfive", "Twentysix", "Twentyseven",
                "Twentyeight", "Twentynine", "Thirty", "Thirtyone",
                "Thirtytwo", "Thirtythree", "Thirtyfour", "Thirtyfive"
            };

            Root = new RootElement ("Home") {
                new Section ()
            };

            foreach (var item in items)
                Root[0].Add (new StyledStringElement (item));            
        }
	}
}
