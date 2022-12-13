using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
       
        [Required(ErrorMessage ="Bu alan Boş geçilemez")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Messages { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
