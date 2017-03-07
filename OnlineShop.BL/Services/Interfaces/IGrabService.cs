using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface IGrabService
    {
        void GrabItemsByKeyword(string keyword);
        void GrabItemsByCategory(long categoryId);
        void GrabJson(string[] input);
    }
}
