using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Product
    {
        public Int32 Id { get; set; }
        public string Title { get; set; }
        public string seller { get; set; }
        public string condition { get; set; }
        public string ItemWebUrl { get; set; }
        [DataMember(Name = "price")]
        public Decimal Cost { get; set; }
        public string itemLocation { get; set; }
        [DataMember(Name = "itemId")]
        public string EbayItemID { get; set; }
        public string[] Categories { get; set; }
    }
}