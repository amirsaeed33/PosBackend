using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Pos.Controllers
{
    public abstract class PosControllerBase : AbpController
    {
        protected PosControllerBase()
        {
            LocalizationSourceName = PosConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
