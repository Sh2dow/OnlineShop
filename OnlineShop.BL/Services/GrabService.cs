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
using ShoppingItem = OnlineShop.Models.ShoppingSvcItem.Item;
using System.Xml;

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
                var arr = GetDataFromWebClient<Models.ShoppingSvcItem.Json>(url);
                var items = arr.ItemArray.Items;
                var itemsFinal = new List<StoreItem>();
                foreach (var item in items)
                {
                    if (itemsFinal.FindAll(x => x.ItemID == item.ItemID).Count < 1) //Sometimes there are items with duplicate PK
                        itemsFinal.Add(ConvertJsonShoppingSvcToStoreItem(item));
                }
                repo.AddProducts(itemsFinal);
            }
            catch (Exception ex)
            {
                Debug.Print("ExceptionMessage: " + ex.Message.ToString());
            }
        }

        public StoreItem ConvertJsonShoppingSvcToStoreItem(ShoppingItem item)
        {
            Debug.Print("ItemID: " + item.ItemID);
            return new StoreItem
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

        public StoreItem ExtendItem(StoreItem localitem)
        {
            string appID = ConfigurationManager.AppSettings["AppID"];
            string FindingApiAddress = ConfigurationManager.AppSettings["FindingApiAddress"];
            var url = FindingApiAddress + "&SECURITY-APPNAME=" + appID + "&sortOrder=startTime&RESPONSE-DATA-FORMAT=xml&REST-PAYLOAD&callname=OPERATION-NAME=findItemsByKeywords&keywords=" + localitem.Title.Replace("&", " ");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(url);
                XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
                manager.AddNamespace("ns", doc.DocumentElement.NamespaceURI);
                foreach (XmlNode node in doc.SelectNodes("//ns:item", manager))
                {
                    string pakey = "", pavalue = "";
                    if (localitem.ItemID == node["itemId"].InnerText && !(node["galleryPlusPictureURL"] == null) && !string.IsNullOrEmpty(node["galleryPlusPictureURL"].InnerText))
                    {
                        localitem.Image = Convert.ToBase64String(LoadBytesFromUrl(node["galleryPlusPictureURL"].InnerText));
                    }
                    foreach (XmlNode listingInfo in doc.SelectNodes("//ns:listingInfo", manager))
                    {
                        pakey = listingInfo["startTime"].InnerText;
                    }
                    foreach (XmlNode listingInfo in doc.SelectNodes("//ns:sellingStatus", manager))
                    {
                        pavalue = listingInfo["convertedCurrentPrice"].InnerText;
                    }

                    if (!localitem.PriceArray.ContainsKey(Convert.ToDateTime(pakey)))
                    {
                        localitem.PriceArray.Add(Convert.ToDateTime(pakey), pavalue);
                    }
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

        public static T GetDataFromWebClient<T>(string _url)
        {
            using (var webClient = new WebClient())
            {
                webClient.BaseAddress = _url;
                return JsonConvert.DeserializeObject<T>(webClient.DownloadString(_url));
            }
        }

        public byte[] LoadBytesFromUrl(string url)
        {
            byte[] data = new byte[0];
            if (string.IsNullOrEmpty(url)) return data;
            var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            var response = (HttpWebResponse)webRequest.GetResponse();
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