using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimerZ.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (!principal.Claims.Any()) return Guid.Empty;
            return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }

    }
}
