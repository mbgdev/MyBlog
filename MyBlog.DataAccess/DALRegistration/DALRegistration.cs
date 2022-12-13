using Microsoft.Extensions.DependencyInjection;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Concrete;
using MyBlog.DataAccess.EntityFramework;
using MyBlog.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using MyBlog.DataAccess.Repository;


namespace MyBlog.DataAccess.DALRegistration
{
    public static class DALRegistration
    {

        public static void AddDALRegistration(this IServiceCollection services)
        {

            services.AddScoped (typeof(IGenericRepositoryDAL<>),typeof(GenericRepository<>));

            services.AddScoped<IPostDAL, EFPostDAL>();
            services.AddScoped<IMessageDAL, EFMessageDAL>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();






        }
    }

}
