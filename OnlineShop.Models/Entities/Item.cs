using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;

namespace OnlineShop.Models
{
    public abstract class ItemBase
    {
        [Key]
        [Required]
        [JsonProperty]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("id")]
        public string ItemID { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty, DisplayName("ebay Url")]
        public string ViewItemURLForNaturalSearch { get; set; }
        [JsonProperty, DisplayName("Ending time")]
        public DateTime EndTime { get; set; }
        [JsonProperty, DisplayName("Category")]
        public string PrimaryCategoryName { get; set; }
    }

    public class StoreItem : ItemBase
    {
        //public StoreItem()
        //{
        //    List<DataPoint> PriceArray = new List<DataPoint>();
        //}
        public string Image { get; set; }
        public double Price { get; set; }
        [Required, DisplayName("Category id")]
        public long PrimaryCategoryID { get; set; }
        public List<DataPoint> PriceArray { get; set; }
    }

    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON. 
        [Key]
        [DataMember(Name = "label")]
        public string Label { get; set; }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double Y { get; set; }
    }
}
