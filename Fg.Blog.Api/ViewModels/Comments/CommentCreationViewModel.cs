using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fg.Blog.Api.ViewModels.Comments
{
    public class CommentCreationViewModel
    { 
        public long BlogId { get; set; } 
        public string Content { get; set; }
    }

}