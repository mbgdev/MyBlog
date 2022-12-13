using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entity.Concrete;
using MyBlog.Web.ViewModels;

namespace MyBlog.Web.Controllers
{
    public class ContactController : Controller
    {

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ContactController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel viewModel)
        {

            viewModel.Id = Guid.NewGuid();
            viewModel.Status = true;
            viewModel.CreatedDate=DateTime.Now;
            
            if (ModelState.IsValid)
            {
                Message mesaj = new();

               await _messageService.AddAsync(_mapper.Map(viewModel, mesaj));

                ViewBag.Message = "Mesajınız Gönderilmiştir";
                return Json(new { isSuccess = "True" });

            }
            else
            {
              

                return BadRequest(viewModel);

            }
        }
    }
}
