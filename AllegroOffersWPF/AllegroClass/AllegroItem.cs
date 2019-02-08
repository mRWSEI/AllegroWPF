using System;

namespace AllegroClass
{
    /// <summary>
    /// Class containing properties of AllegroItem (serialization)
    /// </summary>
    public class AllegroItem
    {
        public AllegroItem()
        {
            // empty constructor is required for proper work
        }
        private long productId;
        private string itemName;
        private decimal priceItem;
        private decimal priceDelivery;
        private bool freeDelivery;
        private long sellerId;
        private bool company;
        private bool superSeller;
        private int stockQuantity;

        public long ProductId { get => productId; set => productId = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public decimal PriceItem { get => priceItem; set => priceItem = value; }
        public decimal PriceDelivery { get => priceDelivery; set => priceDelivery = value; }
        public bool FreeDelivery { get => freeDelivery; set => freeDelivery = value; }
        public long SellerId { get => sellerId; set => sellerId = value; }
        public bool Company { get => company; set => company = value; }
        public bool SuperSeller { get => superSeller; set => superSeller = value; }
        public int StockQuantity { get => stockQuantity; set => stockQuantity = value; }
    }
}
