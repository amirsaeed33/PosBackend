using Abp.Authorization.Users;
using Abp.Extensions;
using System;
using System.Collections.Generic;

namespace Pos.Authorization.Users;

public class User : AbpUser<User>
{
    public const string DefaultPassword = "123qwe";

    /// <summary>
    /// Profile picture URL (can be base64 encoded image or external URL)
    /// </summary>
    public string ProfilePictureUrl { get; set; }

    public static string CreateRandomPassword()
    {
        return Guid.NewGuid().ToString("N").Truncate(16);
    }

    public static User CreateTenantAdminUser(int tenantId, string emailAddress)
    {
        var user = new User
        {
            TenantId = tenantId,
            UserName = AdminUserName,
            Name = AdminUserName,
            Surname = AdminUserName,
            EmailAddress = emailAddress,
            Roles = new List<UserRole>()
        };

        user.SetNormalizedNames();

        return user;
    }
}
