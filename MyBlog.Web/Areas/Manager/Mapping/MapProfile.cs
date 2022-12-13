using AutoMapper;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;
using MyBlog.Web.ViewModels;

namespace MyBlog.Web.Areas.Manager.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUser, MemberAppUserViewModel>();
            CreateMap<AppUser, MemberComponentAppUserViewModel>();
            CreateMap<MemberAppUserViewModel, AppUser >();
            CreateMap<MemberComponentAppUserViewModel, AppUser >();
            CreateMap<AdminUserListViewModel, AppUser>();
            CreateMap<AppUser, AdminUserListViewModel>();
            CreateMap<AppRole, RoleViewModel>();
            CreateMap<RoleViewModel,AppRole>();
            CreateMap<RoleAssignViewModel,AppRole>();
            CreateMap<AppRole,RoleAssignViewModel>();
            CreateMap<Post, ListPostViewModel>();
            CreateMap<ListPostViewModel, Post>();
            CreateMap<DetailPostViewModel, Post>();
            CreateMap<Post,DetailPostViewModel>();
            CreateMap<Message,MessageViewModel>();
            CreateMap<MessageViewModel, Message>();



        }
    }
}
