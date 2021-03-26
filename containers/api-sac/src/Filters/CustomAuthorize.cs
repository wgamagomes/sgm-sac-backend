using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace SGM.SAC.Api.Filters
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles) : base(typeof(ClaimFilter))
        {
            Arguments = new Claim[] { new Claim(ClaimTypes.Role, string.Join(',', roles)) };
        }
    }

    public class ClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }

        public bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
        {
            bool authorized = false;

            var allowedroles = claimValue.Split(',');
            foreach (var role in allowedroles)
            {
                authorized = context.User.Claims.Any(c => c.Type == claimName && c.Value== role);

                if (authorized)
                    break;
            }

            return authorized;
        }
    }
}

