using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OnlineShop.Models
{
    public class ConvertedCurrentPrice
    {
        public double Value { get; set; }
        [Key]
        public string CurrencyID { get; set; }
    }

    public class ShippingServiceCost
    {
        public int Value { get; set; }
        [Key]
        public string CurrencyID { get; set; }
    }

    public class ListedShippingServiceCost
    {
        public int Value { get; set; }
        [Key]
        public string CurrencyID { get; set; }
    }

    public class ShippingCostSummary
    {
        public ShippingServiceCost ShippingServiceCost { get; set; }
        [Key]
        public string ShippingType { get; set; }
        public ListedShippingServiceCost ListedShippingServiceCost { get; set; }
    }

    public class OriginalRetailPrice
    {
        public double Value { get; set; }
        [Key]
        public string CurrencyID { get; set; }
    }

    public class DiscountPriceInfo
    {
        public OriginalRetailPrice OriginalRetailPrice { get; set; }
        [Key]
        public string PricingTreatment { get; set; }
        public bool SoldOneBay { get; set; }
        public bool SoldOffeBay { get; set; }
    }

    public class ItemByCategory
    {
        public string PrimaryCategoryID { get; set; }
        public string PrimaryCategoryName { get; set; }
    }

    public class Item : ItemByCategory
    {
        [Key]
        public string ItemID { get; set; }
        public string EndTime { get; set; }
        public string ViewItemURLForNaturalSearch { get; set; }
        public string ListingType { get; set; }
        public string GalleryURL { get; set; }
        public int BidCount { get; set; }
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }
        public string ListingStatus { get; set; }
        public string TimeLeft { get; set; }
        public string Title { get; set; }
        public ShippingCostSummary ShippingCostSummary { get; set; }
        public int WatchCount { get; set; }
        public DiscountPriceInfo DiscountPriceInfo { get; set; }
    }

    public class ItemArray
    {
        public List<Item> Item { get; set; }
    }

    public class Json
    {
        public string Timestamp { get; set; }
        [Key]
        public string Ack { get; set; }
        public string Build { get; set; }
        public string Version { get; set; }
        public ItemArray ItemArray { get; set; }
    }

    public class Results
    {
        [Key]
        public Json json { get; set; }
    }

    public class Query
    {
        [Key]
        public int count { get; set; }
        public string created { get; set; }
        public string lang { get; set; }
        public Results results { get; set; }
    }

    public class RootObject
    {
        [Key]
        public Query query { get; set; }
    }
}
