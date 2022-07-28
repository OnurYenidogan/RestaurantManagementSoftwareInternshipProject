using System.Web;
using System.Web.Mvc;

namespace MVCRestaurant27Tem2022
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
