using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.Concrete
{
    public partial class Post
    {
        [NotMapped]
        public string Encrypedid { get; set; }
    }
}
