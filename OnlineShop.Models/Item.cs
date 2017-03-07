using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double Value { get; set; }
        [Key]
        public string CurrencyID { get; set; }
    }

    public class ListedShippingServiceCost
    {
        public double Value { get; set; }
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

    public class ItemFinal : ItemBase
    {
        public string Image { get; set; }
        public string Price { get; set; }
        [Required]
        public long PrimaryCategoryID { get; set; }
    }

    public class Item: ItemBase
    {
        [Required, JsonProperty]
        public string PrimaryCategoryID { get; set; }
        [JsonProperty]
        public string ListingType { get; set; }
        [JsonProperty]
        public string GalleryURL { get; set; }
        [JsonProperty]
        public string BidCount { get; set; }
        [JsonProperty]
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }
        [JsonProperty]
        public string ListingStatus { get; set; }
        [JsonProperty]
        public string TimeLeft { get; set; }
        [JsonProperty]
        public ShippingCostSummary ShippingCostSummary { get; set; }
        [JsonProperty]
        public string WatchCount { get; set; }
        [JsonProperty]
        public DiscountPriceInfo DiscountPriceInfo { get; set; }
    }

    public abstract class ItemBase
    {
        [Key, Required, JsonProperty, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemID { get; set; }
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty]
        public string ViewItemURLForNaturalSearch { get; set; }
        [JsonProperty]
        public DateTime EndTime { get; set; }
        [JsonProperty]
        public string PrimaryCategoryName { get; set; }
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

    //public class Results
    //{
    //    [Key]
    //    public Json json { get; set; }
    //}

    //public class Query
    //{
    //    [Key]
    //    public int count { get; set; }
    //    public string created { get; set; }
    //    public string lang { get; set; }
    //    public Results results { get; set; }
    //}

    //public class RootObject
    //{
    //    [Key]
    //    public Query query { get; set; }
    //}
}
