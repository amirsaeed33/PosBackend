using Abp.Authorization;
using Abp.Runtime.Session;
using Pos.Configuration.Dto;
using System.Threading.Tasks;

namespace Pos.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : PosAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
