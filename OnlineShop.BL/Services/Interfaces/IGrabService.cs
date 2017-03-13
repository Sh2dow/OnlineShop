using OnlineShop.Models;
using OnlineShop.Models.ShoppingSvcItem;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface IGrabService
    {
        void GrabTopItemsByKeyword(string keyword);
        void GrabTopItemsByCategory(long categoryId);
        void GrabJsonShoppingSvc(string input);
        StoreItem GrabSingleItem(StoreItem localitem);
        StoreItem ExpandItem(StoreItem localitem);
        StoreItem ConvertJsonShoppingSvcToStoreItem(Item item);
        byte[] LoadBytesFromUrl(string url);
    }
}
