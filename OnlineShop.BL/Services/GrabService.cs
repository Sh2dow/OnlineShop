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
            return GrabJson(new[] { "version=713&", "&QueryKeywords=" + keyword });
        }

        public IEnumerable<Item> GrabItemsByCategory(int categoryId)
        {
            return GrabJson(new[] { "version=957&", "&categoryId=" + categoryId.ToString() });
        }

        public IEnumerable<Item> GrabJson(string[] input)
        {
            var items = new List<Item>();
            Item item;
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";

            try
            {
                // Create a request for the URL.   
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.  
                var reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Debug.WriteLine(responseFromServer);

                //RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(json.ToString());
                //foreach (var product in rootObj.query.results.json.ItemArray.Item)
                //var items = JObject.Parse(json.ToString()).ToObject<ItemArray>();
                //foreach (var product in items.Item)
                //{
                //    repo.AddProduct(product);
                //}
                if ((((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) && (response.ContentLength > 0))
                {
                    var arr = JsonConvert.DeserializeObject<JArray>(responseFromServer);
                    foreach (var obj in arr)
                    {
                        item = new Item();
                        //item.ItemID = obj.["ItemID"];
                        items.Add(item);
                    }
                    repo.Save();

                    // Clean up the streams and the response.  
                    reader.Close();
                    response.Close();
                }
            }
            catch
            {
                throw;
            }
            return items;
        }
    }
}