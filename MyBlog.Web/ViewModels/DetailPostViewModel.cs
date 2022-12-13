using MyBlog.Entity.Concrete;

namespace MyBlog.Web.ViewModels
{
    public class DetailPostViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AppUserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ImageUrl { get; set; }
    }
}
