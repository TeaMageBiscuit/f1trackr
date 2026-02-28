using System.Security.Claims;
using F1Trackr.Core.Domain.Management;

namespace F1Trackr.Core;

public static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal principal)
    {
        public UserId? UserId
        {
            get
            {
                var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out var id))
                {
                    return new UserId(id);
                }

                return null;
            }
        }

        public bool IsAdmin => principal.IsInRole(UserRoles.Admin);
    }
}
