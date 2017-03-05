using System;
using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;
using System.Net;
using Newtonsoft.Json;
using OnlineShop.BL.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
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

        public void AddProducts(IEnumerable<Item> products)
        {

            foreach (var item in products)
            {

            }
            repo.Save();
        }

        public void GrabItemsByKeyword(string keyword)
        {
            GrabJson(new[] { "version=713&", "&QueryKeywords=" + keyword });
        }

        public void GrabItemsByCategory(int categoryId)
        {
            GrabJson(new[] { "version=957&", "&categoryId=" + categoryId.ToString() });
        }

        public void GrabJson(string[] input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";

            var response = (HttpWebResponse)webRequest.GetResponse();
            byte[] data; // will eventually hold the result
                         // create a MemoryStream to build the result
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

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                Debug.WriteLine("webResponse.ContentLength: " + webResponse.ContentLength);
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (data.Length > 0))
                {
                    var resultStream = new MemoryStream(data);
                    var reader = new StreamReader(resultStream);
                    string s = reader.ReadToEnd();
                    var arr = JsonConvert.DeserializeObject<Json>(s);
                    var items = new List<ItemFinal>();
                    foreach (var obj in arr.ItemArray.Item)
                    {
                        var item = new Item();
                        item.ItemID = (string)obj.ItemID;
                        item.PrimaryCategoryName = (string)obj.PrimaryCategoryName;
                        item.PrimaryCategoryID = (string)obj.PrimaryCategoryID;
                        item.Title = (string)obj.Title;
                        item.EndTime = (string)obj.EndTime;
                        item.Price.Value = (double)obj.Price.Value;
                        item.Price.CurrencyID = (string)obj.Price.CurrencyID;
                        item.GalleryURL = (string)obj.GalleryURL;
                        items.Add(ConvertToItemFinal(item));
                    }

                    repo.AddProducts(items);
                    repo.Save();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExceprionMessage: " + ex.Message.ToString());
                //Debug.WriteLine("InnerException: " + ex.InnerException.ToString());
            }
        }

        public async void GrabJsonAsync(string[] input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";

            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)await Task.Factory
                .FromAsync<WebResponse>(request.BeginGetResponse,
                                        request.EndGetResponse,
                                        null);
        }

        public ItemFinal ConvertToItemFinal(Item item)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(item.GalleryURL);
            webRequest.Method = "GET";

            var response = (HttpWebResponse)webRequest.GetResponse();
            byte[] data; // will eventually hold the result
                         // create a MemoryStream to build the result
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

            return new ItemFinal
            {
                ItemID = int.Parse(item.ItemID),
                Title = item.Title,
                EndTime = item.EndTime,
                Price = (decimal)item.Price.Value,
                PrimaryCategoryID = int.Parse(item.PrimaryCategoryID),
                PrimaryCategoryName = item.PrimaryCategoryName,
                Image = data
            };
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