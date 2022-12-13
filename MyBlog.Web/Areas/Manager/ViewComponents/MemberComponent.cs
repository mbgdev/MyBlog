using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;

namespace MyBlog.Web.Areas.Manager.ViewComponents
{
    public class MemberComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
     
        private readonly IMapper _mapper;

        public MemberComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            MemberComponentAppUserViewModel viewModel = _mapper.Map<MemberComponentAppUserViewModel>(user);


            return View(viewModel);
        }
    }
    
}
