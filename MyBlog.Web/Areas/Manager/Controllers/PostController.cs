using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;
using System.Data;


namespace MyBlog.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Admin,Editör,Stajyer")]

    public class PostController : Controller
    {
        private readonly IPostService _postService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IMapper _mapper;


        public PostController(IPostService postService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _postService = postService;
            _userManager = userManager;
            _mapper = mapper;
        }



        public IActionResult Index()
        {

            var result = _postService.PostsWithAuthor().OrderByDescending(x => x.CreatedDate).ToList();



            return View(_mapper.Map<List<ListPostViewModel>>(result));
        }

        public IActionResult MyPost()
        {

            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;



            var result = _postService.PostsWithAuthor(user.Id).OrderByDescending(x => x.CreatedDate).ToList();





            return View(_mapper.Map<List<ListPostViewModel>>(result));
        }


        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel viewModel, IFormFile userPicture)
        {
            viewModel.Id = Guid.NewGuid();
            viewModel.CreatedDate = DateTime.Now;
            viewModel.ModifiedDate = DateTime.Now;
            viewModel.Status = true;
        
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            viewModel.AppUserId = user.Id;

            if (ModelState.IsValid)
            {


                Post newPost = new()
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    CreatedDate = viewModel.CreatedDate,
                    ModifiedDate = viewModel.ModifiedDate,
                    Status = viewModel.Status,
                    AppUserId = viewModel.AppUserId,
                    AppUser = user,

                };

                if (userPicture != null && userPicture.Length > 0)
                {
                    
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/postImage", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        newPost.ImageUrl = "/postImage/" + fileName;
                    }


                }






                await _postService.AddAsync(newPost);
                return RedirectToAction("MyPost");

            }


            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var result = await _postService.GetByIdAsync(id);




            return View(_mapper.Map<ListPostViewModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(ListPostViewModel viewModel, IFormFile userPicture)
        {

            viewModel.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                if (userPicture != null && userPicture.Length > 0)
                {
                    if (viewModel.ImageUrl != null)
                    {
                        string oldImagePath = $"{Directory.GetCurrentDirectory()}\\wwwroot{viewModel.ImageUrl}";

                        System.IO.File.Delete(oldImagePath);
                    }


                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/postImage", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        viewModel.ImageUrl = "/postImage/" + fileName;
                    }


                }
            

                var post = _mapper.Map<Post>(viewModel);
               await _postService.UpdateAsync(post);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bir hata oluştu");
            return View(viewModel);
        }


     
        public  async  Task<IActionResult> DeletePost(Guid id)
        {
            var result=await _postService.GetByIdAsync(id);

            await _postService.RemoveAsync(result);

            return RedirectToAction("MyPost");
        }

    }
}
