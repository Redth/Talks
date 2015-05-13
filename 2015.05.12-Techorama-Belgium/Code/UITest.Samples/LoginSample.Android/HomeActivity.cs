
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
using Android.Support.V7.App;
using V7SearchView = Android.Support.V7.Widget.SearchView;

namespace LoginSample
{
    [Activity (Label = "Home")]			
    public class HomeActivity : AppCompatActivity, V7SearchView.IOnQueryTextListener
    {
        ListView listView;
        ArrayAdapter<string> adapter;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Create your application here
            SetContentView (Resource.Layout.home);

            listView = FindViewById<ListView> (Resource.Id.listView);
            adapter = new ArrayAdapter<string> (this,
                Android.Resource.Layout.SimpleListItem1,
                Android.Resource.Id.Text1,
                new [] {
                    "One", "Two", "Three", "Four", "Five", "Six", "Seven",
                    "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen",
                    "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen",
                    "Nineteen", "Twenty", "Twentyone", "Twentytwo", "Twentythree",
                    "Twentyfour", "Twentyfive", "Twentysix", "Twentyseven",
                    "Twentyeight", "Twentynine", "Thirty", "Thirtyone",
                    "Thirtytwo", "Thirtythree", "Thirtyfour", "Thirtyfive"
                });

            listView.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu (IMenu menu)
        {
            // Inflate our menu
            MenuInflater.Inflate (Resource.Menu.home_menu, menu);

            // Find the search item and grab its search view
            var search = menu.FindItem (Resource.Id.search)
                .ActionView.JavaCast<V7SearchView> ();

            // Setup our listener
            search.SetOnQueryTextListener (this);

            return true;
        }

        public bool OnQueryTextChange (string newText)
        {
            adapter.Filter.InvokeFilter (newText);

            return true;
        }

        public bool OnQueryTextSubmit (string query)
        {
            adapter.Filter.InvokeFilter (query);

            return true;
        }
    }
}

