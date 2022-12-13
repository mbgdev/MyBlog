namespace MyBlog.Web.Areas.Manager.ViewModels
{
    public class AddPostViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public string AppUserId { get; set; }

     
    }
}
