using System;
using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;
using System.Net;
using Newtonsoft.Json;
using OnlineShop.BL.Services.Interfaces;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.BL
{
    public class GrabService : IGrabService
    {
        private ProductsRepository repo;

        public GrabService()
        {
            repo = new ProductsRepository();
        }

        public void GrabItemsByKeyword(string keyword)
        {
            GrabJson(new[] { "version=713&", "&QueryKeywords=" + keyword });
        }

        public void GrabItemsByCategory(long categoryId)
        {
            GrabJson(new[] { "version=957&", "&categoryId=" + categoryId.ToString() });
        }

        public void GrabJson(string[] input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";
            try
            {
                var resultStream = new MemoryStream(LoadBytesFromUrl(url));
                var reader = new StreamReader(resultStream);
                string s = reader.ReadToEnd();
                var arr = JsonConvert.DeserializeObject<Json>(s);
                var items = new List<Item>();
                foreach (var obj in arr.ItemArray.Item)
                {
                    var item = new Item();
                    item.ItemID = (string)obj.ItemID;
                    item.PrimaryCategoryName = (string)obj.PrimaryCategoryName;
                    item.PrimaryCategoryID = (string)obj.PrimaryCategoryID;
                    item.Title = (string)obj.Title;
                    item.EndTime = obj.EndTime;
                    item.ConvertedCurrentPrice = new ConvertedCurrentPrice();
                    item.ConvertedCurrentPrice.CurrencyID = (string)obj.ConvertedCurrentPrice.CurrencyID;
                    item.ConvertedCurrentPrice.Value = (double)obj.ConvertedCurrentPrice.Value;
                    item.GalleryURL = (string)obj.GalleryURL ?? "";
                    items.Add(item);
                }

                var itemsFinal = new List<ItemFinal>();
                foreach (var item in items)
                {
                    if (itemsFinal.FindAll(x => x.ItemID == item.ItemID).Count < 1) //Sometimes there are items with duplicate PK
                        itemsFinal.Add(ConvertToItemFinal(item));
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

        public async void GrabJsonAsync(string[] input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";

            var request = WebRequest.Create(new Uri(url));
            var response = (HttpWebResponse)await Task.Factory
                .FromAsync<WebResponse>(request.BeginGetResponse,
                                        request.EndGetResponse,
                                        null);
        }

        public ItemFinal ConvertToItemFinal(Item item)
        {
            Debug.Print("ItemID: " + item.ItemID);
            return new ItemFinal
            {
                ItemID = item.ItemID,
                Title = item.Title,
                EndTime = item.EndTime,
                Price = String.Concat(item.ConvertedCurrentPrice.Value.ToString(), " ", (string)item.ConvertedCurrentPrice.CurrencyID),
                PrimaryCategoryID = int.Parse(item.PrimaryCategoryID),
                PrimaryCategoryName = item.PrimaryCategoryName,
                Image = (LoadBytesFromUrl(item.GalleryURL))
            };
        }

        public byte[] LoadBytesFromUrl(string url)
        {
            byte[] data = new byte[0];
            if (String.IsNullOrEmpty(url)) return data;
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

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }
}