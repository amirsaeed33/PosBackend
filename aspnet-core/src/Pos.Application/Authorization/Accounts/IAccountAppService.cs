using Abp.Application.Services;
using Pos.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace Pos.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
