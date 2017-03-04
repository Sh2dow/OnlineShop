using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

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

    public class Product
    {
        [Key]
        public int ItemID { get; set; }
        public DateTime EndTime { get; set; }
        public string ViewItemURLForNaturalSearch { get; set; }
        public string ListingType { get; set; }
        public string GalleryURL { get; set; }
        public int PrimaryCategoryID { get; set; }
        public string PrimaryCategoryName { get; set; }
        public int BidCount { get; set; }
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }
        public string ListingStatus { get; set; }
        public string TimeLeft { get; set; }
        public string Title { get; set; }
        public ShippingCostSummary ShippingCostSummary { get; set; }
        public int WatchCount { get; set; }
    }

}