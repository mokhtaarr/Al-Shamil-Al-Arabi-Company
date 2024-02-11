using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Website.Areas.Admin.Data
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        //void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        //{
        //    if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["userId"])))
        //    {
        //        filterContext.Result = new HttpUnauthorizedResult();
        //    }
        //}
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Request.Cookies["userId"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "area","Admin"},{ "controller", "Account" },{ "action", "Login" } 
                    });
            }
        }
    }
}