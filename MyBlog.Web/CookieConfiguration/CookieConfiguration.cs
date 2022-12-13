using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyBlog.Web.CookieConfiguration
{
    public static class CookieConfiguration
    {
        public static void AddCookiest(this IServiceCollection services)
        {




            CookieBuilder cookieBuilder = new CookieBuilder();

            cookieBuilder.Name = "authToken";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Home/LogIn");
                options.LogoutPath = new PathString("/Admin/User/LogOut");

                options.Cookie = cookieBuilder;
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = new PathString("/Error/Error403");

            });





        }
    }
}
