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
using HtmlAgilityPack;
using System.Linq;
using System.Globalization;

namespace OnlineShop.BL
{
    public class GrabService : IGrabService
    {
        private ProductsRepository repo;
        string appID;
        string FindingApiAddress;
        string ShoppingApiAddress;

        public GrabService()
        {
            appID = ConfigurationManager.AppSettings["AppID"];
            FindingApiAddress = ConfigurationManager.AppSettings["FindingApiAddress"];
            ShoppingApiAddress = ConfigurationManager.AppSettings["ShoppingApiAddress"];
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
        public StoreItem GrabSingleItem(StoreItem localitem)
        {
            var url = ShoppingApiAddress + "&callname=GetSingleItem&IncludeSelector=Description&ItemID=" + localitem.ItemID + "&ResponseEncodingType=JSON&appid=" + appID;
            try
            {
                var result = GetDataFromWebClient<Models.ShoppingSvcItem.SingleItem>(url);
                return ConvertJsonShoppingSvcToStoreItem(result.Item);
            }
            catch (Exception ex)
            {
                Debug.Print("ExceptionMessage: " + ex.Message.ToString());
                return localitem;
            }
        }

        public void GrabJsonShoppingSvc(string input)
        {
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
                repo.Save();
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
                Description = (item.Description != null ? HtmlDocument.HtmlEncode(item.Description) : ""),
                ViewItemURLForNaturalSearch = item.ViewItemURLForNaturalSearch,
                EndTime = item.EndTime,
                Price = item.ConvertedCurrentPrice.Value,
                PrimaryCategoryID = long.Parse(item.PrimaryCategoryID),
                PrimaryCategoryName = item.PrimaryCategoryName,
                Image = Convert.ToBase64String(LoadBytesFromUrl(item.GalleryURL))
            };
        }

        public StoreItem ExpandItem(StoreItem localitem)
        {
            localitem = GrabSingleItem(localitem); //just to fill up description
            var url = FindingApiAddress + "&SECURITY-APPNAME=" + appID + "&outputSelector=PictureURLLarge&sortOrder=startTime&RESPONSE-DATA-FORMAT=xml&REST-PAYLOAD&callname=OPERATION-NAME=findItemsByKeywords&keywords=" + localitem.Title.Replace("&", " ");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(url);
                XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
                manager.AddNamespace("ns", doc.DocumentElement.NamespaceURI);
                foreach (XmlNode nodes in doc.SelectNodes("//ns:searchResult", manager))
                {
                    foreach (XmlNode node in nodes.SelectNodes("//ns:item", manager))
                    {
                        if (node["itemId"].InnerText == localitem.ItemID)
                        {
                            if (node["galleryPlusPictureURL"] != null && !string.IsNullOrEmpty(node["galleryPlusPictureURL"].InnerText))
                            {
                                localitem.Image = Convert.ToBase64String(LoadBytesFromUrl(node["galleryPlusPictureURL"].InnerText));
                            }
                            else if (node["pictureURLLarge"] != null && !string.IsNullOrEmpty(node["pictureURLLarge"].InnerText))
                            {
                                localitem.Image = Convert.ToBase64String(LoadBytesFromUrl(node["pictureURLLarge"].InnerText));
                            }
                        }

                        if (node["listingInfo"]["startTime"] != null && node["sellingStatus"]["convertedCurrentPrice"] != null)
                        {
                            var date = node["listingInfo"]["startTime"].InnerText;
                            var price = node["sellingStatus"]["convertedCurrentPrice"].InnerText;
                            var dprice = double.Parse(price, CultureInfo.InvariantCulture);
                            var priceitem = new DataPoint(date, dprice);

                            if (localitem.PriceArray == null)
                            {
                                localitem.PriceArray = new List<DataPoint>();
                                localitem.PriceArray.Add(priceitem);
                            }
                            else if (localitem.PriceArray.Any(x => x.Label != date))
                            {
                                localitem.PriceArray.Add(priceitem);
                            }
                        }
                    }
                }
                repo.UpdateProduct(localitem);
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