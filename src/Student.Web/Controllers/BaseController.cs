using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Student_WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsLoggedIn =>
            !string.IsNullOrEmpty(HttpContext.Session.GetString("JWT"));

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsLoggedIn)
            {
                context.Result =
                    new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }

}
