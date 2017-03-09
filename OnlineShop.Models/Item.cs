using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OnlineShop.Models
{
    public abstract class ItemBase
    {
        [Key, Required, JsonProperty, DatabaseGenerated(DatabaseGeneratedOption.None), DisplayName("id")]
        public string ItemID { get; set; }
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty, DisplayName("ebay Url")]
        public string ViewItemURLForNaturalSearch { get; set; }
        [JsonProperty, DisplayName("Ending time")]
        public DateTime EndTime { get; set; }
        [JsonProperty, DisplayName("Category")]
        public string PrimaryCategoryName { get; set; }
    }

    public class ItemFinal : ItemBase
    {
        public string Image { get; set; }
        public double Price { get; set; }
        [Required, DisplayName("Category id")]
        public long PrimaryCategoryID { get; set; }
    }
}
