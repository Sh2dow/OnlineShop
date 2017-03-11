using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.ShoppingSvcItem
{
    public class ConvertedCurrentPrice
    {
        [Key]
        public double Value { get; set; }
        public string CurrencyID { get; set; }
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
        public string TimeLeft { get; set; }
        [JsonProperty]
        public string WatchCount { get; set; }
    }
    
    public class ItemArray
    {
        [JsonProperty("Item")]
        public List<Item> Items { get; set; }
    }

    public class Json
    {
        public ItemArray ItemArray { get; set; }
    }
}
