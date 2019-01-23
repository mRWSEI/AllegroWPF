using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroOffersWPF
{
        public class Rootobject
        {
            public Items items { get; set; }
            public Searchmeta searchMeta { get; set; }
            public Categories categories { get; set; }
            public Filter[] filters { get; set; }
            public Sort[] sort { get; set; }
        }

        public class Items
        {
            public object[] promoted { get; set; }
            public Regular[] regular { get; set; }
        }

        public class Regular
        {
            public string id { get; set; }
            public string name { get; set; }
            public Seller seller { get; set; }
            public Promotion promotion { get; set; }
            public Delivery delivery { get; set; }
            public Image[] images { get; set; }
            public Sellingmode sellingMode { get; set; }
            public Stock stock { get; set; }
            public Category category { get; set; }
        }

        public class Seller
        {
            public string id { get; set; }
            public bool company { get; set; }
            public bool superSeller { get; set; }
        }

        public class Promotion
        {
            public bool emphasized { get; set; }
            public bool bold { get; set; }
            public bool highlight { get; set; }
        }

        public class Delivery
        {
            public bool availableForFree { get; set; }
            public Lowestprice lowestPrice { get; set; }
        }

        public class Lowestprice
        {
            public string amount { get; set; }
            public string currency { get; set; }
        }

        public class Sellingmode
        {
            public string format { get; set; }
            public Price price { get; set; }
            public int popularity { get; set; }
        }

        public class Price
        {
            public string amount { get; set; }
            public string currency { get; set; }
        }

        public class Stock
        {
            public string unit { get; set; }
            public int available { get; set; }
        }

        public class Category
        {
            public string id { get; set; }
        }

        public class Image
        {
            public string url { get; set; }
        }

        public class Searchmeta
        {
            public int availableCount { get; set; }
            public int totalCount { get; set; }
            public bool fallback { get; set; }
        }

        public class Categories
        {
            public Subcategory[] subcategories { get; set; }
            public Path[] path { get; set; }
        }

        public class Subcategory
        {
            public string id { get; set; }
            public string name { get; set; }
            public int count { get; set; }
        }

        public class Path
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Filter
        {
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public Value[] values { get; set; }
            public int minValue { get; set; }
            public int maxValue { get; set; }
        }

        public class Value
        {
            public string value { get; set; }
            public string name { get; set; }
            public int count { get; set; }
            public bool selected { get; set; }
            public string idSuffix { get; set; }
        }

        public class Sort
        {
            public string value { get; set; }
            public string name { get; set; }
            public string order { get; set; }
            public bool selected { get; set; }
        }
}
