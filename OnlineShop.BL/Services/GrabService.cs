using System;
using System.Collections.Generic;
using OnlineShop.DL;
using System.Net;
using Newtonsoft.Json;
using OnlineShop.BL.Services.Interfaces;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using OnlineShop.Models;
using ItemsByCategory = OnlineShop.Models.ItemsByCategory;
using ItemsByKeywords = OnlineShop.Models.ItemsByKeywords;

namespace OnlineShop.BL
{
    public class GrabService : IGrabService
    {
        private ProductsRepository repo;

        public GrabService()
        {
            repo = new ProductsRepository();
        }

        public void GrabTopItemsByKeyword(string keyword)
        {
            GrabJsonShoppingSvc("FindPopularItems&QueryKeywords=" + keyword);
        }

        public void GrabTopItemsByCategory(long id)
        {
            GrabJsonShoppingSvc("FindPopularItems&categoryId=" + id.ToString());
        }

        public void GrabJsonShoppingSvc(string input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string ShoppingApiAddress = ConfigurationManager.AppSettings["ShoppingApiAddress"];
            var url = ShoppingApiAddress + "&callname=" + input + "&ResponseEncodingType=JSON&appid=" + appID;
            try
            {
                var resultStream = new MemoryStream(LoadBytesFromUrl(url));
                var reader = new StreamReader(resultStream);
                string s = reader.ReadToEnd();
                var arr = JsonConvert.DeserializeObject<ItemsByCategory.Json>(s);
                var items = new List<ItemsByCategory.Item>();
                foreach (var obj in arr.ItemArray.Item)
                {
                    var item = new ItemsByCategory.Item();
                    item.ItemID = obj.ItemID;
                    item.ViewItemURLForNaturalSearch = obj.ViewItemURLForNaturalSearch;
                    item.PrimaryCategoryName = obj.PrimaryCategoryName;
                    item.PrimaryCategoryID = obj.PrimaryCategoryID;
                    item.Title = obj.Title;
                    item.EndTime = obj.EndTime;
                    item.ConvertedCurrentPrice = new ItemsByCategory.ConvertedCurrentPrice();
                    item.ConvertedCurrentPrice.CurrencyID = obj.ConvertedCurrentPrice.CurrencyID;
                    item.ConvertedCurrentPrice.Value = obj.ConvertedCurrentPrice.Value;
                    item.GalleryURL = obj.GalleryURL ?? "";
                    items.Add(item);
                }

                var itemsFinal = new List<LocalItem>();
                foreach (var item in items)
                {
                    if (itemsFinal.FindAll(x => x.ItemID == item.ItemID).Count < 1) //Sometimes there are items with duplicate PK
                        itemsFinal.Add(ConvertJsonShoppingSvcToLocalItem(item));
                }
                Debug.Print("items count: " + items.Count);
                Debug.Print("itemsFinal count: " + itemsFinal.Count);
                repo.AddProducts(itemsFinal);
            }
            catch (Exception ex)
            {
                Debug.Print("ExceptionMessage: " + ex.Message.ToString());
            }
        }

        public LocalItem ConvertJsonShoppingSvcToLocalItem(ItemsByCategory.Item item)
        {
            Debug.Print("ItemID: " + item.ItemID);
            return new LocalItem
            {
                ItemID = item.ItemID,
                Title = item.Title,
                ViewItemURLForNaturalSearch = item.ViewItemURLForNaturalSearch,
                EndTime = item.EndTime,
                Price = item.ConvertedCurrentPrice.Value,
                PrimaryCategoryID = long.Parse(item.PrimaryCategoryID),
                PrimaryCategoryName = item.PrimaryCategoryName,
                Image = Convert.ToBase64String(LoadBytesFromUrl(item.GalleryURL))
            };
        }

        public LocalItem ExtendItem(LocalItem localitem)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string FindingApiAddress = ConfigurationManager.AppSettings["FindingApiAddress"];
            var url = FindingApiAddress + "&callname=OPERATION-NAME=findItemsByKeywords&keywords=" + localitem.Title + "&sortOrder=startTime&RESPONSE-DATA-FORMAT=JSON&SECURITY-APPNAME=" + appID;
            try
            {
                var resultStream = new MemoryStream(LoadBytesFromUrl(url));
                var reader = new StreamReader(resultStream);
                string s = reader.ReadToEnd();
                var arr = JsonConvert.DeserializeObject<ItemsByKeywords.SearchResult>(s);
                localitem.PriceArray = new Dictionary<DateTime, string>();
                foreach (var obj in arr.Item)
                {
                    var key = new DateTime();
                    var val = "";
                    if (obj.ItemId == localitem.ItemID) { localitem.galleryPlusPictureURL = Convert.ToBase64String(LoadBytesFromUrl(obj.GalleryPlusPictureURL)); }
                    foreach (var sellingStatus in obj.SellingStatus)
                    {
                        foreach (var currentPrice in sellingStatus.CurrentPrice)
                        {
                            val = currentPrice.Value;
                        }
                    }
                    foreach (var listingInfo in obj.ListingInfo)
                    {
                        key = listingInfo.StartTime;
                    }
                    localitem.PriceArray.Add(key, val);
                }
                repo.AddProduct(localitem);
                repo.Save();
            }
            catch (Exception ex)
            {
                Debug.Print("ExceptionMessage: " + ex.Message.ToString());
            }
            return localitem;
        }

        public byte[] LoadBytesFromUrl(string url)
        {
            byte[] data = new byte[0];
            if (string.IsNullOrEmpty(url)) return data;
            var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            var response = (HttpWebResponse)webRequest.GetResponse();
            Debug.Print("webResponse.ContentLength: " + response.ContentLength);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var mstrm = new MemoryStream())
                {
                    using (var s = response.GetResponseStream())
                    {
                        var tempBuffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = s.Read(tempBuffer, 0, tempBuffer.Length)) != 0)
                        {
                            mstrm.Write(tempBuffer, 0, bytesRead);
                        }
                    }
                    mstrm.Flush();
                    data = mstrm.GetBuffer();
                }
            }
            return data;
        }
    }
}