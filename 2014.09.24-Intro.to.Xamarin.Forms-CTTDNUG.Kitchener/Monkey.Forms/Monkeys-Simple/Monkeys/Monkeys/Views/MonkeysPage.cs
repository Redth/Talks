using Xamarin.Forms;
using System.Collections.ObjectModel;
using Monkeys.Utils;

namespace Monkeys.Views
{
	public class MonkeysPage : ContentPage
	{
		ObservableCollection<Monkey> Monkeys;

		public MonkeysPage ()
		{
			Title = "Monkeys";
			Monkeys = new ObservableCollection<Monkey> ();

			var list = new ListView ();
			list.ItemsSource = Monkeys;

			var cell = new DataTemplate (typeof (ImageCell));
      
			cell.SetBinding (ImageCell.TextProperty, "Name");
			cell.SetBinding (ImageCell.DetailProperty, "Location");
			cell.SetBinding (ImageCell.ImageSourceProperty, "Image");

			list.ItemTemplate = cell;

			list.ItemTapped += (sender, args) => {
				var monkey = args.Item as Monkey;
				if (monkey == null)
					return;

				Navigation.PushAsync (new DetailsPage (monkey));
				
                // Reset the selected item
				list.SelectedItem = null;
			};

			var addMonkeyButton = new Button {
				Text = "Add Monkey"
			};

			addMonkeyButton.Clicked += (sender, e) => {
				Monkeys.Add (MonkeyHelper.GetRandomMonkey ());
			};

			var stack = new StackLayout {
				Children = { list, addMonkeyButton }
			};

			Content = stack;
		}
	}
}
