using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface IGrabService
    {
        void GrabTopItemsByKeyword(string keyword);
        void GrabTopItemsByCategory(long categoryId);
        void GrabJsonShoppingSvc(string input);
        LocalItem ExtendItem(LocalItem localitem);
        //LocalItem GrabJsonFindingSvc(string input);
    }
}
