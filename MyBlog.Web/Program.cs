using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Business.BusinessRegistration;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Concrete;
using MyBlog.DataAccess.DALRegistration;
using MyBlog.DataAccess.Repository;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.Claims;
using MyBlog.Web.Areas.Manager.Mapping;
using MyBlog.Web.CookieConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));


builder.Services.AddDALRegistration();
builder.Services.AddBusinessRegistration();


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AnkaraPolicy", policy =>
    {
        policy.RequireClaim("City", "Ankara");
    });
});

builder.Services.AddCookiest();
builder.Services.AddScoped<IClaimsTransformation, ClaimsProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithRedirects("/Error/Error{0}");


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Post}/{action=Index}/{id?}"
    );


    endpoints.MapControllerRoute(
   name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
                   name: "men",
                   pattern: "{controller}/{action}/{id?}");

});

app.Run();
