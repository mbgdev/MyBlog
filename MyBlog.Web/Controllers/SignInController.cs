using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Helper;
using MyBlog.Web.ViewModels;

namespace MyBlog.Web.Controllers
{
    public class SignInController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public SignInController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(AppUserViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    UserName=viewModel.UserName,
                    Email=viewModel.Email,
                    Name=viewModel.Name,
                    Surname=viewModel.Surname,
                    BirthDay=viewModel.BirthDay,
                    PhoneNumber=viewModel.PhoneNumber,

                };

                
                var  result=await _userManager.CreateAsync(user,viewModel.Password);

                if(result.Succeeded)
                {
                    string ConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    string link = Url.Action("ConfirmEmail", "SignIn", new
                    {
                        userId = user.Id,
                        token = ConfirmToken,

                    }, protocol: HttpContext.Request.Scheme
                    );

                    EmailConfirmation.EmailConfirmSendEmail(link, viewModel.Email, viewModel.Name);



                   return  RedirectToAction("SendEmail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(viewModel);
        }


        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {

            var user =await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Status = "E-posta adresniz onaylanmıştır";
            }
            else
            {
                ViewBag.Status = "Bir hata oluştu. Lütfen daha sonra tekrar deneyiniz";
            }

            return View();

        }

        public IActionResult SendEmail()
        {
            return View();
        }

    }
}
