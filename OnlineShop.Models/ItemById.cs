using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OnlineShop.Models.ItemById
{
    public class ConvertedCurrentPrice
    {

        [JsonProperty("Value")]
        public double Value { get; set; }

        [JsonProperty("CurrencyID")]
        public string CurrencyID { get; set; }
    }

    public class ShippingServiceCost
    {

        [JsonProperty("Value")]
        public double Value { get; set; }

        [JsonProperty("CurrencyID")]
        public string CurrencyID { get; set; }
    }

    public class ListedShippingServiceCost
    {

        [JsonProperty("Value")]
        public double Value { get; set; }

        [JsonProperty("CurrencyID")]
        public string CurrencyID { get; set; }
    }

    public class ShippingCostSummary
    {

        [JsonProperty("ShippingServiceCost")]
        public ShippingServiceCost ShippingServiceCost { get; set; }

        [JsonProperty("ShippingType")]
        public string ShippingType { get; set; }

        [JsonProperty("ListedShippingServiceCost")]
        public ListedShippingServiceCost ListedShippingServiceCost { get; set; }
    }

    public class NameValueList
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public IList<string> Value { get; set; }
    }

    public class ItemSpecifics
    {

        [JsonProperty("NameValueList")]
        public IList<NameValueList> NameValueList { get; set; }
    }

    public class Item
    {

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ItemID")]
        public string ItemID { get; set; }

        [JsonProperty("EndTime")]
        public DateTime EndTime { get; set; }

        [JsonProperty("ViewItemURLForNaturalSearch")]
        public string ViewItemURLForNaturalSearch { get; set; }

        [JsonProperty("ListingType")]
        public string ListingType { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("GalleryURL")]
        public string GalleryURL { get; set; }

        [JsonProperty("PictureURL")]
        public IList<string> PictureURL { get; set; }

        [JsonProperty("PrimaryCategoryID")]
        public string PrimaryCategoryID { get; set; }

        [JsonProperty("PrimaryCategoryName")]
        public string PrimaryCategoryName { get; set; }

        [JsonProperty("BidCount")]
        public int BidCount { get; set; }

        [JsonProperty("ConvertedCurrentPrice")]
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }

        [JsonProperty("ListingStatus")]
        public string ListingStatus { get; set; }

        [JsonProperty("TimeLeft")]
        public string TimeLeft { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("ShippingCostSummary")]
        public ShippingCostSummary ShippingCostSummary { get; set; }

        [JsonProperty("ItemSpecifics")]
        public ItemSpecifics ItemSpecifics { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("AutoPay")]
        public bool AutoPay { get; set; }

        [JsonProperty("QuantityAvailableHint")]
        public string QuantityAvailableHint { get; set; }

        [JsonProperty("QuantityThreshold")]
        public int QuantityThreshold { get; set; }

        [JsonProperty("ConditionDescription")]
        public string ConditionDescription { get; set; }
    }

    public class ItemById
    {

        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("Ack")]
        public string Ack { get; set; }

        [JsonProperty("Build")]
        public string Build { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("Item")]
        public Item Item { get; set; }
    }


}
