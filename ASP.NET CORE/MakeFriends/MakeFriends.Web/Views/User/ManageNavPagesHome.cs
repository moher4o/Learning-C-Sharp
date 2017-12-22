using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MakeFriends.Web.Views.User
{
    public static class ManageNavPagesHome
    {
        public static string ActivePageKey => "ActivePage";

        public static string NewFriends => "New friendship";

        public static string Favorites => "Favorites";

        public static string Mutualfeelings => "Mutual feelings";

        public static string Likes => "Likes";

        public static string Visitors => "Visitors";

        public static string NewFriendsNavClass(ViewContext viewContext) => PageNavClass(viewContext, NewFriends);

        public static string MutualfeelingsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Mutualfeelings);

        public static string FavoritesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Favorites);

        public static string LikesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Likes);

        public static string VisitorsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Visitors);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
