using System.Collections.Generic;

namespace Fg.Blog.Api.ViewModels
{
    public class BlogDetailViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public long PublishTime { get; set; } 
        public string OwnerId { get; set; }
        public string OwnerUsername { get; set; }

    }
}