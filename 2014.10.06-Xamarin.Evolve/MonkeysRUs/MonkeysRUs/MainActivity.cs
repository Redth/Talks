using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Card.IO;
using AndroidHUD;

namespace Monkeys
{
	[Activity (Label = "Monkeys 'R Us!", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button buttonSearch;
		Button buttonScan;
		ListView listView;
		EditText editTextSearch;

		ItemsAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			buttonScan = FindViewById<Button> (Resource.Id.buttonScan);
			buttonSearch = FindViewById<Button> (Resource.Id.buttonSearch);
			listView = FindViewById<ListView> (Resource.Id.listView);
			editTextSearch = FindViewById<EditText> (Resource.Id.editTextSearch);

            adapter = new ItemsAdapter (this);
			listView.Adapter = adapter;

			buttonScan.Click += async delegate {
				var scanner = new ZXing.Mobile.MobileBarcodeScanner (this);
				var result = await scanner.Scan ();

				if (result != null && !string.IsNullOrEmpty (result.Text)) {

					AndHUD.Shared.Show (this, "Searching");

					await adapter.Search (result.Text);

					AndHUD.Shared.Dismiss (this);
				}
			};

			buttonSearch.Click += async delegate {
				AndHUD.Shared.Show (this, "Searching");

				await adapter.Search (editTextSearch.Text);

				AndHUD.Shared.Dismiss (this);
			};

			listView.ItemClick += (sender, e) => {

				var item = adapter[e.Position];

                Api.CurrentOrder = new Order (item);

				var intent = new Intent(this, typeof(CardIOActivity));
				intent.PutExtra(CardIOActivity.ExtraAppToken, ServiceTokens.CardIOAppToken);
				intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true); 	
				intent.PutExtra(CardIOActivity.ExtraRequireCvv, true); 		
				intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false); 
				intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);

				StartActivityForResult (intent, 101);
			};
		}

		protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

            if (requestCode == 101 && data != null) {

                AndHUD.Shared.Show (this, "Verifying Payment...");

                try {
                    // Be sure to JavaCast to a CreditCard (normal cast won't work)		 
                    var card = data.GetParcelableExtra (CardIOActivity.ExtraScanResult).JavaCast<CreditCard> ();

                    // Create a Stripe credit card object from the card.io card object
                    var stripeCard = new Stripe.Card {
                        Number = card.CardNumber,
                        ExpiryMonth = card.ExpiryMonth,
                        ExpiryYear = card.ExpiryYear,
                        CVC = card.Cvv
                    };

                    // Get a token from stripe given the credit card info
                    var stripeToken = await Stripe.StripeClient.CreateToken (stripeCard, ServiceTokens.StripePublishableKey);

                    // Set our current order's token
                    Api.CurrentOrder.StripeTokenId = stripeToken.Id;

                    // Get the ship to info and signature
                    StartActivityForResult (typeof(SignatureActivity), 102);
                }
                catch (Exception ex) {
                    Toast.MakeText (this, ex.ToString (), ToastLength.Short).Show ();
                } finally {
                    AndHUD.Shared.Dismiss (this);
                }

            } else if (requestCode == 102 && data != null && resultCode == Result.Ok) {

                var success = true;

                try {
                    Api.CurrentOrder.ShipTo = data.GetStringExtra ("SHIP_TO");
                    Api.CurrentOrder.SignaturePoints = data.GetStringExtra ("SIGNATURE");

                    AndHUD.Shared.Show (this, "Completing Order...");

                    // Save to azure
                    await Api.SaveOrder (Api.CurrentOrder);
                   
                    // Notify the warehouse by SMS
                    await Api.NotifyWarehouse (Api.CurrentOrder);
                                                   
                } catch (Exception ex) {

                    Toast.MakeText (this, ex.ToString (), ToastLength.Short).Show ();
                    success = false;

                } finally {

                    AndHUD.Shared.Dismiss (this);

                    if (success)
                        AndHUD.Shared.ShowSuccessWithStatus (this, "Order Completed!", MaskType.Clear, TimeSpan.FromSeconds (3));                   
                }

                Api.CurrentOrder = null;
            } else {
                // Reset the order
                Api.CurrentOrder = null;
            }
		}
	}
}
