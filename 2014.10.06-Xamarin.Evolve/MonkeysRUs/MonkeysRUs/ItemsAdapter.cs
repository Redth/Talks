using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;

namespace Monkeys
{

	public class ItemsAdapter : BaseAdapter<Item>
	{		
        public ItemsAdapter (Activity context) { Context = context; }

		public async Task Search (string query)
		{
			var results = await Api.Search (query);

			Items.Clear ();
			Items.AddRange (results);

			NotifyDataSetChanged ();
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = Items [position];

			var view = convertView ?? Context.LayoutInflater.Inflate (Resource.Layout.ListItem, parent, false);

			view.FindViewById<TextView> (Resource.Id.textViewTitle).Text = item.Title;
			view.FindViewById<TextView> (Resource.Id.textViewPrice).Text = item.Price.ToString ("C");
            view.FindViewById<TextView> (Resource.Id.textViewUpc).Text = "UPC: " + item.Barcode;

            var imageView = view.FindViewById<ImageView> (Resource.Id.imageView);

            // Lazy load the image
            Koush.UrlImageViewHelper.SetUrlDrawable (imageView, item.ImageUrl);

			return view;
		}

        List<Item> Items = new List<Item> ();

        public Activity Context { get; set; }
		public override int Count { get { return Items.Count; } }
		public override Item this [int index] { get { return Items[index]; } }
        public override long GetItemId (int position) { return position; }
	}
}
