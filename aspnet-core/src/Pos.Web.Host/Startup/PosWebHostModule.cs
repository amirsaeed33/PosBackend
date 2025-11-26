using Abp.Modules;
using Abp.Reflection.Extensions;
using Pos.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Pos.Web.Host.Startup
{
    [DependsOn(
       typeof(PosWebCoreModule))]
    public class PosWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PosWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PosWebHostModule).GetAssembly());
        }
    }
}
