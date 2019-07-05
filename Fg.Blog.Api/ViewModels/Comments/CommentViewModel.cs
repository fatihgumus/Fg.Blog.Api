using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fg.Blog.Api.ViewModels.Comments
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public long BlogId { get; set; }
        public long CreationTime { get; set; }
        public string Content { get; set; }
    }

}