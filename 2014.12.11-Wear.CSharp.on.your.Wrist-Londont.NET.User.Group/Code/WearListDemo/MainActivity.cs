using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.Wearable.Views;
using Android.Views;
using Android.Widget;

namespace WearListDemo
{
    [Activity (Label = "WearListDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        WearableListView wearableListView;
        MyWearAdapter adapter;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var v = FindViewById<WatchViewStub> (Resource.Id.watch_view_stub);
            v.LayoutInflated += delegate {

                adapter = new MyWearAdapter (this);

                wearableListView = FindViewById<WearableListView> (Resource.Id.wearableListView);
                wearableListView.SetAdapter (adapter);
            };
        }
    }

    public class MyWearAdapter : WearableListView.Adapter
    {
        public MyWearAdapter (Context context)
        {
            Context = context;
        }

        public Context Context { get; private set; }

        string[] listitems = new [] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5","Item 6" };

        #region implemented abstract members of Adapter
        public override void OnBindViewHolder (Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            var item = listitems [position];

            var textView = holder.ItemView.FindViewById <TextView> (Android.Resource.Id.Text1);

            textView.Text = item;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.FromContext (Context).Inflate (Android.Resource.Layout.SimpleListItem1, null);

            return new WearableListView.ViewHolder (view);
        }

        public override int ItemCount { get { return listitems.Length; } }
        #endregion


    }
}



