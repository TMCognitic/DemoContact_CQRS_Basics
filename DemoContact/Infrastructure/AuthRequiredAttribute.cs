using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoContact.Infrastructure
{
    public class AuthRequiredAttribute : TypeFilterAttribute
    {
        public AuthRequiredAttribute() : base(typeof(AuthRequiredFilter))
        {
        }

        private class AuthRequiredFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                SessionManager sessionManager = context.HttpContext.RequestServices.GetService<SessionManager>()!;

                if (!sessionManager.UserId.HasValue)
                {
                    context.Result = new RedirectToRouteResult(new { action = "Login", controller = "Auth" });
                }
            }
        }
    }

    
}
