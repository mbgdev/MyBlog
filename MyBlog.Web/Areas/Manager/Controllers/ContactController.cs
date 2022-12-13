using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entity.Concrete;

namespace MyBlog.Web.Areas.Manager.Controllers
{

    [Area("Manager")]
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }



        public IActionResult Index()
        {

            var result = _messageService.GetAll().OrderByDescending(x => x.Status).ToList();


            return View(result);
        }


        [HttpGet]
        public IActionResult EditMessage(Guid id)
        {
            var result=_messageService.GetByIdAsync(id).Result;
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> EditMessage(Message msg)
        {
            msg.ModifiedDate = DateTime.Now;
            if(ModelState.IsValid)
            {
                await _messageService.UpdateAsync(msg);
            }

            return RedirectToAction("Index");
        }



    }
}
