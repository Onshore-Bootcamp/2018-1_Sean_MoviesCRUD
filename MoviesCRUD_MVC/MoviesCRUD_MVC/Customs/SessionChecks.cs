using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Customs
{
    public class SessionChecks : ActionFilterAttribute
    {
        private readonly string _Key = "";
        private readonly int[] _Allowed;
        public SessionChecks(string key, params int[] roles)
        {
            _Key = key;
            _Allowed = roles ?? new int[0];
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if not signed in or user role isn't in allowed roles, then redirect to login
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            

            if (session[_Key] == null || !_Allowed.Contains(Convert.ToInt32(session[_Key])))
            {
                filterContext.Result = new RedirectResult("/Account/Login", false);
            }
            else
            {

            }
            base.OnActionExecuting(filterContext);
        }
    }
}