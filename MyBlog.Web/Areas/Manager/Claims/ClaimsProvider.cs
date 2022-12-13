using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MyBlog.Entity.Concrete;
using System.Security.Claims;

namespace MyBlog.Web.Areas.Manager.Claims
{
    public class ClaimsProvider : IClaimsTransformation
    {
        public UserManager<AppUser> userManager;

        public ClaimsProvider(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal != null && principal.Identity.IsAuthenticated)
            {

                ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;

                AppUser user = await userManager.FindByNameAsync(claimsIdentity.Name);

                if (user != null)
                {
                    if (user.City != null)
                    {

                        if (!principal.HasClaim(c => c.Type == "City"))
                        {
                            Claim CityClaim = new Claim("City", user.City, ClaimValueTypes.String, "Internal");

                            claimsIdentity.AddClaim(CityClaim);

                        }


                    }


                }


            }

            return principal;
        }
    }
}
