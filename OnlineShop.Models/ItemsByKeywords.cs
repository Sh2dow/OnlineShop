using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models.ItemsByKeywords
{
    public class PrimaryCategory
    {

        [JsonProperty("categoryId")]
        public IList<string> CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public IList<string> CategoryName { get; set; }
    }

    public class ShippingServiceCost
    {

        [JsonProperty("@currencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty("__value__")]
        public string Value { get; set; }
    }

    public class ShippingInfo
    {

        [JsonProperty("shippingServiceCost")]
        public IList<ShippingServiceCost> ShippingServiceCost { get; set; }

        [JsonProperty("shippingType")]
        public IList<string> ShippingType { get; set; }

        [JsonProperty("shipToLocations")]
        public IList<string> ShipToLocations { get; set; }

        [JsonProperty("expeditedShipping")]
        public IList<string> ExpeditedShipping { get; set; }

        [JsonProperty("oneDayShippingAvailable")]
        public IList<string> OneDayShippingAvailable { get; set; }

        [JsonProperty("handlingTime")]
        public IList<string> HandlingTime { get; set; }
    }

    public class CurrentPrice
    {

        [JsonProperty("@currencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty("__value__")]
        public string Value { get; set; }
    }

    public class ConvertedCurrentPrice
    {

        [JsonProperty("@currencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty("__value__")]
        public string Value { get; set; }
    }

    public class SellingStatu
    {

        [JsonProperty("currentPrice")]
        public IList<CurrentPrice> CurrentPrice { get; set; }

        [JsonProperty("convertedCurrentPrice")]
        public IList<ConvertedCurrentPrice> ConvertedCurrentPrice { get; set; }

        [JsonProperty("sellingState")]
        public IList<string> SellingState { get; set; }

        [JsonProperty("timeLeft")]
        public IList<string> TimeLeft { get; set; }
    }

    public class ListingInfo
    {

        [JsonProperty("bestOfferEnabled")]
        public IList<string> BestOfferEnabled { get; set; }

        [JsonProperty("buyItNowAvailable")]
        public IList<string> BuyItNowAvailable { get; set; }

        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endTime")]
        public IList<DateTime> EndTime { get; set; }

        [JsonProperty("listingType")]
        public IList<string> ListingType { get; set; }

        [JsonProperty("gift")]
        public IList<string> Gift { get; set; }
    }

    public class Condition
    {

        [JsonProperty("conditionId")]
        public IList<string> ConditionId { get; set; }

        [JsonProperty("conditionDisplayName")]
        public IList<string> ConditionDisplayName { get; set; }
    }

    public class Item
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("title")]
        public IList<string> Title { get; set; }

        [JsonProperty("globalId")]
        public IList<string> GlobalId { get; set; }

        [JsonProperty("primaryCategory")]
        public IList<PrimaryCategory> PrimaryCategory { get; set; }

        [JsonProperty("galleryURL")]
        public IList<string> GalleryURL { get; set; }

        [JsonProperty("viewItemURL")]
        public IList<string> ViewItemURL { get; set; }

        [JsonProperty("paymentMethod")]
        public IList<string> PaymentMethod { get; set; }

        [JsonProperty("autoPay")]
        public IList<string> AutoPay { get; set; }

        [JsonProperty("location")]
        public IList<string> Location { get; set; }

        [JsonProperty("country")]
        public IList<string> Country { get; set; }

        [JsonProperty("shippingInfo")]
        public IList<ShippingInfo> ShippingInfo { get; set; }

        [JsonProperty("sellingStatus")]
        public IList<SellingStatu> SellingStatus { get; set; }

        [JsonProperty("listingInfo")]
        public IList<ListingInfo> ListingInfo { get; set; }

        [JsonProperty("returnsAccepted")]
        public IList<string> ReturnsAccepted { get; set; }

        [JsonProperty("galleryPlusPictureURL")]
        public string GalleryPlusPictureURL { get; set; }

        [JsonProperty("condition")]
        public IList<Condition> Condition { get; set; }

        [JsonProperty("isMultiVariationListing")]
        public IList<string> IsMultiVariationListing { get; set; }

        [JsonProperty("topRatedListing")]
        public IList<string> TopRatedListing { get; set; }
    }

    public class SearchResult
    {

        [JsonProperty("@count")]
        public string Count { get; set; }

        [JsonProperty("item")]
        public IList<Item> Item { get; set; }
    }

    public class PaginationOutput
    {

        [JsonProperty("pageNumber")]
        public IList<string> PageNumber { get; set; }

        [JsonProperty("entriesPerPage")]
        public IList<string> EntriesPerPage { get; set; }

        [JsonProperty("totalPages")]
        public IList<string> TotalPages { get; set; }

        [JsonProperty("totalEntries")]
        public IList<string> TotalEntries { get; set; }
    }

    public class FindItemsByKeywordsResponse
    {

        [JsonProperty("ack")]
        public IList<string> Ack { get; set; }

        [JsonProperty("version")]
        public IList<string> Version { get; set; }

        [JsonProperty("timestamp")]
        public IList<DateTime> Timestamp { get; set; }

        [JsonProperty("searchResult")]
        public IList<SearchResult> SearchResult { get; set; }

        [JsonProperty("paginationOutput")]
        public IList<PaginationOutput> PaginationOutput { get; set; }

        [JsonProperty("itemSearchURL")]
        public IList<string> ItemSearchURL { get; set; }
    }

    public class ItemsByKeywords
    {

        [JsonProperty("findItemsByKeywordsResponse")]
        public IList<FindItemsByKeywordsResponse> FindItemsByKeywordsResponse { get; set; }
    }
}
