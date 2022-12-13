using MyBlog.Entity.Concrete;

namespace MyBlog.Web.Areas.Manager.ViewModels
{
    public class ListPostViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public string ImageUrl { get; set; }

        public string  Encrypedid { get; set; }



    }
}
