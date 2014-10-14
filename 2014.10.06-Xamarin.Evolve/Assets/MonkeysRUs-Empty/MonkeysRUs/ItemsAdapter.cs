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

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = Items [position];

			// TODO: 4 Create ListItem.axml layout

            // TODO: 5 Inflate layout and map item at position
		}

		// TODO: 6 Search

        List<Item> Items = new List<Item> ();

        public Activity Context { get; set; }
		public override int Count { get { return Items.Count; } }
		public override Item this [int index] { get { return Items[index]; } }
        public override long GetItemId (int position) { return position; }
	}
}
