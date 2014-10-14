using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using SignaturePad;

namespace Monkeys
{
	[Activity (Label = "Customer Details")]			
	public class SignatureActivity : Activity
	{
		SignaturePadView signaturePad;
		Button buttonDone;
		EditText editTextViewShipTo;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SignatureLayout);

			signaturePad = FindViewById<SignaturePadView> (Resource.Id.signatureView);
			buttonDone = FindViewById<Button> (Resource.Id.buttonDone);
			editTextViewShipTo = FindViewById<EditText> (Resource.Id.editTextShipTo);

			buttonDone.Click += async (sender, e) => {
            
                // Return the signature and shipto info (serialize the signature points)
                var resultIntent = new Intent ();
                resultIntent.PutExtra ("SHIP_TO", editTextViewShipTo.Text);
                resultIntent.PutExtra ("SIGNATURE", JsonConvert.SerializeObject (signaturePad.Points));

                // Set our result as a success and close it
                SetResult (Result.Ok, resultIntent);
                Finish ();
			};
		}
	}
}
