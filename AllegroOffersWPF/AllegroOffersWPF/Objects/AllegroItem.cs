using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroOffersWPF
{
    class AllegroItem
    {
        public AllegroItem()
        {

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
