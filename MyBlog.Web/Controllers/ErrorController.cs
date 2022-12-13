using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }


        public IActionResult Error403()
        {
            return View();
        }
    }
}
