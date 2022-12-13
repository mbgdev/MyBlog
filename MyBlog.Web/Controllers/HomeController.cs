
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Business.Abstract;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;
using MyBlog.Web.ViewModels;
using System.Diagnostics;

namespace MyBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDataProtector _dataProtector;


        public HomeController(IPostService postService, IMapper mapper, UserManager<AppUser> userManager,IDataProtectionProvider dataProtectionProvider)
        {
            _postService = postService;
            _mapper = mapper;
            _userManager = userManager;
            _dataProtector = dataProtectionProvider.CreateProtector("PostId");
        }

        public IActionResult Index()
        {
         
            var result = _postService.PostsWithAuthor().OrderByDescending(x => x.CreatedDate).ToList();

            result.ForEach(x =>
            {
                x.Encrypedid = _dataProtector.Protect(x.Id.ToString());
            });
            

            

            return View(_mapper.Map<List<ListPostViewModel>>(result));
        }

        
        public async Task<IActionResult> PostDetail(string id)
        {
            DetailPostViewModel model = new();

            var decrypedid=_dataProtector.Unprotect(id);

            Post post=await _postService.GetByIdAsync(new Guid(decrypedid));

            var user=await _userManager.FindByIdAsync(post.AppUserId);

            model.Surname = user.Surname;
            model.Name = user.Name;

            return View(_mapper.Map(post,model));
        }



            [Authorize(Policy ="AnkaraPolicy")]
        public  IActionResult AnkaraPage()
        {
            return View();
        }


       



    }
}
