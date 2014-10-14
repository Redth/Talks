using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ModernHttpClient;

namespace Monkeys
{
	public static class Api
	{
        public static Order CurrentOrder { get;set; }

		public static async Task<List<Item>> Search (string query) 
		{
			var http = new HttpClient (new NativeMessageHandler ());

            var json = await http.GetStringAsync (string.Format (ServiceTokens.AzureSearchUrl, query));

			return Newtonsoft.Json.JsonConvert.DeserializeObject <List<Item>> (json);
		}

        public static async Task SaveOrder (Order order) 
        {
            var azure = new MobileServiceClient (
                            ServiceTokens.AzureMobileServiceUrl,
                            ServiceTokens.AzureMobileServiceKey);

            var orderTable = azure.GetTable <Order> ();

            await orderTable.InsertAsync (order);
        }

        public static async Task NotifyWarehouse (Order order)
        {
            var body = new StringBuilder ();
            body.AppendLine ("Ship To:");
            body.AppendLine (order.ShipTo);
            body.AppendLine ();
            body.AppendLine (order.ItemTitle);
            body.AppendLine (order.ItemBarcode);

            var twilio = new Twilio.TwilioRestClient (ServiceTokens.TwilioSid, ServiceTokens.TwilioAuthToken);

            await twilio.SendMessage (ServiceTokens.FromMobileNumber, ServiceTokens.ToMobileNumber, body.ToString ());
        }
	}
}
