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

        public IEnumerable<Item> GrabItemsByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GrabItemsByCategory(int categoryId)
        {
            throw new NotImplementedException();
            //repo.AddProduct(product);
            //return repo.GetProductsByCategory(categoryId);
        }

        public void GrabJson(object json)
        {
            //RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(json.ToString());
            //foreach (var product in rootObj.query.results.json.ItemArray.Item)
            //var items = JObject.Parse(json.ToString()).ToObject<ItemArray>();
            //foreach (var product in items.Item)
            //{
            //    repo.AddProduct(product);
            //}
            //repo.Save();

            var items = new List<Item>();
            Item item;
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?version=957&appid=" + appID + "&callname=FindPopularItems&categoryId=" + "11116" + "&ResponseEncodingType=JSON";

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    var arr = JsonConvert.DeserializeObject<JArray>(s);
                    int i = 1;

                    foreach (var obj in arr)
                    {
                        item = new Item();
                        //item.ItemID = obj["ItemID"];
                        items.Add(item);
                    }
                }
            }
            catch
            {
                throw;
            }
            //return categories; 
        }
    }
}