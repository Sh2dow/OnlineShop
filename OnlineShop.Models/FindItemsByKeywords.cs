using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.FindItemsByKeywords
{
    public class PrimaryCategory
    {

        [JsonProperty("categoryId")]
        public IList<string> categoryId { get; set; }

        [JsonProperty("categoryName")]
        public IList<string> categoryName { get; set; }
    }

    public class ShippingServiceCost
    {

        [JsonProperty("@currencyId")]
        public string @currencyId { get; set; }

        [JsonProperty("__value__")]
        public string __value__ { get; set; }
    }

    public class ShippingInfo
    {

        [JsonProperty("shippingServiceCost")]
        public IList<ShippingServiceCost> shippingServiceCost { get; set; }

        [JsonProperty("shippingType")]
        public IList<string> shippingType { get; set; }

        [JsonProperty("shipToLocations")]
        public IList<string> shipToLocations { get; set; }

        [JsonProperty("expeditedShipping")]
        public IList<string> expeditedShipping { get; set; }

        [JsonProperty("oneDayShippingAvailable")]
        public IList<string> oneDayShippingAvailable { get; set; }

        [JsonProperty("handlingTime")]
        public IList<string> handlingTime { get; set; }
    }

    public class CurrentPrice
    {

        [JsonProperty("@currencyId")]
        public string @currencyId { get; set; }

        [JsonProperty("__value__")]
        public string __value__ { get; set; }
    }

    public class ConvertedCurrentPrice
    {

        [JsonProperty("@currencyId")]
        public string @currencyId { get; set; }

        [JsonProperty("__value__")]
        public string __value__ { get; set; }
    }

    public class SellingStatu
    {

        [JsonProperty("currentPrice")]
        public IList<CurrentPrice> currentPrice { get; set; }

        [JsonProperty("convertedCurrentPrice")]
        public IList<ConvertedCurrentPrice> convertedCurrentPrice { get; set; }

        [JsonProperty("sellingState")]
        public IList<string> sellingState { get; set; }

        [JsonProperty("timeLeft")]
        public IList<string> timeLeft { get; set; }
    }

    public class ListingInfo
    {

        [JsonProperty("bestOfferEnabled")]
        public IList<string> bestOfferEnabled { get; set; }

        [JsonProperty("buyItNowAvailable")]
        public IList<string> buyItNowAvailable { get; set; }

        [JsonProperty("startTime")]
        public IList<DateTime> startTime { get; set; }

        [JsonProperty("endTime")]
        public IList<DateTime> endTime { get; set; }

        [JsonProperty("listingType")]
        public IList<string> listingType { get; set; }

        [JsonProperty("gift")]
        public IList<string> gift { get; set; }
    }

    public class Condition
    {

        [JsonProperty("conditionId")]
        public IList<string> conditionId { get; set; }

        [JsonProperty("conditionDisplayName")]
        public IList<string> conditionDisplayName { get; set; }
    }

    public class Item
    {

        [JsonProperty("itemId")]
        public IList<string> itemId { get; set; }

        [JsonProperty("title")]
        public IList<string> title { get; set; }

        [JsonProperty("globalId")]
        public IList<string> globalId { get; set; }

        [JsonProperty("primaryCategory")]
        public IList<PrimaryCategory> primaryCategory { get; set; }

        [JsonProperty("galleryURL")]
        public IList<string> galleryURL { get; set; }

        [JsonProperty("viewItemURL")]
        public IList<string> viewItemURL { get; set; }

        [JsonProperty("paymentMethod")]
        public IList<string> paymentMethod { get; set; }

        [JsonProperty("autoPay")]
        public IList<string> autoPay { get; set; }

        [JsonProperty("location")]
        public IList<string> location { get; set; }

        [JsonProperty("country")]
        public IList<string> country { get; set; }

        [JsonProperty("shippingInfo")]
        public IList<ShippingInfo> shippingInfo { get; set; }

        [JsonProperty("sellingStatus")]
        public IList<SellingStatu> sellingStatus { get; set; }

        [JsonProperty("listingInfo")]
        public IList<ListingInfo> listingInfo { get; set; }

        [JsonProperty("returnsAccepted")]
        public IList<string> returnsAccepted { get; set; }

        [JsonProperty("galleryPlusPictureURL")]
        public IList<string> galleryPlusPictureURL { get; set; }

        [JsonProperty("condition")]
        public IList<Condition> condition { get; set; }

        [JsonProperty("isMultiVariationListing")]
        public IList<string> isMultiVariationListing { get; set; }

        [JsonProperty("topRatedListing")]
        public IList<string> topRatedListing { get; set; }
    }

    public class SearchResult
    {

        [JsonProperty("@count")]
        public string @count { get; set; }

        [JsonProperty("item")]
        public IList<Item> item { get; set; }
    }

    public class PaginationOutput
    {

        [JsonProperty("pageNumber")]
        public IList<string> pageNumber { get; set; }

        [JsonProperty("entriesPerPage")]
        public IList<string> entriesPerPage { get; set; }

        [JsonProperty("totalPages")]
        public IList<string> totalPages { get; set; }

        [JsonProperty("totalEntries")]
        public IList<string> totalEntries { get; set; }
    }

    public class FindItemsByKeywordsResponse
    {

        [JsonProperty("ack")]
        public IList<string> ack { get; set; }

        [JsonProperty("version")]
        public IList<string> version { get; set; }

        [JsonProperty("timestamp")]
        public IList<DateTime> timestamp { get; set; }

        [JsonProperty("searchResult")]
        public IList<SearchResult> searchResult { get; set; }

        [JsonProperty("paginationOutput")]
        public IList<PaginationOutput> paginationOutput { get; set; }

        [JsonProperty("itemSearchURL")]
        public IList<string> itemSearchURL { get; set; }
    }

    public class findItemsByKeywords
    {

        [JsonProperty("findItemsByKeywordsResponse")]
        public IList<FindItemsByKeywordsResponse> findItemsByKeywordsResponse { get; set; }
    }


}
