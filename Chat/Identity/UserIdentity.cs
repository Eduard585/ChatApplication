using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Chat.Identity
{
    public class UserIdentity
    {
        public long GetUserId(IPrincipal principal)
        {
            var value = ((ClaimsPrincipal)principal).FindFirst(ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt64(value);
        }
    }
}
