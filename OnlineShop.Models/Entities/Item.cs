using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace OnlineShop.Models
{
    public abstract class ItemBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ItemID")]
        [DisplayName("id")]
        public string ItemID { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty]
        [DisplayName("ebay Url")]
        public string ViewItemURLForNaturalSearch { get; set; }

        [JsonProperty("EndTime")]
        [DisplayName("Ending time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("PrimaryCategoryName")]
        public string PrimaryCategoryName { get; set; }

        [JsonProperty("PrimaryCategoryID")]
        [Required]
        public string PrimaryCategoryID { get; set; }
    }

    public class StoreItem : ItemBase
    {
        public string Image { get; set; }
        public double Price { get; set; }
        public List<DataPoint> PriceArray { get; set; }
    }

    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.X = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON. 
        [Key]
        [DataMember(Name = "label")]
        public string X { get; set; }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double Y { get; set; }
    }
}
