using System.Web;
using System.Web.Mvc;

namespace Ecommerce_CubicTaks_.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
