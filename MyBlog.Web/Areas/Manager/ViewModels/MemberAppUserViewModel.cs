using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Areas.Manager.ViewModels
{
    public class MemberAppUserViewModel
    {

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string City { get; set; }
    }
}
