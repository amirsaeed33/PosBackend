using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pos.Authorization;

namespace Pos;

[DependsOn(
    typeof(PosCoreModule),
    typeof(AbpAutoMapperModule))]
public class PosApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<PosAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(PosApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
