﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.Concrete
{
    public partial class Post
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

      
 

    }
}
