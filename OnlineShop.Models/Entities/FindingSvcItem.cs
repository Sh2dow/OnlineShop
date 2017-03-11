using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OnlineShop.Models.FindingSvcItem
{
    public class ConvertedCurrentPrice
    {

        [JsonProperty("@currencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty("__value__")]
        public string Value { get; set; }
    }

    public class SellingStatus
    {
        [JsonProperty("convertedCurrentPrice")]
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }

        [JsonProperty("sellingState")]
        public string SellingState { get; set; }

        [JsonProperty("timeLeft")]
        public string TimeLeft { get; set; }
    }

    public class ListingInfo
    {
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }
    }
    public class Item
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("globalId")]
        public string GlobalId { get; set; }

        [JsonProperty("galleryPlusPictureURL")]
        public string GalleryPlusPictureURL { get; set; }

        [JsonProperty("listingInfo")]
        public ListingInfo ListingInfo { get; set; }

        [JsonProperty("sellingStatus")]
        public SellingStatus SellingStatus { get; set; }
    }

    public class SearchResult
    {
        [JsonProperty("@count")]
        public string Count { get; set; }

        [JsonProperty("item")]
        public List<Item> Item { get; set; }
    }

    public class FindItemsByKeywordsResponse
    {
        [JsonProperty("searchResult")]
        public SearchResult SearchResult { get; set; }
    }

    public class ItemsByKeywords
    {
        [JsonProperty("findItemsByKeywordsResponse")]
        public FindItemsByKeywordsResponse FindItemsByKeywordsResponse { get; set; }
    }

}