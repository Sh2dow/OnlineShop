using System;
using System.Collections.Generic;
using OnlineShop.DL;
using System.Net;
using Newtonsoft.Json;
using OnlineShop.BL.Services.Interfaces;
using System.Configuration;
using System.IO;
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
            catch
            {
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
            catch
            {

            }
        }

        public StoreItem ConvertJsonShoppingSvcToStoreItem(ShoppingItem item)
        {
            return new StoreItem
            {
                ItemID = item.ItemID,
                Title = item.Title,
                Description = (item.Description != null ? HtmlDocument.HtmlEncode(item.Description) : ""),
                ViewItemURLForNaturalSearch = item.ViewItemURLForNaturalSearch,
                EndTime = item.EndTime,
                Price = item.ConvertedCurrentPrice.Value,
                PrimaryCategoryID = item.PrimaryCategoryID,
                PrimaryCategoryName = item.PrimaryCategoryName,
                Image = Convert.ToBase64String(LoadBytesFromUrl(item.PictureURL != null && item.PictureURL.Count > 0 ? item.PictureURL[0] : item.GalleryURL))
            };
        }

        /// <summary>
        /// Downloading and parsing xml with ebay Finding Api to set model with detailed information 
        /// </summary>
        /// <param name="localitem"></param>
        /// <returns></returns>
        public StoreItem ExpandItem(StoreItem localitem)
        {
            localitem = GrabSingleItem(localitem);
            //looking for similar items by title
            string url = FindingApiAddress + "&SECURITY-APPNAME=" + appID +
                "&outputSelector=PictureURLLarge&sortOrder=startTime&RESPONSE-DATA-FORMAT=xml&callname=OPERATION-NAME=findItemsByKeywords&keywords=" +
                localitem.Title.Replace("&", " ");
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
                        if (node["listingInfo"]["startTime"] != null && node["sellingStatus"]["convertedCurrentPrice"] != null)
                        {
                            string date = node["listingInfo"]["startTime"].InnerText;
                            string price = node["sellingStatus"]["convertedCurrentPrice"].InnerText;

                            var dprice = double.Parse(price, CultureInfo.InvariantCulture);
                            var priceitem = new DataPoint(date, dprice);

                            if (localitem.PriceArray == null)
                            {
                                localitem.PriceArray = new List<DataPoint>();
                                localitem.PriceArray.Add(priceitem);
                            }
                            else if (localitem.PriceArray.Any(x => x.X != date))
                            {
                                localitem.PriceArray.Add(priceitem);
                            }
                            //sort price dynamics data by date
                            localitem.PriceArray.Sort((x, nx) => DateTime.Compare(Convert.ToDateTime(x.X), Convert.ToDateTime(nx.X))); 
                        }
                    }
                }
                repo.UpdateProduct(localitem);
                repo.Save();
                return localitem;
            }
            catch
            {
                return localitem;
            }
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