using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface IGrabService
    {
        IEnumerable<Item> GrabItemsByKeyword(string keyword);
        IEnumerable<Item> GrabItemsByCategory(int categoryId);
        IEnumerable<Item> GrabJson(string[] input);
    }
}
