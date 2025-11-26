using Pos.Configuration.Dto;
using System.Threading.Tasks;

namespace Pos.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
