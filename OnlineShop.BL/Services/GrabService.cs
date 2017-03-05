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
                    //var rootObj = JsonConvert.DeserializeObject<RootObject>(s);
                    var items = new List<Item>();
                    //foreach (var obj in rootObj.query.results.json.ItemArray.Item)
                    //Root.SelectToken("ItemArray[0].Item[0]")
                    foreach (var obj in arr.ItemArray.Item)
                    {
                        var item = new Item();
                        item.ItemID = (string)obj.ItemID;
                        item.PrimaryCategoryName = (string)obj.PrimaryCategoryName;
                        item.PrimaryCategoryID = (string)obj.PrimaryCategoryID;
                        item.Title = (string)obj.Title;
                        item.EndTime = (string)obj.EndTime;
                        //item.ItemID = (string)obj["ItemID"];
                        //item.PrimaryCategoryName = (string)obj["PrimaryCategoryName"];
                        //item.PrimaryCategoryID = (string)obj["PrimaryCategoryID"];
                        //item.Title = (string)obj["Title"];
                        //item.EndTime = (string)obj["EndTime"];
                        items.Add(item);
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

        public void _GrabJson(string[] input)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            var url = findingServerAddress + "shopping?" + input[0] + "appid=" + appID + "&callname=FindPopularItems" + input[1] + "&ResponseEncodingType=JSON";

            try
            {
                // Create a request for the URL.   
                Debug.WriteLine(url);
                var request = WebRequest.Create(url);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                var response = request.GetResponse();
                // Display the status.  
                Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.  
                var reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Debug.WriteLine(responseFromServer);
                if ((((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) && (response.ContentLength > 0))
                {
                    var items = JsonConvert.DeserializeObject<ItemArray>(responseFromServer);
                    Debug.WriteLine("items.Item: " + items.Item.ToString());
                    repo.AddProducts(items.Item);

                    //var rootObj = JsonConvert.DeserializeObject<RootObject>(responseFromServer);
                    ////foreach (var product in items.Item)
                    //foreach (var product in rootObj.query.results.json.ItemArray.Item)
                    //{
                    //    var myProduct = JsonConvert.DeserializeObject<Item>(product);
                    //    repo.AddProduct((Item)product);
                    //}

                    //var arr = JsonConvert.DeserializeObject<JObject>(responseFromServer);
                    //foreach (var obj in arr)
                    //{
                    //    item = new Item();
                    //    //item.ItemID = obj.["ItemID"];
                    //    items.Add(item);
                    //}
                    repo.Save();

                    // Clean up the streams and the response.  
                    reader.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExceprionMessage: " + ex.Message.ToString());
                Debug.WriteLine("InnerException: " + ex.InnerException.ToString());
            }
        }
    }
}