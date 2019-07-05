using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fg.Blog.Api.Cache;
using Fg.Blog.Api.Loging; 
using Fg.Blog.Api.ViewModels;
using Fg.Blog.Api.ViewModels.Comments;
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
    public class CommentsController : ControllerBase
    { 
        ICommentRepository _commentRepository; 
        IMapper _mapper;
        IMemoryCache _cache;
        ILoggerManager _logger;

        public CommentsController(
            IBlogRepository blogRepository,
            IUserRepository userRepository,
            ICommentRepository commentRepository,
           
            IMapper mapper,
            IMemoryCache cache,
            ILoggerManager logger)

        { 
            this._commentRepository = commentRepository; 
            this._mapper = mapper;
            this._cache = cache;
            this._logger = logger;

        }
 

        [HttpGet("{id}")]
        public ActionResult<List<CommentViewModel>> GetBlogComments(long id)
        { 
            _logger.LogInfo("Blog yorumlarý alýnyor "+ id +". " );

            List<Model.Comment> blogComments;
            if (!_cache.TryGetValue<List<Model.Comment>>(CacheKeys.BlogCommentsKey(id), out blogComments))
            {
                blogComments = _commentRepository.FindBy(s => s.BlogId == id).ToList();
                if (blogComments != null)
                {
                    _cache.Set<List<Model.Comment>>(CacheKeys.BlogCommentsKey(id), blogComments);
                }
            }
            var userId = HttpContext.User.Identity.Name;
            return _mapper.Map<List<Model.Comment>, List<CommentViewModel>>(blogComments);
        }

        [HttpPost]
        public ActionResult<CommentViewModel> Post([FromBody]CommentCreationViewModel model)
        {
            var creationTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(); 
            var comm = new Model.Comment
            {
                BlogId = model.BlogId,
                UserId = NVLInt64(HttpContext.User.Identity.Name, 0),
                Content = model.Content,
                CreationTime = creationTime,
            };
            _commentRepository.Add(comm);
            _commentRepository.Commit();
            return _mapper.Map<Model.Comment, CommentViewModel>(comm); 
        }


        [HttpPatch("{id}")]
        public ActionResult Patch(long id, [FromBody]CommentCreationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
            var comm = _commentRepository.GetSingle(s => s.Id == id);
            comm.Content = model.Content; 
            _commentRepository.Update(comm);
            _commentRepository.Commit();
            _logger.LogInfo("Yorum güncellendi " + id + ". ");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        { 
            _commentRepository.DeleteWhere(co => co.Id == id);
            _commentRepository.Commit();
            _logger.LogInfo("Yorum silindi " + id + ".");
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