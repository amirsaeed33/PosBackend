using Abp.Authorization;
using Pos.Authorization.Roles;
using Pos.Authorization.Users;

namespace Pos.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
