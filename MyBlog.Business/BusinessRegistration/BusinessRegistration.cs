using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.Business.IdentityCustomValidator;
using MyBlog.DataAccess.Concrete;
using MyBlog.Entity.Concrete;

namespace MyBlog.Business.BusinessRegistration
{
    public static class BusinessRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostManager>();
            services.AddScoped<IMessageService, MessageManager>();


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.AllowedUserNameCharacters =
            "abcçdefghiıjklmnoöpqrstuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSTUÜVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;


                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 5;



            })
               
                .AddPasswordValidator<CustomPasswordValidator>()
                 .AddUserValidator<CustomUserValidator>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(1));


        }
    }
}
