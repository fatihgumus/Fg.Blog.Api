using Microsoft.AspNetCore.SignalR;

namespace Fg.Blog.Api.Providers
{
  public class UserIdProvider : IUserIdProvider
  {
    public string GetUserId(HubConnectionContext connection)
    {
      return connection.User.Identity.Name;
    }
  }
}