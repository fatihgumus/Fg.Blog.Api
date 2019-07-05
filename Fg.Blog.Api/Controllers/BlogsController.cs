using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fg.Blog.Api.Cache;
using Fg.Blog.Api.Loging; 
using Fg.Blog.Api.ViewModels;
using Fg.Blog.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Fg.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        IBlogRepository _blogRepository;
        IUserRepository _userRepository;
        ICommentRepository _commentRepository; 
        IMapper _mapper;
        IMemoryCache _cache;
        ILoggerManager _logger;

        public BlogsController(
            IBlogRepository blogRepository,
            IUserRepository userRepository,
            ICommentRepository commentRepository, 
            IMapper mapper,
            IMemoryCache cache,
            ILoggerManager logger)

        {
            this._blogRepository = blogRepository;
            this._commentRepository = commentRepository;
             
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._cache = cache;
            this._logger = logger;

        }

        [HttpGet()]
        public ActionResult<BlogsViewModel> GetBlogs()
        {

            _logger.LogInfo("Getting Blogs");
            List<Model.Blog> blogs;
            if (!_cache.TryGetValue<List<Model.Blog>>(CacheKeys.BlogListKey, out blogs))
            {
                blogs = _blogRepository.AllIncluding(s => s.Owner).ToList();
                _cache.Set<List<Model.Blog>>(CacheKeys.BlogListKey, blogs);
            }
            return new BlogsViewModel
            {
                Blogs = blogs.Select(_mapper.Map<BlogViewModel>).ToList()
            };
        }

        [HttpGet("{id}")]
        public ActionResult<BlogDetailViewModel> GetBlogDetail(long id)
        {

            _logger.LogInfo("Get Blog Detail");

            Model.Blog blog;
            if (!_cache.TryGetValue<Model.Blog>(CacheKeys.BlogKey(id), out blog))
            {
                blog = _blogRepository.GetSingle(s => s.Id == id, s => s.Owner);
                _cache.Set<Model.Blog>(CacheKeys.BlogKey(id), blog);
            }
            var userId = HttpContext.User.Identity.Name;
            return _mapper.Map<Model.Blog, BlogDetailViewModel>(blog);
        }

        [HttpPost]
        public ActionResult<BlogCreationViewModel> Post([FromBody]UpdateBlogViewModel model)
        {
            var creationTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

            var blog = new Model.Blog
            {

                Title = model.Title,
                Content = model.Content,
                CreationTime = creationTime,
                LastEditTime = creationTime,
                OwnerId = NVLInt64(HttpContext.User.Identity.Name, 0),
                Draft = true
            };

            _blogRepository.Add(blog);
            _blogRepository.Commit();

            return new BlogCreationViewModel
            {
                BlogId = blog.Id
            };
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(long id, [FromBody]UpdateBlogViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

       
            var newBlog = _blogRepository.GetSingle(s => s.Id == id);

            newBlog.Title = model.Title;
            newBlog.LastEditTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

            newBlog.Content = model.Content;

            _blogRepository.Update(newBlog);
            _blogRepository.Commit();



            return NoContent();
        }


        [HttpGet("title/{title}")]
        public ActionResult<BlogsViewModel> Get(string title)
        {
            var blogs = _blogRepository.FindBy(blog => blog.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase) && !blog.Draft);
            return new BlogsViewModel
            {
                Blogs = blogs.Select(_mapper.Map<BlogViewModel>).ToList()
            };
        }

        [HttpGet("user/{id}")]
        public ActionResult<OwnerStoriesViewModel> Get(long id)
        {
            var blogs = _blogRepository.FindBy(blog => blog.OwnerId == id && !blog.Draft);
            return new OwnerStoriesViewModel
            {
                Stories = blogs.Select(_mapper.Map<OwnerBlogViewModel>).ToList()
            };
        }
         
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            Model.Blog blg;
            blg = _blogRepository.GetSingle(s => s.Id == id, s => s.Owner);

            if (blg.OwnerId != NVLInt64(HttpContext.User.Identity.Name,0))
            {
                return Forbid("Sadece Kendi Bloðunuzla Ýlgili Ýþlem Yapabilirsiniz");
            }

            _commentRepository.DeleteWhere(co => co.BlogId == id);
            _blogRepository.DeleteWhere(blog => blog.Id == id);
            _blogRepository.Commit();

            return NoContent();
        }


        public static Int64 NVLInt64(string value, Int64 nullValue)
        {
            Int64 number;
            if (!Int64.TryParse(value, out number))
                number = nullValue;
            return number;
        }

    }
}