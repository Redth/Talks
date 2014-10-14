namespace Monkeys
{
	public class Order
	{
        public Order (Item item)
        {
            ItemBarcode = item.Barcode;
            ItemImageUrl = item.ImageUrl;
            ItemTitle = item.Title;
            ItemPrice = item.Price;
        }

        public string Id { get;set; }	
        public string ItemBarcode { get; set; }      
        public string ItemImageUrl { get;set; }
        public string ItemTitle { get;set; }
        public decimal ItemPrice { get;set; }
		public string SignaturePoints { get;set; }		
        public string ShipTo { get;set; }
		public string StripeTokenId { get;set; }
	}
}
