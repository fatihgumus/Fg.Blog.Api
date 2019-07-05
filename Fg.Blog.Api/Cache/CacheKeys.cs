using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fg.Blog.Api.Cache
{
    public class CacheKeys
    {
        public static string BlogListKey = "BlogList";
        public static string BlogKey(long id)
        {
            return string.Concat("Blog__", id);
        }
        public static string BlogCommentsKey(long id)
        {
            return string.Concat("Blog_Comments__", id);
        }

    }
}
