using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OnlineShop.Models
{
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

    public class ItemFinal : ItemBase
    {
        public string Image { get; set; }
        public double Price { get; set; }
        [Required]
        public long PrimaryCategoryID { get; set; }
    }
}
