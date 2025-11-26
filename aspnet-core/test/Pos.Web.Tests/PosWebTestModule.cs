using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pos.EntityFrameworkCore;
using Pos.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Pos.Web.Tests;

[DependsOn(
    typeof(PosWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class PosWebTestModule : AbpModule
{
    public PosWebTestModule(PosEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PosWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(PosWebMvcModule).Assembly);
    }
}