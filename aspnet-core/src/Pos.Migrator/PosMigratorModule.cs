using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pos.Configuration;
using Pos.EntityFrameworkCore;
using Pos.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace Pos.Migrator;

[DependsOn(typeof(PosEntityFrameworkModule))]
public class PosMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public PosMigratorModule(PosEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(PosMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            PosConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PosMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
