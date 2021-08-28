using Microsoft.AspNetCore.Http;
using System;
using TimerZ.Api.Extensions;
using TimerZ.Common;

namespace TimerZ.Api.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public UserProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid GetUserId()
        {
            return _accessor.HttpContext.User.GetUserId();
        }
    }
}
