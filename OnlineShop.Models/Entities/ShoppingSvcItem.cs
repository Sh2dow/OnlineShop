﻿using Newtonsoft.Json;
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
        public Item()
        {
            PictureURL = new List<string>();
        }
        [JsonProperty("GalleryURL")]
        public string GalleryURL { get; set; }
        [JsonProperty]
        public List<string> PictureURL { get; set; }
        [JsonProperty("ConvertedCurrentPrice")]
        public ConvertedCurrentPrice ConvertedCurrentPrice { get; set; }
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

    public class SingleItem
    {

        [JsonProperty("Item")]
        public Item Item { get; set; }
    }
}
