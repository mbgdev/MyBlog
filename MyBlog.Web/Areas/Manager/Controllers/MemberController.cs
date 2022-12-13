using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;
using System.Data;

namespace MyBlog.Web.Areas.Manager.Controllers
{


    [Area("Manager")]
    [Authorize(Roles = "Admin,Editör,Stajyer")]

    public class MemberController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;

        public MemberController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);


            return View(_mapper.Map<MemberAppUserViewModel>(user));


        }

        [HttpGet]

        public IActionResult EditMember()
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            return View(_mapper.Map<MemberAppUserViewModel>(user));
        }

        [HttpPost]

        public async Task<IActionResult> EditMember(MemberAppUserViewModel viewModel, IFormFile userPicture)
        {


            ModelState.Remove("ImageUrl");


            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (userPicture != null && userPicture.Length > 0)
                {
                    if(viewModel.ImageUrl!=null)
                    {
                        string oldImagePath = $"{Directory.GetCurrentDirectory()}\\wwwroot{viewModel.ImageUrl}";

                        System.IO.File.Delete(oldImagePath);
                    }
              

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        viewModel.ImageUrl = "/Images/" + fileName;
                    }


                }
                else
                    viewModel.ImageUrl = user.ImageUrl;





                var result = await _userManager.UpdateAsync(_mapper.Map(viewModel, user));

                if (result.Succeeded)
                {



                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);

                    return RedirectToAction("Index");


                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, item.Description);
                    }
                }


            }


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel viewModel)
        {



            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    bool exist = await _userManager.CheckPasswordAsync(user, viewModel.PasswordOld);


                    if (exist)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, viewModel.PasswordOld, viewModel.PasswordNew);

                        if (result.Succeeded)
                        {
                            await _userManager.UpdateSecurityStampAsync(user);
                            await _signInManager.SignOutAsync();
                            await _signInManager.PasswordSignInAsync(user, viewModel.PasswordNew, true, false);

                            ViewBag.msg = "Şifreniz Başarılı Bir Şekilde Değiştirilmiştir.";


                           
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }

                        }

                    }

                    else
                    {

                        ModelState.AddModelError("", "Eski şifreniz yanlış girdiriz.");


                    }
                }
            }






            return View(viewModel);
        }


    }
}
